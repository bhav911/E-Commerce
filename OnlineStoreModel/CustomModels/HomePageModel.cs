using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class HomePageModel
    {
        public List<CategoryModel> CategoryList { get; set; }

        public string[] ImagePath = { "electronic.jpg", "fashion.jpg", "kitchen.jpg", "beauty.jpg", "sports.jpg", "toys.jpg", "books.jpg", "auto.jpg", "pet.jpg" };
    }
}
