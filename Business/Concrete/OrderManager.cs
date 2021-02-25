using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(),"Siparişler listelendi");
        }

        public IDataResult<Order> GetById(int orderId)
        {
            return new SuccessDataResult<Order>(_orderDal.Get(p=>p.OrderID==orderId), "Siparişler listelendi");

        }

        public IResult Add(Order order)
        {
            _orderDal.Add(order);
            return new SuccessResult("Sipariş eklendi");
        }

        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult("Sipariş güncellendi");
        }

        public IResult Delete(int orderId)
        {
            Order order = new Order() {OrderID = orderId};

            _orderDal.Delete(order);
            return new SuccessResult("Sipariş güncellendi");
        }
    }
}
