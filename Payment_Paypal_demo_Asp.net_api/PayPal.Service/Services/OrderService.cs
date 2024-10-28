using Microsoft.Extensions.Logging;
using PayPal.Data;
using PayPal.Data.Entities;
using PayPal.Data.Enum;
using PayPal.Data.Model;
using PayPal.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal.Service.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaypalService _payPalService;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IUnitOfWork unitOfWork,IPaypalService payPalService,
                            ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _payPalService = payPalService;
            _logger = logger;
        }
        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto, int userId)
        {
            try
            {
                var order = new Orders                {
                    UserId = userId,
                    OrderNumber = GenerateOrderNumber(),
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    TotalAmount = orderDto.TotalAmount,
                    OrderItems = orderDto.Items.Select(item => new OrderItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        Subtotal = item.Quantity * item.UnitPrice
                    }).ToList()
                };

                await _unitOfWork.OrderRepository.AddAsync(order);
                await _unitOfWork.SaveChangesAsync();

                return orderDto;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error creating order");
                throw;
            }
        }

        public async Task<OrderDto> GetOrderByNumberAsync(string orderNumber)
        {
            var order = await _unitOfWork.OrderRepository.GetByOrderNumberAsync(orderNumber);
            return MapToOrderDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetUserOrdersAsync(userId);
                return orders.Select(MapToOrderDto);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Error getting orders for user {userId}");
                throw;
            }
        }

        public async Task ProcessPaymentAsync(string orderNumber, string paypalToken)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByOrderNumberAsync(orderNumber);
                if (order == null)
                {
                    _logger.LogError($"Order {orderNumber} not found during payment processing");
                    throw new InvalidOperationException($"Order {orderNumber} not found");
                }

                // Capture payment from PayPal
                var (status, paypalOrderId) = await _payPalService.CapturePaymentAsync(paypalToken);

                // Update order status based on PayPal status
                order.Status = MapPayPalStatusToOrderStatus(status);

                // Create payment record
                var payment = new Payment
                {
                    OrderId = order.Id,
                    CreatedDate = DateTime.UtcNow,
                    Amount = order.TotalAmount,
                    PaymentMethod = PaymentMethod.PayPal,
                    PaypalOrderId = paypalOrderId,
                    Status = MapPayPalStatusToPaymentStatus(status)
                };

                if (status.Equals("COMPLETED", StringComparison.OrdinalIgnoreCase))
                {
                    payment.CompletedDate = DateTime.UtcNow;
                    order.Status = OrderStatus.Processing;
                }

                // Explicitly attach the order to the context if it's not being tracked
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.PaymentRepository.AddAsync(payment);
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (System.Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Error processing payment for order {orderNumber}");
                throw;
            }
        }

        public async Task UpdateOrderStatusAsync(string orderNumber, OrderStatus status)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByOrderNumberAsync(orderNumber);
                if (order == null)
                {
                    _logger.LogError($"Order {orderNumber} not found during status update");
                    throw new InvalidOperationException($"Order {orderNumber} not found");
                }

                order.Status = status;
                order.OrderDate = DateTime.UtcNow;

                await _unitOfWork.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Error updating status for order {orderNumber}");
                throw;
            }
        }
        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N").Substring(0, 8)}".ToUpper();
        }

        private OrderDto MapToOrderDto(Orders order)
        {
            if (order == null) return null;

            return new OrderDto
            {
                OrderNumber = order.OrderNumber,
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(item => new OrderItemDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };
        }
        private PaymentStatus MapPayPalStatusToPaymentStatus(string paypalStatus)
        {
            return paypalStatus.ToUpper() switch
            {
                "COMPLETED" => PaymentStatus.Completed,
                "APPROVED" => PaymentStatus.Processing,
                "FAILED" => PaymentStatus.Failed,
                _ => PaymentStatus.Pending
            };
        }
        private OrderStatus MapPayPalStatusToOrderStatus(string paypalStatus)
        {
            return paypalStatus.ToUpper() switch
            {
                "COMPLETED" => OrderStatus.Processing,
                "APPROVED" => OrderStatus.Processing,
                "FAILED" => OrderStatus.Cancelled,
                _ => OrderStatus.Pending
            };
        }
    }
}
