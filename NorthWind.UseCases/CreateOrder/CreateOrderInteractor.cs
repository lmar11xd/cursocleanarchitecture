using MediatR;
using NorthWind.Entities.Exceptions;
using NorthWind.Entities.Interfaces;
using NorthWind.Entities.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NorthWind.UseCases.CreateOrder
{
    public class CreateOrderInteractor : IRequestHandler<CreateOrderInputPort, int>
    {
        readonly IOrderRepository orderRepository;
        readonly IOrderDetailRepository orderDetailRepository;
        readonly IUnitOfWork unitOfWork;
        public CreateOrderInteractor(
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IUnitOfWork unitOfWork) =>
            (this.orderRepository, this.orderDetailRepository, this.unitOfWork) = 
            (orderRepository, orderDetailRepository, unitOfWork);

        public async Task<int> Handle(CreateOrderInputPort request, CancellationToken cancellationToken)
        {
            Order order = new()
            {
                CustomerId = request.CustomerId,
                OrderDate = DateTime.Now,
                ShipAddress = request.ShipAddress,
                ShipCity = request.ShipCity,
                ShipCountry = request.ShipCountry,
                ShipPostalCode = request.ShipPostalCode,
                ShippingType = Entities.Enums.ShippingType.Road,
                DiscountType = Entities.Enums.DiscountType.Percentage,
                Discount = 10
            };

            orderRepository.Create(order);
            foreach(var item in request.OrderDetails)
            {
                orderDetailRepository.Create(
                    new OrderDetail { 
                        Order = order,
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity
                    }
                );

                try {
                    await unitOfWork.SaveChangesAsync();
                } catch(Exception ex)
                {
                    throw new GeneralException("Error al crear la orden.", ex);
                }
            }

            return order.Id;
        }
    }
}
