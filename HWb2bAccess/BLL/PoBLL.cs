using HWb2bAccess.DAL;
using HWb2bAccess.Model;
using HWb2bAccess.Model.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.BLL
{
    public class PoBLL
    {
        PoDAL dal = new PoDAL();
        public PoBLL()
        {
                        
           
        }
       public PoLineListOutputParameter GetPoLineList(PoLineListInParameter inParameter, int page, int pageSize = 100)
        {
            PoLineListOutputParameter output = dal.GetPoLineList(inParameter, page, pageSize);
            return output;
        }
    }
}
