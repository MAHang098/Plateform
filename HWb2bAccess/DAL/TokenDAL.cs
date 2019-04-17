using Common;
using HWb2bAccess.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.DAL
{
    internal class TokenDAL
    {
       
       
        public static Token GetToken()
        {
            string uri = "oauth2/token";
            if (File.Exists(SettingItems.settingFile))
            {
                //读配置文件
                MyConfiguration cfg = new MyConfiguration(false);
                cfg.Load(SettingItems.settingFile);
                string baseUrl = cfg.ReadString("BaseUrl");
                string appKey = cfg.ReadString("AppKey");
                string appSecret = cfg.ReadString("AppSecret");
                System.Net.HttpWebResponse response = HwApiHelper.CreateHwTokenHttpResponse(baseUrl, uri, appKey, appSecret, null);
                string result = HwApiHelper.GetResponseString(response);
                Token token = JsonConvert.DeserializeObject<Token>(result);
                return token;
            }
            else
            {
                return null;
            }

        }
        
    }
}
