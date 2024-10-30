using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Core.Interfaces;
using TestTask.Infrastructure.Data;
using TestTask.Infrastructure.Logging;
using TestTask.Services;

namespace TestTask.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Укажите путь к файлу данных и логов
            string ordersFilePath = "C:\\Users\\MSI\\source\\repos\\TestTask\\TestTask\\bin\\Debug\\net8.0\\orders.csv";  // путь к файлу заказов
            string logFilePath = "C:\\Users\\MSI\\source\\repos\\TestTask\\TestTask\\bin\\Debug\\net8.0\\log.txt";        // путь к файлу логов
            string cityDistrict = "Downtown";
            DateTime firstDeliveryTime = DateTime.Parse("2023-10-29 08:00:00");
            //Создание репозитория и логгера
            ILogger logger = new FileLogger(logFilePath);
            IOrderRepositoryInterface orderRepository = new OrderFileRepository(ordersFilePath,logger);
            

            // Создаём OrderService с репозиторием и логгером

            OrderService orderService = new OrderService(orderRepository, logger);

            var filteredOrders = orderService.FilterOrders(cityDistrict, firstDeliveryTime);

            Console.WriteLine("Filtered orders:");
            foreach (var order in filteredOrders)
            {
                Console.WriteLine($"OrderId: {order.OrderId}, Weight: {order.Weight}, CityDistrict{order.CityDistrict}, DeliveryTime:{order.DeliveryTime}");
            }
            logger.Log("Success", "Filtering is done");
        }

    }
}
