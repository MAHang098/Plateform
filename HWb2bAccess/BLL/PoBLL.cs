using HWb2bAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.BLL
{
    public class PoBLL
    {
        TokenBLL tokenBLL = null;
        AccessToken accessToken = null;
        public PoBLL()
        {
            

            tokenBLL = new TokenBLL();
            accessToken = tokenBLL.CurrentToken;
        }
        public POInfo GetPO()
        {

        }
    }
}
