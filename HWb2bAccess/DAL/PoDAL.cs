using Common;
using HWb2bAccess.Model;
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
        string uri = "oauth2/token";
        public PoDAL()
        {
            //读配置文件
            MyConfiguration cfg = new MyConfiguration(false);
            cfg.Load(file);
            baseUrl = cfg.ReadString("BaseUrl");
            token = TokenDAL.GetToken();
        }
        #region 各种方法
        public PO GetPo(string id)
        {
            return null;
        }

        #endregion
    }
}
