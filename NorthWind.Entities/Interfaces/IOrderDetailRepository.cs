using NorthWind.Entities.POCOs;

namespace NorthWind.Entities.Interfaces
{
    public interface IOrderDetailRepository
    {
        void Create(OrderDetail orderDetail);
    }
}
