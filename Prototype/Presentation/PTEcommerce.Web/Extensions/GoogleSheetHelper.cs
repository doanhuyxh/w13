using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Framework.Configuration;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;

namespace PTEcommerce.Web.Extensions
{
    public class GoogleSheetHelper
    {
        public static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        public static bool GetWhiteListId(string vid)
        {
            ServiceAccountCredential credential;
            string[] Scopes = { SheetsService.Scope.Spreadsheets };
            string serviceAccountEmail = "hagopaysheets@hagopaysheet.iam.gserviceaccount.com";
            string jsonfile = Config.GetConfigByKey("pathCredentials");
            using (Stream stream = new FileStream(@jsonfile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                credential = (ServiceAccountCredential)
                    GoogleCredential.FromStream(stream).UnderlyingCredential;

                var initializer = new ServiceAccountCredential.Initializer(credential.Id)
                {
                    User = serviceAccountEmail,
                    Key = credential.Key,
                    Scopes = Scopes
                };
                credential = new ServiceAccountCredential(initializer);
            }

            SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "HagoPaySheets",
            });
            string spreadsheetId = "1R-6RKLQNDgLDv9jj76kejntffAr-2qFQtogd05hLTs0";  // TODO: Update placeholder value.

            string range = "工作表1!A2:A";
            SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum valueRenderOption = (SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum)0;  // TODO: Update placeholder value.

            SpreadsheetsResource.ValuesResource.GetRequest.DateTimeRenderOptionEnum dateTimeRenderOption = (SpreadsheetsResource.ValuesResource.GetRequest.DateTimeRenderOptionEnum)0;  // TODO: Update placeholder value.

            SpreadsheetsResource.ValuesResource.GetRequest request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
            request.ValueRenderOption = valueRenderOption;
            request.DateTimeRenderOption = dateTimeRenderOption;

            ValueRange response = request.Execute();
            if (response != null && response.Values != null && response.Values.Count > 0)
            {
                var dataCheck = response.Values.Where(x => x.Count() > 0).Where(x => x.FirstOrDefault().ToString() == vid).FirstOrDefault();
                if (dataCheck != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}