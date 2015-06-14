using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CEvery.Models;
using PagedList;
using System.Globalization;

namespace CEvery.Controllers
{
    [Authorize]
    public class SubscriptionController : Controller
    {
        private SubscriptionDBContext db = new SubscriptionDBContext();

        // GET: /Subscription/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, string searchStringPin, string searchStringDistrict, int? page, string typeSearch)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var subscription = from s in db.Subscription
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                subscription = subscription.Where(l => l.CustomerName.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(typeSearch))
            {
                subscription = subscription.Where(l => l.Type.ToUpper().Contains(typeSearch.ToUpper()));
            }

            if (!String.IsNullOrEmpty(searchStringPin))
            {
                subscription = subscription.Where(l => l.Pin.ToUpper().Contains(searchStringPin.ToUpper()));
            }

            if (!String.IsNullOrEmpty(searchStringDistrict))
            {
                subscription = subscription.Where(l => l.City.ToUpper().Contains(searchStringDistrict.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    subscription = subscription.OrderByDescending(l => l.CustomerName);
                    break;
                default:
                    subscription = subscription.OrderByDescending(l => l.ModifiedDate);
                    break;
            }

            int pageSize = 20;
            int pageNumber = (page ?? 1);

            ViewBag.AllModelValue = db.Subscription.ToList();

            return View(subscription.ToPagedList(pageNumber, pageSize));

            //return View(db.Subscription.ToList());
        }

        // GET: /Subscription/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscription.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // GET: /Subscription/Create
        public ActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //
            SelectListItem s = new SelectListItem();
            s.Text = "Life";
            s.Value = "Life";
            items.Add(s);
            SelectListItem s1 = new SelectListItem();
            s1.Text = "Year";
            s1.Value = "Year";
            items.Add(s1);
            SelectListItem s2 = new SelectListItem();
            s2.Text = "Vinayakar";
            s2.Value = "Vinayakar";
            items.Add(s2);
            //
            ViewBag.TypeList = items;


            return View();
        }

        // POST: /Subscription/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="SNo,CreatedDate,SubscriptionNo,CustomerName,Type,Address1,Address2,City,Pin,ModifiedDate,Comments,DueDate,IsInvalid,Phone,Address3")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Subscription.Add(subscription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subscription);
        }

        // GET: /Subscription/Edit/5
        public ActionResult Edit(int? id)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            //
            SelectListItem s = new SelectListItem();
            s.Text = "Life";
            s.Value = "Life";
            items.Add(s);
            SelectListItem s1 = new SelectListItem();
            s1.Text = "Year";
            s1.Value = "Year";
            items.Add(s1);
            SelectListItem s2 = new SelectListItem();
            s2.Text = "Vinayakar";
            s2.Value = "Vinayakar";
            items.Add(s2);
            //
            ViewBag.TypeList = items;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscription.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: /Subscription/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="SNo,CreatedDate,SubscriptionNo,CustomerName,Type,Address1,Address2,City,Pin,ModifiedDate,Comments,DueDate,IsInvalid,Phone,Address3")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subscription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subscription);
        }

        // GET: /Subscription/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscription subscription = db.Subscription.Find(id);
            if (subscription == null)
            {
                return HttpNotFound();
            }
            return View(subscription);
        }

        // POST: /Subscription/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscription subscription = db.Subscription.Find(id);
            db.Subscription.Remove(subscription);
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
