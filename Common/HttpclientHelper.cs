using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpClientHelper
    {
        /// <summary>
        /// 执行HTTPGET指令,带回状态信息,json信息和头部信息
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="uri"></param>
        /// <param name="jsonStr"></param>
        /// <param name="respHeaders"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static HttpStatusCode HttpClientGetSync(string baseUri, string uri, out string jsonStr,out HttpResponseHeaders respHeaders, Dictionary<string, string> headers = null)
        {
           
            // Create an HttpClient instance 
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != null)
                {
                    if (headers.Count > 0)
                    {
                        foreach (var k in headers.Keys)
                        {
                            client.DefaultRequestHeaders.Add(k, headers[k]);
                        }
                    }
                }
                //远程获取数据
                var task = client.GetAsync(uri);
                var rep = task.Result;//在这里会等待task返回。
                                      //读取响应内容
                respHeaders = rep.Headers;
                var task2 = rep.Content.ReadAsStringAsync();
                jsonStr = task2.Result;//在这里会等待task返回。
                return task.Result.StatusCode;
            }
        }
        public static HttpWebResponse CreateTokenHttpResponse(string url, string key, string secury, CookieCollection cookies)
        {
            HttpWebRequest request = null;
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

        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 执行HTTPGET指令,带回状态信息,json信息,无头部信息
        /// </summary>
        /// <param name="baseUri"></param>
        /// <param name="uri"></param>
        /// <param name="jsonStr"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static HttpStatusCode HttpClientGetSync(string baseUri, string uri, out string jsonStr, Dictionary<string,string> headers=null)
        {
            return HttpClientGetSync(baseUri, uri, out jsonStr, out HttpResponseHeaders respHeaders, headers);
        }
        /// <summary>
        /// 同步方式获取Post返回的Json数据,无头部信息
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static HttpStatusCode HttpClientPostSync(string baseUri, string uri, FormUrlEncodedContent content,out string jsonStr, Dictionary<string, string> headers=null)
        {
            return HttpClientPostSync(baseUri, uri, content, out jsonStr, out HttpResponseHeaders respHeaders, headers);
        }

        /// <summary>
        /// 同步方式获取Post返回的Json数据,带回头部信息
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static HttpStatusCode HttpClientPostSync(string baseUri, string uri, FormUrlEncodedContent content, out string jsonStr, out HttpResponseHeaders respHeaders,  Dictionary<string, string> headers = null)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != null)
                {
                    if (headers.Count > 0)
                    {
                        foreach (var k in headers.Keys)
                        {
                            client.DefaultRequestHeaders.Add(k, headers[k]);                            
                        }
                    }
                }
                var task = client.PostAsync(uri, content);
                var rep = task.Result;//在这里会等待task返回。
                                      //读取响应内容
                respHeaders = rep.Headers;
                var task2 = rep.Content.ReadAsStringAsync();
                jsonStr = task2.Result;//在这里会等待task返回。
                return task.Result.StatusCode;
            }
        }

        /// <summary>
        /// 同步方式获取Put返回的Json数据
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="content"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static HttpStatusCode HttpClientPutSync(string baseUri, string uri, FormUrlEncodedContent content, out string jsonStr, out HttpResponseHeaders respHeaders, Dictionary<string, string> headers = null)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != null)
                {
                    if (headers.Count > 0)
                    {
                        foreach (var k in headers.Keys)
                        {
                            client.DefaultRequestHeaders.Add(k, headers[k]);
                        }
                    }
                }
                var task = client.PutAsync(uri, content);
                var rep = task.Result;//在这里会等待task返回。

                //读取响应内容
                respHeaders = rep.Headers;
                var task2 = rep.Content.ReadAsStringAsync();
                jsonStr = task2.Result;//在这里会等待task返回。
                return task.Result.StatusCode;
            }
        }
        public static HttpStatusCode HttpClientDeleteSync(string baseUri, string uri, out string jsonStr, Dictionary<string, string> headers = null)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != null)
                {
                    if (headers.Count > 0)
                    {
                        foreach (var k in headers.Keys)
                        {
                            client.DefaultRequestHeaders.Add(k, headers[k]);
                        }
                    }
                }
                
                var task = client.DeleteAsync(uri);
                var rep = task.Result;//在这里会等待task返回。
                //读取响应内容
                var task2 = rep.Content.ReadAsStringAsync();
                jsonStr = task2.Result;//在这里会等待task返回。
                return task.Result.StatusCode;
            }
        }
        #region 异步方法
        /// <summary>
        /// 异步HTTPGET获取Json数据函数
        /// 需传入两个方法分别处理成功和失败
        /// </summary>
        /// <param name="uri">资源地址</param>
        /// <param name="token">访问令牌</param>
        /// <param name="susFunc">获取成功，处理Json数据</param>
        /// <param name="failFunc">获取失败处理方法</param>
        public static async void HttpClientGetAsync(string baseUri, string uri, Action<string> susFunc, Action<HttpResponseMessage,string> failFunc, Dictionary<string, string> headers=null)
        {
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(baseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (headers != null)
                {
                    if (headers.Count > 0)
                    {
                        foreach (var k in headers.Keys)
                        {
                            client.DefaultRequestHeaders.Add(k, headers[k]);
                        }

                    }
                }
                HttpResponseMessage response = await client.GetAsync(uri);
                string jsonStr = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    susFunc.Invoke(jsonStr);
                }
                else
                {
                    failFunc(response, jsonStr);
                }
            }
        }
        public static async void HttpClientPostAsync(string baseUri, string uri, FormUrlEncodedContent content, Action<string> susFunc, Action<HttpResponseMessage,string> failFunc, Dictionary<string,string>headers=null)
        {

            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None };

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(baseUri);               
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if(headers!=null)
                {
                    if (headers.Count > 0)
                    {
                        foreach (var k in headers.Keys)
                        {
                            client.DefaultRequestHeaders.Add(k, headers[k]);
                        }
                    }
                }
                var response = await client.PostAsync(uri, content);
                string jsonStr = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {                    
                    susFunc.Invoke(jsonStr);
                }
                else
                {  
                    failFunc(response, jsonStr);
                }  
            }
        }
        #endregion 
    }
}
