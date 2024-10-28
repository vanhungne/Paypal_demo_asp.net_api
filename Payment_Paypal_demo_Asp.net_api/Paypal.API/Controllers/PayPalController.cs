using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using PayPal.Service.Interfaces;
using PayPal.Service.Service;

namespace Paypal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IPaypalService _payPalService;
        private readonly IOrderService _orderService;
        private readonly ILogger<PayPalController> _logger;
        public PayPalController(
                                IPaypalService payPalService,
                                IOrderService orderService,
                                ILogger<PayPalController> logger)
        {
            _payPalService = payPalService;
            _orderService = orderService;
            _logger = logger;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
            {
                return BadRequest("Order number is required.");
            }

            try
            {
                var order = await _orderService.GetOrderByNumberAsync(orderNumber);
                if (order == null)
                {
                    return NotFound($"Order {orderNumber} not found");
                }

                var approvalUrl = await _payPalService.CreatePaymentAsync(order.TotalAmount, orderNumber);
                return Ok(new { approvalUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating PayPal payment");
                return StatusCode(500, "An error occurred while processing your request");
            }
        }


        [HttpGet("capture-payment")]
        public async Task<IActionResult> CapturePayment([FromQuery] string token, [FromQuery] string orderNumber)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(orderNumber))
            {
                return BadRequest("Token and orderNumber are required.");
            }

            try
            {
                var captureResult = await _payPalService.CapturePaymentAsync(token);
                if (captureResult.Status == "COMPLETED")
                {
                    await _orderService.ProcessPaymentAsync(orderNumber, token);
                    var orderDetails = await _orderService.GetOrderByNumberAsync(orderNumber);
                    return Ok(new { status = "Success", orderId = captureResult.OrderId ,orderNumber,Amount = orderDetails.TotalAmount});
                }
                else
                {
                    return Ok(new { status = "pending", orderId = captureResult.OrderId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error capturing PayPal payment");
                return StatusCode(500, "An error occurred while processing the payment");
            }
        }


        [HttpGet("cancel-payment")]
        public IActionResult CancelPayment([FromQuery] string orderNumber)
        {
            return RedirectToPage("/OrderCancelled", new { orderNumber });
        }
    }
}
