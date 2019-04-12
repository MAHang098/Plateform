using Common;
using HWb2bAccess.Model;
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
        static string appId = null;
        static string appSecret = null;
        static string file = "HWinfo.ini";
        public TokenDAL()
        {

            
        }
        public static AccessToken GetToken()
        {

            MyConfiguration cfg = new MyConfiguration(false);
            cfg.Load(file);
            cfg.ReadString(file);
            /*System.Net.HttpWebResponse response = hhelp.CreateTokenHttpResponse(baseUrl, appId, appSecret, null);
            string result = HttpHelper.GetResponseString(response);
            Token token = JsonHelper.JsonDeserialize<Token>(result);
            return token.access_token;*/
            AccessToken token = null;
            string uri = "connect/token";
            var client = new RestClient(baseUri);
            var request = new RestRequest(uri, Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            client.Authenticator = new HttpBasicAuthenticator(authBase.ClientId, authBase.ClientSecret);
            request.AddParameter("client_id", authBase.ClientId);
            request.AddParameter("appSecret", authBase.ClientSecret);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", authBase.Username);
            request.AddParameter("password", authBase.Password);
            //request.AddParameter("scope", "openapi offline_access");//可不用
            IRestResponse<AccessToken> response = client.Execute<AccessToken>(request);
            if (response.IsSuccessful)
            {
                token = response.Data;
            }
            return token;
        }
        
    }
}
