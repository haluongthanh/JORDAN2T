using JORDAN_2T.Infrastructure.Interfaces;
using JORDAN_2T.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JORDAN_2T.Infrastructure.Data
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private MvcMovieContext _context;

        public OrderRepository(MvcMovieContext context) : base(context)
        {
            _context = context;
        }


        public void Update(Order order)
        {
            _context.Orders.Update(order);
        }
        public IEnumerable<Order> GetOrders(int pg)
        {
             IEnumerable<Order> orders;
            var ordersList = _dbSet.Where(p => p.ApplicationUser != null);

            const int pageSize=8;
            if(pg<1)
                pg=1;
                ;

            int recsCount =ordersList.Count();

            var pager=new Pager(recsCount,pg,pageSize);

            int recSkip=(pg-1)*pageSize;
            orders= ordersList.OrderByDescending(p => p.Id).Skip(recSkip).Take(pageSize).ToList();
            return orders;
        }
        
        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _context.Orders.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }
    }
}
