using HWb2bAccess.BLL;
using HWb2bAccess.Model;
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
            Token token = TokenBLL.CurrentToken;
            Console.WriteLine("The Token is: " + token.Access_token);
            Console.ReadKey();
        }





    }
   
}
