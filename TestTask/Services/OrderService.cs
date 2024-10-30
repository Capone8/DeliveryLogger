using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Core.Interfaces;
using TestTask.Core.Models;
using TestTask.Infrastructure.Data;

namespace TestTask.Services
{
    
    public class OrderService 
    {
        private readonly IOrderRepositoryInterface _orderRepository;
        private readonly ILogger _logger;
        public OrderService(IOrderRepositoryInterface orderRepository, ILogger logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }
        public List<Order> FilterOrders(string CityDistrict, DateTime FirstDeliveryTime)
        {
           
            if (string.IsNullOrWhiteSpace(CityDistrict))
            {
                throw new ArgumentException("City district cannot be null or empty");
            }

            if (FirstDeliveryTime == DateTime.MinValue)
            {
                _logger.Log("ERROR", "Invalid FistDeliveryTime specified");
                throw new ArgumentException("First delivery time cannot be DateTime.MinValue.");
            }
            
            
            _logger.Log("INFO","Starting to filter orders");
            List <Order> allOrders = _orderRepository.GetAllOrders();

            DateTime maxDeliveryTime = FirstDeliveryTime.AddMinutes(30);

            var filteredOrders = allOrders
                .Where(order => order.CityDistrict == CityDistrict&&
                                order.DeliveryTime >= FirstDeliveryTime&&
                                order.DeliveryTime <= maxDeliveryTime)
                .ToList();
           
            

            _logger.Log("INFO", $"Total orders founded: {filteredOrders.Count}");
            return filteredOrders;
        }
    }
}
