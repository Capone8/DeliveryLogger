using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Core.Models;

namespace TestTask.Core.Interfaces
{
    public interface IOrderRepositoryInterface
    {
        List<Order> GetAllOrders();
    }
}
