using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();


        // GET: Orders
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult Index()
        {
            return View(context.Orders.ToList());
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult Create(int? idPlace)
        {
            int? playgroundId = idPlace;
            string eventsJson = string.Empty;
            List<int> ids = new List<int>();
            using (var context = new ApplicationDbContext())
            {
                var Reservations = context.Reservations.Where(x => x.Place.Id == idPlace).ToList();
                var events = Reservations.Select(wh => new Event
                {
                    id = wh.Id,
                    start_date = wh.Hour.ToString("MM/dd/yyyy HH:mm"),
                    end_date = wh.EndHour.ToString("MM/dd/yyyy HH:mm"),
                    text = string.Empty
                });
                ids.AddRange(events.Select(wh => wh.id));
                eventsJson = JsonConvert.SerializeObject(events);
            }
            return View(new OrderViewModel { eventsJson = eventsJson, ids = ids, playgroundId=playgroundId });
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin,Customer")]
        public ActionResult Create(IEnumerable<Event> items, int? idPlace)
        {
            Playground playground = context.Playgrounds.FirstOrDefault(i => i.Id == idPlace);
            Order order = new Order() { Reservation = new List<Reservation>() };
            foreach (var item in items)
            {
                DateTime start_date;
                DateTime end_date;
                if (item.read_only)
                    continue;

                if (!DateTime.TryParse(item.start_date, out start_date))
                    continue;
                
                if (!DateTime.TryParse(item.end_date, out end_date))
                    continue;

                TimeSpan orderTime = end_date - start_date;
                for (int i = 0; i < orderTime.TotalHours; i++)
                {
                    order.Reservation.Add(new Reservation
                    {
                        Hour = start_date.AddHours(i),
                        Place = playground
                    });
                }
            }
            order.DateCreated = DateTime.Now;
            var userId = User.Identity.GetUserId();
            order.Owner = context.Users.FirstOrDefault(c => c.Id == userId);
                //System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            context.Orders.Add(order);
            context.SaveChanges();
            //  return View();
            return null;

        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DateCreated")] Order order)
        {
            if (ModelState.IsValid)
            {
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = context.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = context.Orders.Find(id);
            context.Orders.Remove(order);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
