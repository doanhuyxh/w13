using Framework.EF;
using marketplace;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using PTEcommerce.Business;
using System.ServiceProcess;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using Framework.Helper.Extensions;
using RestSharp;

namespace PTEcommerce.Services.AutoCalculateResult
{
    public partial class ServiceAutoCalculateResult : ServiceBase
    {
        private readonly Timer TimeReload;
        private readonly IPlayGames playGames;
        private readonly IAccountCustomer accountCustomer;
        private readonly IHistoryTransfer historyTransfer;
        private readonly ISessionGames sessionGames;
        private readonly string urlDomain = ConfigurationManager.AppSettings["url"];
        public ServiceAutoCalculateResult()
        {
            InitializeComponent();
            playGames = SingletonIpl.GetInstance<IplPlayGames>();
            accountCustomer = SingletonIpl.GetInstance<IplAccountCustomer>();
            historyTransfer = SingletonIpl.GetInstance<IplHistoryTransfer>();
            sessionGames = SingletonIpl.GetInstance<IplSessionGames>();
            long timeLoop = long.Parse(ConfigurationManager.AppSettings["timeLoop"]);
            //Set khoảng thời gian chạy lại
            TimeReload = new Timer(timeLoop);
            TimeReload.Elapsed += new ElapsedEventHandler(WorkProcess);
        }
        public void WorkProcess(object o, ElapsedEventArgs e)
        {
            try
            {
                var sessionOld = sessionGames.GetYetSession();
                #region Create New Session
                Random random = new Random();
                int value1 = random.Next(1, 3);
                int value2 = random.Next(3, 5);
                int newSessionId = 0;
                var resultCreateSession = sessionGames.CreateSessionGame(value1, value2, ref newSessionId);
                if (!resultCreateSession)
                {
                    LogService("Error create session");
                }
                if(newSessionId <= 0)
                {
                    LogService("Session current has running! Cannot create");
                }
                var client = new RestClient(urlDomain + "/AdministratorManager/PushSignalR/CreateSession?sessionId=" + newSessionId + "&result1=" + value1 + "&result2=" + value2);
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if(response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    LogService("Calll signalR success");
                }
                else
                {
                    LogService("Calll signalR error");
                }
                #endregion
                #region Proccess Result Session Old
                if (sessionOld != null)
                {
                    var listProccess = playGames.GetListPlayGameBySessionId(sessionOld.Id);
                    if (listProccess != null && listProccess.Count > 0)
                    {
                        foreach (var item in listProccess)
                        {
                            var dataConvert = Helper.ConvertValue(item.Value);
                            var dataConvertResult = Helper.ConvertValue(string.Join(",", new List<string> { sessionOld.Value.ToString(), sessionOld.Value2.ToString() }));
                            var dataWin = dataConvert.value.Where(x => x == sessionOld.Value.ToString() || x == sessionOld.Value2.ToString()).ToList();
                            if (dataWin != null && dataWin.Count > 0)
                            {
                                item.AmountReceive = dataWin.Count * item.Amount * 2;
                                item.Status = 2;
                                item.Result = string.Join(",", dataConvertResult.value);
                                item.ResultString = dataConvertResult.valuestring;
                                item.CompletedDate = DateTime.Now;
                                playGames.Update(item);
                                var accountData = accountCustomer.GetById(item.IdAccount);
                                if (accountData != null)
                                {
                                    var amountBefore = accountData.AmountAvaiable;
                                    accountData.AmountAvaiable += dataWin.Count * item.Amount * 2;
                                    accountCustomer.Update(accountData);
                                    historyTransfer.Insert(new HistoryTransfer
                                    {
                                        IdAccount = accountData.Id,
                                        AmountBefore = amountBefore,
                                        AmountModified = item.Amount,
                                        AmountAfter = amountBefore + item.Amount,
                                        CreatedDate = DateTime.Now,
                                        Note = "Phiên " + item.SessionId + " đoán trúng " + dataConvertResult.valuestring + " nhận " + Helper.MoneyFormat(item.AmountReceive),
                                        Type = 1
                                    });
                                }
                            }
                            else
                            {
                                item.Status = 4;
                                item.Result = string.Join(",", dataConvertResult.value);
                                item.ResultString = dataConvertResult.valuestring;
                                item.CompletedDate = DateTime.Now;
                                item.AmountReceive = 0;
                                playGames.Update(item);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogService("Error: " + JsonConvert.SerializeObject(ex));
            }
        }
        protected override void OnStart(string[] args)
        {
            LogService("=========== Service Recharge Zing started on: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " ===========");
            TimeReload.Enabled = true;
        }

        protected override void OnStop()
        {
            LogService("=========== Service Recharge Zing stopped on: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " ===========");
            TimeReload.Enabled = false;
        }

        private string ConvertValue(int value)
        {
            string result = string.Empty;
            switch (value)
            {
                case 1:
                    result = "LỚN";
                    break;
                case 2:
                    result = "NHỎ";
                    break;
                case 3:
                    result = "ĐÔI";
                    break;
                case 4:
                    result = "ĐƠN";
                    break;
            }
            return result;
        }
        public void LogService(string content)
        {
            string url = ConfigurationManager.AppSettings["LogServiceUrl"];
            string fileName = @"\RechargeGarena_" + DateTime.Now.ToString("dd_MM_yyyy") + "_Log.txt";
            FileStream fs = new FileStream(url + fileName, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();
        }
    }
}
