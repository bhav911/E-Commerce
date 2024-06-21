using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public partial class AdminModel
    {
        public int adminID { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
