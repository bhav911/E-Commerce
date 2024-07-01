using OnlineStoreModel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public class EditProductModel
    {
        public Products Product { get; set; }
        public string AggregatedImagePathToAdd { get; set; }
        public string[] ImageFileToDelete { get; set; }
        public int ImgID { get; set; }
    }
}
