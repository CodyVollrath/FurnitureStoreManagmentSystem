using FurnitureStoreManagmentSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStoreManagmentSystem.ViewModel
{
    public class AdminPortalViewModel
    {

        public string Command { get; set; } = "";

        public string Output { get; set; } = "";

        public AdminPortalViewModel() 
        {

        }

        public void SendCommand() 
        {
            this.Output = AdminDal.AdminCommand(this.Command);
        }
    }
}
