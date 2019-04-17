using Common;
using HWb2bAccess.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.DAL
{
    internal class TokenDAL
    {
        static string baseUrl = null;
        static string appKey = null;
        static string appSecret = null;
        static string file = "HwApi.ini";
        static string uri = "oauth2/token";
       
        public static Token GetToken()
        {
            //读配置文件
            MyConfiguration cfg = new MyConfiguration(false);
            cfg.Load(file);
            baseUrl = cfg.ReadString("BaseUrl");
            appKey = cfg.ReadString("AppKey");
            appSecret = cfg.ReadString("AppSecret");

            System.Net.HttpWebResponse response = HwApiHelper.CreateHwTokenHttpResponse(baseUrl,uri, appKey, appSecret, null);
            string result = HwApiHelper.GetResponseString(response);
            Token token = JsonConvert.DeserializeObject<Token>(result);
            return token;

        }
        
    }
}
