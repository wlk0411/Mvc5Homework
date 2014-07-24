using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;
using System.Text;
using OfficeOpenXml;
using AutoMapper;

namespace WebApplication1.Controllers
{
    public class CustomerController : BaseController
    {
        private 客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();




        public ActionResult ExportXls()
        {
            var 客戶資料 = repo.All().ToList();


            //版本1
            StringBuilder sb = new StringBuilder();

            foreach( var item in 客戶資料 )
            {
                sb.AppendLine(string.Join(",", new string[] { item.Id.ToString(),
                    item.客戶名稱, item.地址, item.傳真, item.電話, item.Email, item.統一編號 }));
            }

            byte[] data = Encoding.GetEncoding("big5").GetBytes(sb.ToString());
            var fileName = string.Format("{0}_客戶資料匯出.xlsx", DateTime.Now.ToString("yyyyMMdd"));


            return File(data, "application/vnd.ms-excel", fileName);



            ////版本2
            //ExcelPackage ep = new ExcelPackage();
            //ExcelWorksheet sheet1 = ep.Workbook.Worksheets.Add("客戶資料");

            //foreach( var item in 客戶資料 )
            //{
            //    sheet1.Cells[客戶資料.IndexOf(item) + 1, 1].Value = item.Id;
            //    sheet1.Cells[客戶資料.IndexOf(item) + 1, 2].Value = item.客戶名稱;
            //    sheet1.Cells[客戶資料.IndexOf(item) + 1, 3].Value = item.地址;
            //    sheet1.Cells[客戶資料.IndexOf(item) + 1, 4].Value = item.傳真;
            //    sheet1.Cells[客戶資料.IndexOf(item) + 1, 5].Value = item.電話;
            //    sheet1.Cells[客戶資料.IndexOf(item) + 1, 6].Value = item.Email;
            //    sheet1.Cells[客戶資料.IndexOf(item) + 1, 6].Value = item.統一編號;
            //}

            //byte[] data = ep.GetAsByteArray();
            //var fileName = string.Format("{0}_客戶資料匯出.xlsx", DateTime.Now.ToString("yyyyMMdd"));


            //return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);







            //50+70 分鐘
            //todo：匯出 Excel
            //實作匯出資料功能，可將「客戶資料」匯出，用 FileResult 輸出檔案，輸出格式不拘 (XLS, CSV, ...)，
            //下載檔名規則："YYYYMMDD_客戶資料匯出.xlsx"


            //using( MemoryStream ms = new MemoryStream() )
            //using( StreamWriter bw = new StreamWriter(ms) )
            //{
            //    foreach( var item in 客戶資料 )
            //    {
            //        bw.Write(string.Join(", ", new string[]
            //        {
            //            item.Id.ToString(), item.客戶名稱, item.地址, item.傳真
            //        }));
            //    }

            //    return File(bw.BaseStream as Stream, "application/vnd.ms-excel");
            //}
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(
            int? id,
            IList<客戶聯絡人> contact,
            IList<客戶銀行資訊> bank )
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



            var repoContact = RepositoryHelper.Get客戶聯絡人Repository(repo.UnitOfWork);
            var repoBank = RepositoryHelper.Get客戶銀行資訊Repository(repo.UnitOfWork);

            foreach( var item in contact.Where(x => x.IsDelete) )
            {
                repoContact.DeleteById(item.Id);
            }

            foreach( var item in bank.Where(x => x.IsDelete) )
            {
                repoBank.DeleteById(item.Id);
            }

            repo.UnitOfWork.Commit();



            return RedirectToAction("Details", new { id = id });
        }








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
        public ActionResult Edit(
            int? id,
            FormCollection form,
            IList<客戶聯絡人> contact,
            IList<客戶銀行資訊> bank
            //[Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,IsDelete")] 客戶資料 客戶資料
            )
        {
            //if( ModelState.IsValid )
            //{
            //    repo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
            //    repo.UnitOfWork.Commit();
            //    return RedirectToAction("Index");
            //}


            var 客戶資料 = repo.GetById(id);

            if( TryUpdateModel<I客戶資料更新>(客戶資料) && ModelState.IsValid )
            {
                var repoContact = RepositoryHelper.Get客戶聯絡人Repository(repo.UnitOfWork);
                var repoBank = RepositoryHelper.Get客戶銀行資訊Repository(repo.UnitOfWork);


                foreach( var item in contact )
                {
                    var 客戶聯絡人 = repoContact.GetById(item.Id);
                    Mapper.DynamicMap<I客戶聯絡人更新, 客戶聯絡人>(item, 客戶聯絡人);
                }

                foreach( var item in bank )
                {
                    var 客戶銀行資訊 = repoBank.GetById(item.Id);
                    Mapper.DynamicMap<I客戶銀行資訊更新, 客戶銀行資訊>(item, 客戶銀行資訊);
                }


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