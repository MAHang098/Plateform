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
            
            PoLineListOutput polineList = bll.GetPoLineList(2,EPoStatus.all,EPoSubType.P,EShipmentStatus.all,2 );
            if(polineList!=null)
            {
                Console.WriteLine("Find POs!");
                Console.WriteLine("Total rows: " + polineList.PageVO.TotalRows);
                Console.WriteLine("Total pages: " + polineList.PageVO.TotalPages);
                Console.WriteLine("Page size: " + polineList.PageVO.PageSize);
                Console.WriteLine("Current pages: " + polineList.PageVO.CurPage);
            }
            else
            {
                Console.WriteLine("Nothing found!");
            }
            Console.WriteLine("");
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }





    }
   
}
