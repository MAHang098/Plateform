using Common;
using HWb2bAccess.Model;
using HWb2bAccess.Model.PO;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.DAL
{
   public class PoDAL
    {
        string baseUrl = null;
        Token token = null;
        string settingFile = "HwApi.ini";
        string findPoLineListUri = "service/esupplier/findPoLineList/1.0.0/";
        string genPoPdfUri= "service/esupplier/genPdfOfPo/1.0.0/";
        internal PoDAL()
        {
            //读配置文件
            MyConfiguration cfg = new MyConfiguration(false);
            cfg.Load(settingFile);
            baseUrl = cfg.ReadString("BaseUrl");
            token = TokenDAL.GetToken();
        }
        #region 各种方法
        /// <summary>
        /// PO列表查询
        /// </summary>
        /// <param name="inParameter"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        internal PoLineListOutput GetPoLineList(PoLineListInput inParameter,int page,int pageSize)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string json = JsonConvert.SerializeObject(inParameter, settings);
            string url = pageSize == 100 ? findPoLineListUri + page: findPoLineListUri + pageSize + "/" + page;
            var res= HwApiHelper.HuaweiPostSync(baseUrl, url, token.Access_token, json, null);
            string resJson = HwApiHelper.GetResponseString(res);
            var output = JsonConvert.DeserializeObject<PoLineListOutput>(resJson);
            return output;
        }

        internal GenPoPdfOutParameter GenPoPdfDAL(GenPoPdfInput input)
        {
            string json = JsonConvert.SerializeObject(input);
            var res = HwApiHelper.HuaweiPostSync(baseUrl, genPoPdfUri, token.Access_token, json, null);
            string resJson = HwApiHelper.GetResponseString(res);
            var output = JsonConvert.DeserializeObject<GenPoPdfOutParameter>(resJson);
            return output;
        }


        /// <summary>
        /// RestSharp版PO列表查询
        /// </summary>
        /// <param name="inParameter"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        internal PoLineListOutput GetPoLineListRest(PoLineListInput inParameter, int page, int pageSize)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string json = JsonConvert.SerializeObject(inParameter, settings);
            string url = pageSize == 100 ? findPoLineListUri + page : findPoLineListUri + pageSize + "/" + page;
            var resJson = HwApiHelper.HuaweiPostSyncRest(baseUrl, url, token.Access_token, json, null);
            
            PoLineListOutput output = JsonConvert.DeserializeObject<PoLineListOutput>(resJson);
            return output;
        }
        #endregion





    }
}
