using HWb2bAccess.BLL;
using HWb2bAccess.Model;
using HWb2bAccess.Model.PO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestHwOpenApi
{
    class Program
    {
        static void Main(string[] args)
        {
            PoBLL bll = new PoBLL();
            PoLineListInParameter inParameter = new PoLineListInParameter();
            PoLineListOutputParameter polineList = bll.GetPoLineList(inParameter, 1,2);
            Console.WriteLine(polineList.FindPoLineListPageV0);
            Console.ReadKey();
        }





    }
   
}
