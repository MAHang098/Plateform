using HWb2bAccess.DAL;
using HWb2bAccess.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.BLL
{
    public class DownloadBLL
    {
        DownloadDAL dal = new DownloadDAL();
        public DownloadBLL() { }

        public bool DownloadToFile(string file, FileDownloadInput input)
        {
            Stream stream = dal.DownloadToStream(input);
            if (stream == null) return false;
            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);
            byte[] data = new byte[1024];
            while(stream.Read(data,0,data.Length)>0)
            {
                fs.Write(data, 0, data.Length);
                fs.Flush();
            }
            fs.Close();
            return true;
        }
    }
}
