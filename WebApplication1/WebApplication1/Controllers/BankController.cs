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
    public class BankController : BaseController
    {
        private 客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();
        private 客戶資料Repository repoCustomer = RepositoryHelper.Get客戶資料Repository();

        // GET: Bank
        public ActionResult Index()
        {
            var 客戶銀行資訊 = repo.All().Include(客 => 客.客戶資料);
            return View(客戶銀行資訊.ToList());
        }

        // GET: Bank/Details/5
        public ActionResult Details( int? id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶銀行資訊 客戶銀行資訊 = repo.Where(x => x.Id == id.Value).FirstOrDefault();
            客戶銀行資訊 客戶銀行資訊 = repo.GetById(id);
            if( 客戶銀行資訊 == null )
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: Bank/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Bank/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( [Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,IsDelete")] 客戶銀行資訊 客戶銀行資訊 )
        {
            if( ModelState.IsValid )
            {
                repo.Add(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: Bank/Edit/5
        public ActionResult Edit( int? id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶銀行資訊 客戶銀行資訊 = repo.Where(x => x.Id == id.Value).FirstOrDefault();
            客戶銀行資訊 客戶銀行資訊 = repo.GetById(id);
            if( 客戶銀行資訊 == null )
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: Bank/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            int? id,
            FormCollection form
            //[Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,IsDelete")] 客戶銀行資訊 客戶銀行資訊
            )
        {
            //if( ModelState.IsValid )
            //{
            //    repo.UnitOfWork.Context.Entry(客戶銀行資訊).State = EntityState.Modified;
            //    repo.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}

            var 客戶銀行資訊 = repo.GetById(id);

            if( TryUpdateModel<I客戶銀行資訊更新>(客戶銀行資訊) )
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repoCustomer.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: Bank/Delete/5
        public ActionResult Delete( int? id )
        {
            if( id == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶銀行資訊 客戶銀行資訊 = repo.Where(x => x.Id == id.Value).FirstOrDefault();
            客戶銀行資訊 客戶銀行資訊 = repo.GetById(id);
            if( 客戶銀行資訊 == null )
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: Bank/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed( int id )
        {
            //客戶銀行資訊 客戶銀行資訊 = repo.Where(x => x.Id == id).FirstOrDefault();
            //repo.Delete(客戶銀行資訊);
            //repo.UnitOfWork.Commit();
            repo.DeleteById(id);
            repo.UnitOfWork.Commit();
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