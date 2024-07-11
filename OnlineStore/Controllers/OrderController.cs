using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public class OrderController : Controller
    {

        [CustomCustomerAuthenticateHelper]
        public async Task<ActionResult> GetOrdersPlaced()
        {
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/OrderApi/GetOrdersPlaced?customerID={UserSession.UserID}");
            List<OrderHistoryModel> orderHistoryList = JsonConvert.DeserializeObject<List<OrderHistoryModel>>(response);
            return View(orderHistoryList);
        }

        [HttpPost]
        [CustomCustomerAuthenticateHelper]
        public async Task<ActionResult> BuyProduct(OrderModel order)
        {
            order.CustomerID = UserSession.UserID;
            string response = await WebApiHelper.WebApiHelper.HttpPostResponseRequest($"api/OrderApi/BuyProduct", JsonConvert.SerializeObject(order));
            bool status = JsonConvert.DeserializeObject<bool>(response);
            TempData["success"] = "Order Placed Successfully";
            return RedirectToAction("GetOrdersPlaced");
        }

        [CustomOwnerAuthentucateHelper]
        public async Task<ActionResult> GetRecievedOrders(DateTime? startDate, DateTime? endDate, int? productID)
        {
            if(startDate != null && endDate != null && startDate > endDate || startDate > DateTime.Today || endDate > DateTime.Today)
            {
                TempData["error"] = "Please select appropriate date";
                return RedirectToAction("GetRecievedOrders");
            }
            string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/OwnerApi/GetRecievedOrders?ownerID={UserSession.UserID}&startDate={startDate}&endDate={endDate}&productID={productID}");
            List<OrdersReceivedModel> ordersReceivedModels = JsonConvert.DeserializeObject<List<OrdersReceivedModel>>(response);
            response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/ProductApi/GetMyProducts?ownerID={UserSession.UserID}");
            List<ProductModel> productList = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            FilterOrderModel filterOrderModel = new FilterOrderModel()
            {
                OrderList = ordersReceivedModels,
                ProductList = productList
            };
            TempData["startDate"] = startDate;
            TempData["endDate"] = endDate;
            TempData["productID"] = productID;
            return View("../Owner/GetRecievedOrders", filterOrderModel);
        }

        public async Task<ActionResult> GeneratePDF()
        {
            // Create a new PDF document
            Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                    pdfDoc.Open();

                    Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                    PdfPCell cell;

                    PdfPTable table = new PdfPTable(4);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 3f, 1f, 3f, 1f });
                    cell = new PdfPCell(new Phrase("Product", boldFont));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Quantity", boldFont));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Order Placer", boldFont));
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Order Date", boldFont));
                    table.AddCell(cell);

                    string response = await WebApiHelper.WebApiHelper.HttpGetResponseRequest($"api/OwnerApi/GetRecievedOrders?ownerID={UserSession.UserID}&startDate={TempData["startDate"]}&endDate={TempData["endDate"]}&productID={TempData["productID"]}");
                    List<OrdersReceivedModel> ordersReceivedModels = JsonConvert.DeserializeObject<List<OrdersReceivedModel>>(response);


                    foreach (OrdersReceivedModel order in ordersReceivedModels)
                    {
                        table.AddCell(order.ProductName.ToString());
                        table.AddCell(order.ProductQuantity.ToString());
                        table.AddCell(order.UserEmail.ToString());
                        table.AddCell(order.OrderDate.ToString().Split(' ')[0]);
                    }

                    pdfDoc.Add(table);
                    pdfDoc.Close();

                    byte[] bytes = memoryStream.ToArray();
                    memoryStream.Close();
                    return File(bytes, "application/pdf", "Order_Report.pdf");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return null;
                // Handle exception
            }
        }
    }
}