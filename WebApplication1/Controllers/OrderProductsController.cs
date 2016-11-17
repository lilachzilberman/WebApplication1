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
    public class OrderProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderProducts
        public ActionResult Index()
        {
            return View(db.OrderProducts.ToList());
        }

        // GET: OrderProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProducts orderProducts = db.OrderProducts.Find(id);
            if (orderProducts == null)
            {
                return HttpNotFound();
            }
            return View(orderProducts);
        }

        // GET: OrderProducts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,Quantity,OrderId")] OrderProducts orderProducts)
        {
            if (ModelState.IsValid)
            {
                db.OrderProducts.Add(orderProducts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderProducts);
        }

        // GET: OrderProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProducts orderProducts = db.OrderProducts.Find(id);
            if (orderProducts == null)
            {
                return HttpNotFound();
            }
            return View(orderProducts);
        }

        // POST: OrderProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,Quantity,OrderId")] OrderProducts orderProducts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderProducts);
        }

        // GET: OrderProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProducts orderProducts = db.OrderProducts.Find(id);
            if (orderProducts == null)
            {
                return HttpNotFound();
            }
            return View(orderProducts);
        }

        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderProducts orderProducts = db.OrderProducts.Find(id);
            db.OrderProducts.Remove(orderProducts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
