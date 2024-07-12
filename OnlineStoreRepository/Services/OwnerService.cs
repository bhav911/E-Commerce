using OnlineStoreModel.Context;
using OnlineStoreModel.CustomModels;
using OnlineStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreRepository.Services
{
    public class OwnerService : IOwnerInterface
    {
        private readonly OnlineStoreEntities db = new OnlineStoreEntities();
        public void RegisterOwner(Owner owner)
        {
            owner = db.Owner.Add(owner);
            OwnerKYC ownerkyc = new OwnerKYC()
            {
                OwnerID = owner.OwnerID
            };
            db.OwnerKYC.Add(ownerkyc);
            db.SaveChanges();
        }
        public Owner AuthenticateOwner(LoginModel credentials)
        {
            Owner status = db.Owner.Where(o => o.email == credentials.Login_email && o.password == credentials.Login_password).FirstOrDefault();
            return status;
        }
        public List<Owner> GetAllShops()
        {
            List<Owner> allShops = db.Owner.ToList();
            return allShops;
        }

        public Owner DoesOwnerExist(string email)
        {
            Owner owner = db.Owner.FirstOrDefault(q => q.email == email);
            return owner;
        }

        public List<OrderDetails> GetReceivedOrders(int ownerID, DateTime? startDate, DateTime? endDate, int? productID)
        {
            if (startDate == null)
            {
                startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddMonths(-1);
            }
            if (endDate == null)
            {
                endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            }

            endDate = endDate.Value.AddDays(1);

            List<OrderDetails> orderList;
            if (productID == null)
            {
                orderList = db.OrderDetails.Where(q => q.Products.OwnerID == ownerID & q.Orders.OrderDate > startDate && q.Orders.OrderDate < endDate).ToList();
            }
            else
            {
                orderList = db.OrderDetails.Where(q => q.Products.OwnerID == ownerID & q.Orders.OrderDate > startDate && q.Orders.OrderDate < endDate && q.ProductID == productID).ToList();
            }

            return orderList;
        }
        public void SaveDocuments(string[] docs, int userID)
        {
            Owner owner = db.Owner.FirstOrDefault(u => u.OwnerID == userID);
            OwnerKYC ownerkyc = owner.OwnerKYC.FirstOrDefault();
            if(docs[0] != null)
                ownerkyc.panCard = docs[0];
            if (docs[1] != null)
                ownerkyc.aadharCard = docs[1];
            if (docs[2] != null)
                ownerkyc.passpostImage = docs[2];
            if (docs[3] != null)
                ownerkyc.shopImage = docs[3];
            db.SaveChanges();
        }

        public DocumentModel GetDocumentPath(int userID)
        {
            OwnerKYC ownerKyc = db.OwnerKYC.FirstOrDefault(u => u.OwnerID == userID);
            DocumentModel docs = new DocumentModel();
            docs.DocPaths[0] = ownerKyc.panCard?? null;
            docs.DocPaths[1] = ownerKyc.aadharCard?? null;
            docs.DocPaths[2] = ownerKyc.passpostImage?? null;
            docs.DocPaths[3] = ownerKyc.shopImage?? null;
            return docs;
        }

        public Owner GetOwner(int ownerID)
        {
            Owner owner = db.Owner.FirstOrDefault(q => q.OwnerID == ownerID);
            return owner;
        }

        public bool UpdateProfile(OwnerModel ownerModel)
        {
            try
            {
                Owner owner = db.Owner.FirstOrDefault(q => q.OwnerID == ownerModel.OwnerID);
                owner.shopname = ownerModel.Shopname;
                owner.CityID = ownerModel.CityID;
                owner.StateID = ownerModel.StateID;
                owner.email = ownerModel.Email;
                owner.Description = ownerModel.Description;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }

        public DashboardModel BuildDashboard(int ownerID, DateTime? startDate, DateTime? endDate)
        {
            DashboardModel dashboardModel = new DashboardModel()
            {
                OwnerID = ownerID
            };

            if(startDate == null)
            {
                startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddMonths(-1);
            }
            if (endDate == null)
            {
                endDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            }

            endDate = endDate.Value.AddDays(1);


            List<OrderDetails> orderDetailList = db.OrderDetails.Where(q => q.Products.OwnerID == ownerID).ToList();
            GetSales((DateTime)startDate, (DateTime)endDate, orderDetailList, dashboardModel);
            GetRevenue((DateTime)startDate, (DateTime)endDate, orderDetailList, dashboardModel);
            dashboardModel.MostSoldProduct = GetMostSold(ownerID, (DateTime)startDate, (DateTime)endDate);
            dashboardModel.MostLikedProduct = GetMostLiked(ownerID);
            return dashboardModel;
        }

        private void GetSales(DateTime startD, DateTime endD, List<OrderDetails> orderDetailList, DashboardModel dashboardModel)
        {
            List<Sales> salesList = new List<Sales>();
            int FilteredSales = 0;
            while (startD < endD)
            {
                int saleCount = (int)orderDetailList.Where(q => q.Orders.OrderDate.ToString().Split(' ')[0] == startD.ToString().Split(' ')[0]).Sum(q => q.Quantity);
                if (saleCount > 0)
                {
                    Sales temp = new Sales(startD, saleCount);
                    FilteredSales += saleCount;
                    salesList.Add(temp);
                }
                startD = startD.AddDays(1);
            }
            dashboardModel.FilteredSales = FilteredSales;
            dashboardModel.Sales = salesList;
        }
        private void GetRevenue(DateTime startD, DateTime endD, List<OrderDetails> orderDetailList, DashboardModel dashboardModel)
        {
            List<Revenue> revenueList = new List<Revenue>();
            decimal FilteredRevenue = 0;
            while (startD < endD)
            {
                decimal revenueSum = (decimal)orderDetailList.Where(q => q.Orders.OrderDate.ToString().Split(' ')[0] == startD.ToString().Split(' ')[0]).Sum(q => q.Quantity * q.unitPrice);
                if (revenueSum > 0)
                {
                    Revenue temp = new Revenue(startD, revenueSum);
                    FilteredRevenue += revenueSum;
                    revenueList.Add(temp);
                }
                startD = startD.AddDays(1);
            }
            dashboardModel.FilteredRevenue = FilteredRevenue;
            dashboardModel.Revenue = revenueList;
        }
        private List<MostSold> GetMostSold(int ownerID, DateTime startD, DateTime endD)
        {
                var query = from od in db.OrderDetails
                                        join o in db.Orders on od.OrderID equals o.OrderID
                                        join p in db.Products on od.ProductID equals p.ProductID
                                        where p.OwnerID == ownerID && o.OrderDate > startD && o.OrderDate < endD
                                        group new { od, p } by new { od.ProductID, p.ProductName } into g
                                        select new
                                        {
                                            ProductID = g.Key.ProductID,
                                            ProductName = g.Key.ProductName,
                                            Sold = g.Count()
                                        } into result
                                        orderby result.Sold descending
                                        select result;
            var orderDetailList = query.Take(5).ToList();


            List<MostSold> mostSoldList = new List<MostSold>();
            
            foreach (var item in orderDetailList)
            {
                MostSold temp = new MostSold((int)item.ProductID, item.ProductName, item.Sold);
                mostSoldList.Add(temp);
            }
            
            return mostSoldList;
        }
        private List<MostLiked> GetMostLiked(int ownerID)
        {
            var query = from od in db.OrderDetails
                        join p in db.Products on od.ProductID equals p.ProductID into productJoin
                        from p in productJoin.DefaultIfEmpty()
                        join pr in db.ProductRating on p.ProductID equals pr.productID into ratingJoin
                        from pr in ratingJoin.DefaultIfEmpty()
                        join o in db.Orders on od.OrderID equals o.OrderID
                        where p.OwnerID == ownerID
                        group new { p, pr } by new { p.ProductID, p.ProductName, pr.avgRating } into g
                        orderby g.Key.avgRating descending
                        select new
                        {
                            ProductID = g.Key.ProductID,
                            ProductName = g.Key.ProductName,
                            Rating = g.Key.avgRating
                        };

            var orderDetailList = query.Take(5).ToList();


            List<MostLiked> mostLikedList = new List<MostLiked>();

            foreach (var item in orderDetailList)
            {
                MostLiked temp = new MostLiked(item.ProductID, item.ProductName, (decimal)item.Rating);
                mostLikedList.Add(temp);
            }

            return mostLikedList;
        }
    }
}
