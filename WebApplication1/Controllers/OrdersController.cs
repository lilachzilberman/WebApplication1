using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrdersView
        [Authorize(Roles = "Admin")]
        public ActionResult OrdersView(string UserEmail)
        {
            var results = (from prodOr in db.OrderProducts 
                               join order in db.Orders on prodOr.OrderId equals order.Id
                               join prod in db.Products on prodOr.ProductId equals prod.Id
                               join user in db.Users on order.UserId equals user.Id
                               select new OrdersViewModel {Email = user.Email,
                                   OrderId = order.Id,
                                   OrderDate = order.CreationTime,
                                   ProductName = prod.Name,
                                   Quantity = prodOr.Quantity});

            if (!String.IsNullOrEmpty(UserEmail))
            {
                results = SearchByUser(results, UserEmail);
            }
            return View(results);
        }

        public IQueryable<OrdersViewModel> SearchByUser(IQueryable<OrdersViewModel> results, string user)
        {
            return results.Where(p => p.Email.ToString().Equals(user));
        }
    }
}
