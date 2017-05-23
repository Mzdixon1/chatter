using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chatter.Models;
using Newtonsoft.Json;
namespace Chatter.Controllers
{
    public class chatsController : Controller
    {
        private ChatterEntities1 db = new ChatterEntities1();

        // GET: chats
        public ActionResult Index()
        {
            var chats = db.chats.Include(c => c.AspNetUser);
            return View(chats.ToList());
        }
        public JsonResult TestJson()
        {
            string jsonTest = "{ \"firstName\": \"Melanie\",\"lastName\": \"McGee\", \"children\": [{\"firstName\": \"Mira\", \"age\": 13 },{\"firstName\": \"Ethan\", \"age\": null }] }";

                return Json(jsonTest, JsonRequestBehavior.AllowGet);
            }


        // GET: chats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chat chat = db.chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // GET: chats/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: chats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,userID,message,Timestamp")] chat chat)
        {
            if (ModelState.IsValid)
            {
                db.chats.Add(chat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", chat.userID);
            return View(chat);
        }

        // GET: chats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chat chat = db.chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", chat.userID);
            return View(chat);
        }

        // POST: chats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,userID,message,Timestamp")] chat chat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", chat.userID);
            return View(chat);
        }

        // GET: chats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            chat chat = db.chats.Find(id);
            if (chat == null)
            {
                return HttpNotFound();
            }
            return View(chat);
        }

        // POST: chats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            chat chat = db.chats.Find(id);
            db.chats.Remove(chat);
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
