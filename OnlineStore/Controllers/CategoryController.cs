using Newtonsoft.Json;
using OnlineStore.Sessions;
using OnlineStoreHelper.Helpers;
using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [CustomAuthorizeHelper]
    public class CategoryController : Controller
    {
        public async Task<JsonResult> GetCategory()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CategoryApi/GetCategory");
            List<CategoryModel> cartList = JsonConvert.DeserializeObject<List<CategoryModel>>(response);
            return Json(cartList, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetSubCategories(int subCategoryID)
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CategoryApi/GetSubCategories?subCategoryID={subCategoryID}");
            List<SubCategoryModel> subCategoryModelList = JsonConvert.DeserializeObject<List<SubCategoryModel>>(response);
            return Json(subCategoryModelList, JsonRequestBehavior.AllowGet);
        }
        
        [CustomCustomerAuthenticateHelper]
        public async Task<ActionResult> Category()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/CategoryApi/Category");
            List<CategoryModel> categoryModelList = JsonConvert.DeserializeObject<List<CategoryModel>>(response);
            return View(categoryModelList);
        }
    }
}