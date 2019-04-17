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
        /// 
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
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
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
            return request.GetResponse() as HttpWebResponse;
        }

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
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
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
        private static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
