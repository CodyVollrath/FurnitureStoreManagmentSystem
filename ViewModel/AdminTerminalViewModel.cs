using FurnitureStoreManagmentSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStoreManagmentSystem.ViewModel
{
    public class AdminTerminalViewModel
    {

        public string Command { get; set; } = "";

        public string Output { get; set; } = "";

        public AdminTerminalViewModel() 
        {

        }

        public void SendCommand() 
        {
            this.Output = AdminDal.AdminCommand(this.Command);
        }
    }
}
