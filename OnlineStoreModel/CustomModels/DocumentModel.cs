using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineStoreModel.CustomModels
{
    public class DocumentModel
    {
        public HttpPostedFileBase PanCard { get; set; }
        public HttpPostedFileBase AadharCard { get; set; }
        public HttpPostedFileBase PassportImage { get; set; }
        public HttpPostedFileBase ShopImage { get; set; }   
        public string[] DocPaths { get; set; }
        public DocumentModel()
        {
            DocPaths = new string[4];
        }
    }
}
