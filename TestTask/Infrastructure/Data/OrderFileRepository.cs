using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Core.Interfaces;
using CsvHelper;
using TestTask.Core.Models;
using System.Globalization;


namespace TestTask.Infrastructure.Data
{
    public class OrderFileRepository : IOrderRepositoryInterface
    {
        private readonly string _filePath;
        private readonly ILogger _logger;
        public OrderFileRepository(string filePath, ILogger logger)
        {
            _filePath = filePath;
            _logger = logger;
        }

        public List<Order> GetAllOrders()  //Метод загрузит данные из файла и вернёт их в виде списка объектов Order.
        {
            var orders = new List<Order>();
            try
            {
                try
                {

                    using (var reader = new StreamReader(_filePath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    return csv.GetRecords<Order>().ToList();
                }
                catch (FormatException ex)
                {
                    _logger.Log("ERROR", "The file format is incorrect");
                    Console.WriteLine("Ошибка! Неверный формат файла");
                }

            }
            catch (FileNotFoundException ex)
            {
                _logger.Log("ERROR", "The file was not found");
                Console.WriteLine("Ошибка! Файл не был найден");
            }
           
            catch (Exception ex)
            {
                _logger.Log("ERROR",$"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Неизвестная ошибка! Смотреть логи");
            }

            return orders;
        }

        public void WriteFilteredOrders(List<Order> orders)
        {
            try
            {
                using var writer = new StreamWriter(_filePath);
                {
                    foreach (var order in orders)
                    {
                        writer.WriteLine($"Order ID: {order.OrderId}, Weight: {order.Weight}, District: {order.CityDistrict}, Delivery Time: {order.DeliveryTime}");
                    }
                }
            }
            catch(IOException ex)
            {
                _logger.Log("ERROR", "Failed to write filtered orders to the file");
                Console.WriteLine("Ошибка! Не удалось записать отфильтрованные заказы в файл");
            }
        }
    }
}
