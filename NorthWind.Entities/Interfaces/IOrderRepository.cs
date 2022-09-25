using NorthWind.Entities.POCOs;
using NorthWind.Entities.Specifications;
using System.Collections.Generic;

namespace NorthWind.Entities.Interfaces
{
    public interface IOrderRepository
    {
        void Create(Order order);
        IEnumerable<Order> GetOrderBySpecification(Specification<Order> specification);//Patron Especificacion
    }
}
