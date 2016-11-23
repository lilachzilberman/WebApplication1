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
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            fetchFKs();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,Price,SupplierId,AnimalsId,CategoryId,PicturePath")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(products);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            fetchFKs();
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,SupplierId,AnimalsId,CategoryId,PicturePath")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(products);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        protected void fetchFKs()
        {
            ViewBag.SupplierOptions = db.Suppliers.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.Name
            }).ToArray();

            ViewBag.AnimalsTypes = db.Animals.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.Name
            }).ToArray();

            ViewBag.CategoriesTypes = db.Categories.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.Name
            }).ToArray();
        }

        // GET: Products/Catalog
        public ActionResult Catalog(string AnimalsTypes, string CategoriesTypes, string minPrice, string maxPrice, string Group)
        {
            var products = db.Products.Include(path => path.Supplier);

            if (!String.IsNullOrEmpty(AnimalsTypes))
            {
                products = SearchByAnimal(products, AnimalsTypes);
            }

            if (!String.IsNullOrEmpty(CategoriesTypes))
            {
                products = SearchByCategoryName(products, CategoriesTypes);
            }

            if (!(String.IsNullOrEmpty(minPrice) && String.IsNullOrEmpty(maxPrice)))
            {
                products = SearchByPrice(products, minPrice, maxPrice);
            }

            ViewBag.Group = Group;

            fetchFKsForFilter();
            return View(products.ToList());
        }

        protected void fetchFKsForFilter()
        {
            List<SelectListItem> animals = db.Animals.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.Name
            }).ToList();
            animals.Add(new SelectListItem { Text = "", Value = null, Selected = true });

            ViewBag.AnimalsTypes = animals.ToArray();

            List<SelectListItem> categories = db.Categories.Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.Name
            }).ToList();
            categories.Add(new SelectListItem { Text = "", Value = null, Selected = true });
            ViewBag.CategoriesTypes = categories.ToArray();
        }

        public IQueryable<Products> SearchByAnimal(IQueryable<Products> products, string animalName)
        {
            return products.Where(p => p.AnimalsId.ToString().Equals(animalName));
        }
        public IQueryable<Products> SearchByCategoryName(IQueryable<Products> products, string categoryName)
        {
            return products.Where(p => p.CategoryId.ToString().Equals(categoryName));
        }
        public IQueryable<Products> SearchByPrice(IQueryable<Products> products, string min, string max)
        {
            if (!String.IsNullOrEmpty(min))
            {
                int minInt = int.Parse(min);
                products = products.Where(p => p.Price >= minInt);
            }
            if (!String.IsNullOrEmpty(max))
            {
                int maxInt = int.Parse(max);
                products = products.Where(p => p.Price <= maxInt);
            }

            return products;
        }
    }
}
