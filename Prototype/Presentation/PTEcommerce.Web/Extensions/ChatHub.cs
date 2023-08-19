using Framework.EF;
using marketplace;
using Microsoft.AspNet.SignalR;
using PTEcommerce.Web.Models;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace PTEcommerce.Web.Extensions
{
    public class ChatHub : Hub
    {
        //public void SendMessage(MemberSession userInfor, string message)
        //{
        //    Clients.All.broadcastMessage(userInfor.Nick, userInfor.VID, userInfor.Avatar, DateTime.Now.ToString("HH:mm"), message, 1);
        //}
        //public void SendImage(MemberSession userInfor, string image)
        //{
        //    Clients.All.broadcastMessage(userInfor.Nick, userInfor.VID, userInfor.Avatar, DateTime.Now.ToString("HH:mm"), image, 2);
        //}
    }
}