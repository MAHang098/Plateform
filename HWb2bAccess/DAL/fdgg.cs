﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.DAL
{
    class fdgg
    {



        NET_Example

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

        主方法：
public static void PushGPSToHuawei(DataMap.GPS gps)
        {
            try
            {
                string accessToken = GetToken();
                string url = ConfigurationManager.AppSettings["HW_GPS_URL"];
                string sql = @"select Ord.OrdID, Ord.CustOrdNO from Ord where OrdID=" + gps.OrdId;
                DataTable ordDt = SqlHelper.ExecuteDataset(CGlobal.ConnectionString, CommandType.Text, sql).Tables[0];
                string custOrdNO = ordDt.Rows[0]["CustOrdNO"].ToString();
                string jsonGpsModel = GetParamStr(gps, custOrdNO);

                System.Net.HttpWebResponse response = HttpHelper.CreateToHuaWiePostHttp(url, accessToken, jsonGpsModel, null);
                string strResult = HttpHelper.GetResponseString(response);

                YLAiTrackPModel resultModel = JsonHelper.JsonDeserialize<YLAiTrackPModel>(strResult);
                if (resultModel.stat != "1") //stat：0，调用失败；1，调用成功；2，非法访问
                {
                    string note = "推送失败，ResultStatus：" + resultModel.stat + "，车牌号码：" + gps.DeviceId + "，OrdId：" + gps.OrdId;
                    throw new Exception(note);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("往华为推送GPS数据异常：" + ex.Message);
            }
        }

        获取令牌方法：
private static string GetToken()
        {
            string url = System.Configuration.ConfigurationManager.AppSettings["HW_TOKEN_URL"];
            string key = System.Configuration.ConfigurationManager.AppSettings["key"];
            string secury = System.Configuration.ConfigurationManager.AppSettings["secury"];
            System.Net.HttpWebResponse response = HttpHelper.CreateTokenHttpResponse(url, key, secury, null);
            string result = HttpHelper.GetResponseString(response);
            Token token = JsonHelper.JsonDeserialize<Token>(result);
            return token.access_token;
        }
        编辑json格式的字串参数方法：
private static string GetParamStr(DataMap.GPS gps, string custOrdNO)
        {
            StringBuilder paramStr = new StringBuilder();
            paramStr.Append("processDyn=Y&serviceCode=addLSPLOCINFO&params={\"P_LSP_DATA\":\"");
            paramStr.Append("{shipmentID:\\\"" + custOrdNO + "\\\",");
            paramStr.Append("timestamp:{\\\"$date\\\":\\\"" + Newtonsoft.Json.JsonConvert.SerializeObject(gps.CreateTime).Replace("+", "%2B").Replace("\"", "") + "\\\"},");
            paramStr.Append("location:{type:\\\"Point\\\",");
            paramStr.Append("coordinates:[" + gps.Longitude + "," + gps.Latitude + "]},");
            paramStr.Append("transEquipType:\\\"" + System.Web.HttpUtility.UrlEncode("汽车") + "\\\",");
            paramStr.Append("transEquipID:\\\"" + System.Web.HttpUtility.UrlEncode(gps.DeviceId) + "\\\",");
            paramStr.Append("driverName:\\\"\\\",");
            paramStr.Append("driverPhoneNO:\\\"\\\",");
            paramStr.Append("speed:0,");
            paramStr.Append("temperature:0,");
            paramStr.Append("acceleration:0,");
            paramStr.Append("luminousIntensity:0,");
            paramStr.Append("pressure:0,");
            paramStr.Append("direction:0,");
            paramStr.Append("carrierName:\\\"\\\",");
            paramStr.Append("carrierOrderNO:\\\"\\\",");
            paramStr.Append("deviceType:\\\"" + gps.DeviceType + "\\\",");
            paramStr.Append("deviceSN:\\\"\\\",");
            paramStr.Append("deviceStatus:\\\"" + System.Web.HttpUtility.UrlEncode(gps.Status) + "\\\",");
            paramStr.Append("battaryLevel:0,");
            paramStr.Append("signalStatus:0,");
            paramStr.Append("uploadFrequency:0,");
            paramStr.Append("bindingTime:{\\\"$date\\\":\\\"" + Newtonsoft.Json.JsonConvert.SerializeObject(DateTime.Now).Replace("+", "%2B").Replace("\"", "") + "\\\"},");
            paramStr.Append("unbindingTime:{\\\"$date\\\":\\\"" + Newtonsoft.Json.JsonConvert.SerializeObject(DateTime.Now).Replace("+", "%2B").Replace("\"", "") + "\\\"},");
            paramStr.Append("mileage:0,");
            paramStr.Append("locationDesc:\\\"" + System.Web.HttpUtility.UrlEncode(gps.Location) + "\\\",");
            paramStr.Append("ETA:{\\\"$date\\\":\\\"" + Newtonsoft.Json.JsonConvert.SerializeObject(DateTime.Now).Replace("+", "%2B").Replace("\"", "") + "\\\"},");
            paramStr.Append("LSPCode:\\\"HTM.YUEHAI\\\",PLDN:\\\"\\\"}");
            paramStr.Append("\"}");
            return paramStr.ToString();
        }
        http Post方法：
public static HttpWebResponse CreateToHuaWiePostHttp(string url, string accessToken, string paramStr, CookieCollection cookies)
        {
            HttpWebRequest request = null;
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
            byte[] data = Encoding.Default.GetBytes(paramStr);
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
        证书验证：
public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

    }
}
