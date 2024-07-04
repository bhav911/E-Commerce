using Newtonsoft.Json;
using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
    [CustomOwnerAuthentucateHelper]
    public class OwnerController : Controller
    {
        public ActionResult Dashboard()
        {
            return View();
        }

        public async Task<JsonResult> GetDashboardData(DateTime? startDate, DateTime? endDate)
        {
            if (startDate > endDate || startDate > DateTime.Today || endDate > DateTime.Today)
            {
                return Json(new DashboardModel(), JsonRequestBehavior.AllowGet);
            }
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/OwnerApi/GetDashboard?ownerID={UserSession.UserID}&startDate={startDate}&endDate={endDate}");
            DashboardModel dashboardModel = JsonConvert.DeserializeObject<DashboardModel>(response);
            return Json(dashboardModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Unauthorize(string role)
        {
            ViewBag.role = role;
            return View();
        }

        public async Task<ActionResult> UploadDocuments()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/OwnerApi/UploadDocuments?ownerID={UserSession.UserID}");
            DocumentModel docs = JsonConvert.DeserializeObject<DocumentModel>(response);
            return View(docs);
        }
        private string GetUniqueFileName(HttpPostedFileBase file)
        {
            if (file == null)
                return null;
            string ext = Path.GetExtension(file.FileName);
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            file.SaveAs(HttpContext.Server.MapPath("~/Content/ProductImages/") + uniqueFileName);
            if (ext.Equals(".pdf"))
            {
                file.SaveAs(HttpContext.Server.MapPath("~/Content/KYC/PDFs/") + uniqueFileName);
            }
            else
            {
                file.SaveAs(HttpContext.Server.MapPath("~/Content/KYC/IMGs/") + uniqueFileName);
            }
            return uniqueFileName;
        }

        [HttpPost]
        public async Task<ActionResult> UploadDocuments(DocumentModel docs)
        {
            if (ModelState.IsValid)
            {
                docs.DocPaths[0] = GetUniqueFileName(docs.PanCard);
                docs.DocPaths[1] = GetUniqueFileName(docs.AadharCard);
                docs.DocPaths[2] = GetUniqueFileName(docs.PassportImage);
                docs.DocPaths[3] = GetUniqueFileName(docs.ShopImage);
                string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/OwnerApi/UploadDocuments?ownerID={UserSession.UserID}", JsonConvert.SerializeObject(docs.DocPaths));
                TempData["success"] = "Documents Uploaded Successfully";
                return RedirectToAction("UploadDocuments");
            }
            return RedirectToAction("UploadDocuments");
        }

        public async Task<ActionResult> GetAccount()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/OwnerApi/AccountDetails?ownerID={UserSession.UserID}");
            OwnerModel OwnerDetails = JsonConvert.DeserializeObject<OwnerModel>(response);
            return View(OwnerDetails);
        }

        [HttpPost]
        public ActionResult EditDetails(OwnerModel ownerModel)
        {
            return View(ownerModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditAccount(OwnerModel ownerModel)
        {
            if (ModelState.IsValid)
            {
                ownerModel.OwnerID = UserSession.UserID;
                string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/OwnerApi/UpdateProfile", JsonConvert.SerializeObject(ownerModel));
                return RedirectToAction("GetAccount");
            }
            return View(ownerModel);
        }
    }
}