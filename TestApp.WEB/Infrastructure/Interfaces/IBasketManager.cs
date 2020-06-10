using System;
using TestApp.Domain.Models;

namespace TestApp.WEB.Infrastructure.Interfaces
{
    public interface IBasketManager
    {
        Order GetBasket();

        void AddOrderDetails(Guid productId, short quantity);

        void ClearBasket();

        void RemoveOrderDetails(Guid productId);
    }
}
