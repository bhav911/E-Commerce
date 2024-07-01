using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreModel.CustomModels
{
    public partial class AdminModel
    {
        public int AdminID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
