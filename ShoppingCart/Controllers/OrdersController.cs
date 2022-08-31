using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Interfaces;
using ShoppingCart.DAL.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stripe.Checkout;
using ShoppingCart.DTO.DTOs;
using ShoppingCart.BLL.Class;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersBL _ordersRepository;
        private readonly ILoggerManager _loggerManager;
        public OrdersController(OrdersBL ordersRepository, ILoggerManager loggerManager)
        {
            _ordersRepository = ordersRepository;
            _loggerManager = loggerManager;
        }
        [HttpPost("AddBookToWishlist")]
        public async Task<ActionResult> AddBookToWishlist([FromBody] AddWishListDto wishlist)
        {
            try
            {
                if (wishlist is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _ordersRepository.AddToWishList(wishlist);
                return Ok();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside AddBookToWishlist action: {ex.Message}");
                return StatusCode(500, $"Internal server error:{ex}");
            }

        }

        [HttpGet("GetWishListItem")]
        public async Task<ActionResult> GetWishListItem(int userId)
        {
            try
            {
                if (userId < 0)
                {
                    return BadRequest();
                }

                var wishListItem = await _ordersRepository.GetWishListItemByUserId(userId);
                return Ok(wishListItem);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside AddBookToWishlist action: {ex.Message}");
                return StatusCode(500, $"Internal server error:{ex}");
            }

        }

        [HttpPost("ShippingDetails")]
        public async Task<ActionResult> ShippingDetails([FromBody] ShippingDto shipping)
        {
            try
            {
                if (shipping is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _ordersRepository.AddShippingDetails(shipping);
                return Ok();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside AddBookToWishlist action: {ex.Message}");
                return StatusCode(500, $"Internal server error:{ex}");
            }
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var orderdetail = await _ordersRepository.GetOrdersAsync();
            return Ok(orderdetail);
        }

        [HttpGet("GetOrderStatus")]
        public async Task<IActionResult> GetOrderStatus()
        {

            var orders = await _ordersRepository.GetOrdersStatus();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var orderdetail = await _ordersRepository.GetOrderByIdAsync(id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            return Ok(orderdetail);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDetailDto orderdto)
        {
            var orderdetail = await _ordersRepository.AddOrderAsync(orderdto);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderdetail, Controller = "OrderDetail" }, orderdto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDetailDto orderdto, [FromRoute] int id)
        {
            await _ordersRepository.UpdateOrderAsync(id, orderdto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            await _ordersRepository.DeleteOrderAsync(id);
            return Ok();
        }

        [HttpPost("Checkout")]
        public async Task<ActionResult<CheckOutDto>> Checkout([FromBody] int orderTotal, int userId)
        {
            try

            {
                if (orderTotal == 0)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                var data = await _ordersRepository.CheckOut(orderTotal, userId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }
        }

        [HttpPost("AddOrderDetails")]
        public async Task<ActionResult> AddOrderDetails(int[] bookId, int userId, int checkoutId)
        {
            try
            {
                if (bookId.Length > 0 && userId > 0 && checkoutId > 0)
                {
                    await _ordersRepository.AddOrderDetails(bookId, userId, checkoutId);
                }

                else
                {
                    return BadRequest("Invalid Input");
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
            }

            return Ok();
        }

        [HttpGet("GetOrdersPlaced")]
        public async Task<IEnumerable<OrderPlcedDTO>> GetOrdersPlaced(int userId)
        {
            var orders = await _ordersRepository.GetOrdersPlaced(userId);
            return orders;
        }

        [HttpPost("StripePayment")]
        public IActionResult StripePayment([FromBody] StripePaymentRequest paymentRequest)
        {
            try
            {
                // ========For Creating a new user========
                //var options1 = new CustomerCreateOptions
                //{
                //    Description = "Test User 2",
                //    Email="testuser@gmail.com",
                //    Name="Test User",
                //    Phone="9166948765",

                //};
                //var service = new CustomerService();
                //service.Create(options1);


                var paymentMethod = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardOptions
                    {
                        Number = "5555555555554444",
                        ExpMonth = 7,
                        ExpYear = 2023,
                        Cvc = "314"
                       

                    },
                };
                var payment = new PaymentMethodService();
                var cardDetails = payment.Create(paymentMethod);
                var options = new PaymentIntentCreateOptions
                {
                    Amount = Convert.ToInt32(paymentRequest.amount),
                    Currency = "inr",
                    Description = "Order Name : " + paymentRequest.productName,
                    Customer = "cus_M3ZkjkHABcPsew",
                    PaymentMethodTypes = new List<string> { "card", },
                    PaymentMethod = cardDetails.Id,//"pm_1LNtV6SBVnjJH0yCu611zor6",
                   

                    Metadata = new Dictionary<string, string> { { "OrderId", paymentRequest.tokenId }, },
                    SetupFutureUsage = "on_session"
                };

                var chargeService = new PaymentIntentService();
                var paymentIntent = chargeService.Create(options);
                chargeService.Confirm(paymentIntent.Id);

                AtttachPaymentMethod("pm_1LNtV6SBVnjJH0yCu611zor6");

                var aetUpIntent = new SetupIntentCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                      {
                        "card",
                      },
                    Customer= "cus_M3ZkjkHABcPsew",
                    PaymentMethod = "pm_1LNtV6SBVnjJH0yCu611zor6"
                };

                var setupService = new SetupIntentService();
                var setupResult= setupService.Create(aetUpIntent);

                var setupIntent = new SetupIntentConfirmOptions
                {
                    PaymentMethod = "pm_1LNtV6SBVnjJH0yCu611zor6",
                };
                var setupIntentResult= setupService.Confirm(setupResult.Id);
                //var service2 = new EventService();
                //var rr= service2.Get(paymentIntent.Id);

              //  CreateInvoice(paymentRequest);
                return Ok(paymentIntent);
            } 
            catch(Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                return StatusCode(500, $"Internal server error:{ex}");
            }
            
        }

        private void AtttachPaymentMethod(string paymentMthodId)
        {
            var options = new PaymentMethodAttachOptions
            {
                Customer = "cus_M3ZkjkHABcPsew",
            };
            var service = new PaymentMethodService();
            service.Attach(paymentMthodId     ,
              options);
            
        }

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = 2000,
                      Currency = "inr",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = "T-shirt",
                      },

                    },
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = "https://example.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [HttpPost("CreateInvoice")]
        public IActionResult CreateInvoice([FromBody] StripePaymentRequest PaymentRequest)
        {
            var options = new ProductCreateOptions { Name = "AWS Learning Test" };
            var service = new ProductService();
            var productDetails = service.Create(options);
            

            var priceCreate = new PriceCreateOptions
            {
                Product = productDetails.Id,//"{{PRODUCT_ID}}"
                UnitAmount = Convert.ToInt64(PaymentRequest.amount),
                Currency = "inr",
            };
            var priceService = new PriceService();
            var price = priceService.Create(priceCreate);

            var customer = new CustomerCreateOptions
            {
                Name = "Jeremy Renner",
                Email = "nekisaini@outlook.com",
                Description = "Book Store Customer",
            };
            var customerService = new CustomerService();
            var customerDetails = customerService.Create(customer);

            var invoice = new InvoiceItemCreateOptions
            {
                Customer = customerDetails.Id, //"{{CUSTOMER_ID}}",
                Price = price.Id //"{{PRICE_ID}}",

            };
            var invoiceItem = new InvoiceItemService();
            var invoiceService = invoiceItem.Create(invoice);

            var invoiceCreate = new InvoiceCreateOptions
            {
                Customer = customerDetails.Id,//"{{CUSTOMER_ID}}",
                CollectionMethod = "send_invoice",
                DaysUntilDue = 30,
            };
            var invCreate = new InvoiceService();
            var createInvoice = invCreate.Create(invoiceCreate);

            var finalize = new InvoiceService();
            var fnlz= finalize.FinalizeInvoice(createInvoice.Id);//("{{INVOICE_ID}}");
            finalize.SendInvoice(createInvoice.Id);
           // var details = service.Get(invoiceService.Id);
            var payInvoiceUrl = fnlz.HostedInvoiceUrl;


            return Ok(payInvoiceUrl);
        }

        [HttpPost("PaymentDetails")]
        public async Task<ActionResult> AddPaymentDetails([FromBody] PaymentDto payment)
        {
            try
            {
                if (payment is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                await _ordersRepository.AddPaymentDetails(payment);
                return Ok();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong inside AddBookToWishlist action: {ex.Message}");
                return StatusCode(500, $"Internal server error:{ex}");
            }
        }
    }
}
