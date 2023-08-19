using Framework.EF;
using marketplace;
using PTEcommerce.Business.IBusiness;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PTEcommerce.Business.Business
{
    public class TelegramAPI : ITelegram
    {
        private readonly ISettings setting;
        public TelegramAPI()
        {
            setting = SingletonIpl.GetInstance<IplSettings>();
        }
        public bool SendMessageToGroup(string message)
        {
            var tokenTelegram = setting.GetValueByKey("token_telegram");
            var chatIdTelegram = setting.GetValueByKey("chatid_telegram");
            var client = new RestClient("https://api.telegram.org/bot" + tokenTelegram.Value + "/sendMessage?chat_id=" + chatIdTelegram.Value + "&text=" + System.Web.HttpUtility.UrlEncode(message));
            client.Timeout = -1;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            return false;
        }
    }
}
