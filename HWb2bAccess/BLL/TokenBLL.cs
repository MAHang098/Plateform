using HWb2bAccess.DAL;
using HWb2bAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.BLL
{
    public class TokenBLL
    {
        
        public static Token CurrentToken {
            get {
                return TokenDAL.GetToken();
            } }
       
    }
}
