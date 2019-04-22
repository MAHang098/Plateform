using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.DAL
{
    public class HwApiHelper
    {
        static int timeout = 30000;//10s
        #region HW OpenAPI Access
        /// <summary>
        /// 用于HW Open API获取token
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="uri"></param>
        /// <param name="key">AppKey</param>
        /// <param name="secury">AppSecrect</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static HttpWebResponse CreateHwTokenHttpResponse(string baseUrl, string uri, string key, string secury, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            string url = CompleteUrl(baseUrl, uri);
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                SetCertificatePolicy();
                request = WebRequest.Create(url) as HttpWebRequest;
                
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
               
            }
            request.Timeout = timeout;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(key + ":" + secury)).Trim());

            byte[] data = Encoding.Default.GetBytes("grant_type=client_credentials");
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }
        /// <summary>
        /// 执行HW API的POST方法
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="uri"></param>
        /// <param name="accessToken"></param>
        /// <param name="paramsJson">根据输入参数序列化后的json数据</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static HttpWebResponse HuaweiPostSync(string baseUrl, string uri, string accessToken, string paramsJson, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            string url = CompleteUrl(baseUrl, uri);
            //发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                SetCertificatePolicy();               
            }
            
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = timeout;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + accessToken.Trim());
            request.Headers.Add("soapAction", "");
            byte[] data = Encoding.Default.GetBytes(paramsJson);
            request.ContentLength = data.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //发送POST数据    
            var reps = request.GetResponse() as HttpWebResponse;
            return reps;
        }
        /// <summary>
        /// 执行HW API的Get方法
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="uri"></param>
        /// <param name="accessToken"></param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static HttpWebResponse HuaweiGetSync(string baseUrl, string uri, string accessToken, CookieCollection cookies)
        {
            HttpWebRequest request = null;
            string url = CompleteUrl(baseUrl, uri);
            //发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                SetCertificatePolicy();
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Timeout = timeout;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";// "application/x-www-form-urlencoded";
            request.Headers.Add("Authorization", "Bearer " + accessToken.Trim());
            request.Headers.Add("soapAction", "");
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //接收数据        
            
            return request.GetResponse() as HttpWebResponse;
        }

        #endregion
        
      
        public static string GetResponseString(HttpWebResponse response)
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }

        }
        private static string CompleteUrl(string baseUrl, string uri)
        {
            baseUrl = baseUrl.Trim().TrimEnd('/');//删除多余的空格和尾部的/
            uri = uri.Trim().TrimStart('/');
            string url = baseUrl + "/" + uri;
            return url;
        }
        internal static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        #region Restsharp版
        /// <summary>
        /// HW API Post方法Restsharp版
        /// </summary>
        /// <typeparam name="T">传入参数的类型</typeparam>
        /// <param name="baseUrl">基础URL</param>
        /// <param name="uri">资源链接</param>
        /// <param name="accessToken">令牌</param>
        /// <param name="objToPost">要传输的实体，类型由T给定</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static string HuaweiPostSyncRest<T>(string baseUrl, string uri, string accessToken, T objToPost, params  Cookie[] cookies)
        {            
            //发送HTTPS请求  
            if (baseUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                HwApiHelper.SetCertificatePolicy();
            }
            var client = new RestClient(baseUrl);

            var request = new RestRequest(uri);
            request.Timeout = timeout;

            request.Method = Method.POST;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + accessToken.Trim());
            request.AddHeader("soapAction", "");
            request.AddJsonBody(objToPost);//直接传输一个实体,无需如HttpWebRequest那样，先序列化，编码成byte[]，再通过WriteSteam写
            if (cookies != null)
            {
                foreach (var c in cookies)
                {
                    request.AddCookie(c.Name, c.Value);
                }                
            }
            //方式1：按Json格式返回响应文本
            IRestResponse response = client.Execute(request);
            return response.Content;//按指定的格式返回响应文本，如json           
            
        }
        #endregion
    }
}
