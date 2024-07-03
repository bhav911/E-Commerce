using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class DashboardModel
    {
        public int OwnerID { get; set; }
        public int FilteredSales { get; set; }
        public decimal FilteredRevenue { get; set; }
        public List<Sales> Sales { get; set; }
        public List<Revenue> Revenue { get; set; }
        public List<MostSold> MostSoldProduct { get; set; }
        public List<MostLiked> MostLikedProduct { get; set; }
        public List<MostInteracted> MostInteractedProduct { get; set; }

    }

    public class Sales
    {
        public DateTime Date { get; set; }
        public int SaleCount { get; set; }
        public Sales(DateTime date, int saleCount)
        {
            Date = date;
            SaleCount = saleCount;
        }
    }
    public class Revenue
    {
        public DateTime Date { get; set; }
        public decimal RevenueSum { get; set; }
        public Revenue(DateTime date, decimal revenueSum)
        {
            Date = date;
            RevenueSum = revenueSum;
        }
    }
    public class MostSold
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Sold { get; set; }
        public MostSold(int productID, string productName, int soldQ)
        {
            ProductID = productID;
            ProductName = productName;
            Sold = soldQ;
        }
    }
    public class MostLiked
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Liked { get; set; }
        public MostLiked(int productID, string productName, decimal liked)
        {
            ProductID = productID;
            ProductName = productName;
            Liked = liked;
        }
    }
    public class MostInteracted
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Interaction { get; set; }
        public MostInteracted(int productID, string productName, int interaction)
        {
            ProductID = productID;
            ProductName = productName;
            Interaction = interaction;
        }
    }
}
