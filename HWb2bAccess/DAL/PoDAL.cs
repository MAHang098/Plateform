using Common;
using HWb2bAccess.Model;
using HWb2bAccess.Model.PO;
using Newtonsoft.Json;
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
        string file = "HwApi.ini";
        string findPoLineListUri = "service/esupplier/findPoLineList/1.0.0/";
        internal PoDAL()
        {
            //读配置文件
            MyConfiguration cfg = new MyConfiguration(false);
            cfg.Load(file);
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
        internal PoLineListOutputParameter GetPoLineList(PoLineListInParameter inParameter,int page,int pageSize)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string json = JsonConvert.SerializeObject(inParameter, settings);
            string url = pageSize == 100 ? findPoLineListUri + page: findPoLineListUri + pageSize + "/" + page;
            var res= HwApiHelper.HuaweiPostSync(baseUrl, url, token.Access_token, json, null);
            string resJson = HwApiHelper.GetResponseString(res);
            PoLineListOutputParameter output = JsonConvert.DeserializeObject<PoLineListOutputParameter>(resJson);
            return output;
        }

        #endregion
    }
}
