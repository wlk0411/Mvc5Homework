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
    public class CustomerController : Controller
    {
        private 客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        // GET: Customer
        public ActionResult Index()
        {
            return View(repo.All().ToList());
        }

        // GET: Customer/Details/5
        public ActionResult Details( int? id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = repo.Where(x => x.Id == id.Value).FirstOrDefault();
            客戶資料 客戶資料 = repo.GetById(id);
            if( 客戶資料 == null )
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( [Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,IsDelete")] 客戶資料 客戶資料 )
        {
            if( ModelState.IsValid )
            {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit( int? id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = repo.Where(x => x.Id == id.Value).FirstOrDefault();
            客戶資料 客戶資料 = repo.GetById(id);
            if( 客戶資料 == null )
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customer/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( [Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,IsDelete")] 客戶資料 客戶資料 )
        {
            if( ModelState.IsValid )
            {
                repo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete( int? id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = repo.Where(x => x.Id == id.Value).FirstOrDefault();
            客戶資料 客戶資料 = repo.GetById(id);
            if( 客戶資料 == null )
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed( int id )
        {
            //客戶資料 客戶資料 = repo.Where(x => x.Id == id).FirstOrDefault();
            //repo.Delete(客戶資料);
            //repo.UnitOfWork.Commit();
            repo.DeleteById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}