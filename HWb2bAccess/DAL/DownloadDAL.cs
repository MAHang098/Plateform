﻿using Common;
using HWb2bAccess.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HWb2bAccess.DAL
{
    public class DownloadDAL
    {
        string baseUrl = null;
        Token token = null;
        string settingFile = "HwApi.ini";
        string downloadUri = "service/esupplier/download/1.0.0/";
        internal DownloadDAL()
        {
            //读配置文件
            MyConfiguration cfg = new MyConfiguration(false);
            cfg.Load(settingFile);
            baseUrl = cfg.ReadString("BaseUrl");
            token = TokenDAL.GetToken();
        }

        internal Stream DownloadToStream( FileDownloadInput input)
        {
            string json = JsonConvert.SerializeObject(input);
            var res = HwApiHelper.HuaweiPostSync(baseUrl, downloadUri, token.Access_token, json, null);
            Stream stream = res.GetResponseStream();
            return stream;
        }
    }
}
