using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class MyConfiguration
    {
        private class Variable
        {
            public string Value;
            public bool Changed;

            public Variable(string value)
            {
                Value = value;
                Changed = false;
            }
        }
        private string mFilePath;
        private Dictionary<string, Variable> mDictionaries;
        private bool encrypt;//选择加密与否
        public MyConfiguration( bool encrypt=true)
        {
            this.encrypt = encrypt;
            mDictionaries = new Dictionary<string, Variable>();
        }
        public int TotalKeys()
        {
            return mDictionaries.Count;
        }
        public void Load(string filePath)//下载文件
        {            
            mDictionaries.Clear();
            mFilePath = filePath;
            StreamReader reader = new StreamReader(mFilePath);
            string line, key, value;
            int index;
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                if(encrypt)line = Decrypt(line);
                index = line.IndexOf('!');//忽略注释
                if (index>=0) line = line.Substring(0,index);
                index = line.IndexOf('！');//忽略注释，兼容中文注释符
                if (index >= 0) line = line.Substring(0, index);
                index = line.IndexOf('=');
                if (index <0)
                    continue;
                key = line.Substring(0, index);
                if (string.IsNullOrEmpty(key))
                    continue;

                if (index >= line.Length - 1)
                    value = string.Empty;
                else
                    value = line.Substring(index + 1);

                mDictionaries.Add(key, new Variable(value));
            }
            reader.Close();
        }
        
        public void Save()
        {
            Save(mFilePath);
        }

        public void Save(string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath, false);

            ArrayList keyList = new ArrayList(mDictionaries.Keys);
            keyList.Sort();
            if (encrypt)
            {
                foreach (string key in keyList)
                    writer.WriteLine(Encrypt(key + "=" + mDictionaries[key].Value));
            }
            else
            {
                foreach (string key in keyList)
                    writer.WriteLine(key + "=" + mDictionaries[key].Value);
            }
               
            writer.Close();
        }

        public void Clear()
        {
            mDictionaries.Clear();            
        }

        public bool ContainsKey(string key)
        {
            return mDictionaries.ContainsKey(key);
        }
        
       
        public void WriteString(string key, string value)
        {
            if (mDictionaries.ContainsKey(key))
            {
                if (mDictionaries[key].Value != value)
                {
                    mDictionaries[key].Value = value;
                    mDictionaries[key].Changed = true;
                }
                return;              
            }
            else
            {
                mDictionaries.Add(key, new Variable(value));
                mDictionaries[key].Changed = true;
                //throw (new Exception("不存在变量 '" + key + "'，写入失败."));
            }
        }
        /// <summary>
        /// 因为文件是以文本方式保存，所以本方法为读出配置的基本方法
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string ReadString(string key)//读取文件内容
        {
            string value = string.Empty;
            if (mDictionaries.ContainsKey(key))
                value= mDictionaries[key].Value;
            return value;          
        }

        public void WriteIntger(string key, int value)
        {
            WriteString(key, value.ToString());
        }

        public int ReadIntger(string key)
        {
            int result = 0;
            string value = ReadString(key);
            if (!int.TryParse(value, out result))
            {
                result = 0;
            }
            return result;
        }

        public void WriteDouble(string key, double value)
        {
            WriteString(key, value.ToString());
        }
       
        public double ReadDouble(string key)
        {
            double result = 0;
            string value = ReadString(key);
            if (!double.TryParse(value, out result))
            {
                result = 0;
                //throw (new Exception("变量 '" + key + "' 值为 '" + value + "', 无法转换成double型，默认返回0."));                
            }
            return result;
        }
        public void WriteFloat(string key, float value)
        {
            WriteString(key, value.ToString());
        }
        public float ReadFloat(string key)
        {
            string value = ReadString(key);
            float.TryParse(value, out float result);
            return result;
        }
        public void WriteBoolean(string key, bool value)
        {
            WriteString(key, value.ToString());
        }

        public bool ReadBoolean(string key)
        {
            bool result = false;
            string value = ReadString(key);
            if (!bool.TryParse(value, out result))
            {
                result = false;
                //throw (new Exception("变量 '" + key + "' 值为 '" + value + "', 无法转换成bool型，默认返回0."));
               
            }
            return result;
        }

        public bool IsValueChanged()
        {
            foreach (KeyValuePair<string, Variable> entry in mDictionaries)
            {
                if (entry.Value.Changed)
                    return true;
            }
            return false;
        }

        public bool IsValueChangedForSection(string section)
        {
            foreach (KeyValuePair<string, Variable> entry in mDictionaries)
            {
                if (entry.Key.Contains(section + "."))
                {
                    if (entry.Value.Changed)
                        return true;
                }
            }
            return false;
        }

        public bool IsValueChangedForKey(string key)
        {
            foreach (KeyValuePair<string, Variable> entry in mDictionaries)
            {
                if (entry.Key == key)
                {
                    if (entry.Value.Changed)
                        return true;
                }
            }
            return false;
        }

        public MyConfiguration GetChanges(MyConfiguration config)
        {
            MyConfiguration result = new MyConfiguration();
            result.mDictionaries.Clear();
            foreach (KeyValuePair<string, Variable> entry in mDictionaries)
                result.mDictionaries.Add(entry.Key, new Variable(entry.Value.Value));
            foreach(KeyValuePair<string, Variable> entry in config.mDictionaries)
            {
                if (result.ContainsKey(entry.Key))
                    result.WriteString(entry.Key, entry.Value.Value);
            }
            return result;
        }

        private byte[] RgbKey = { 0x64, 0x6C, 0x41, 0x4B, 0x63, 0x56, 0x49, 0x67 };
        private byte[] RgbIV = { 0x18, 0x52, 0x36, 0x98, 0x90, 0xAE, 0xBE, 0xDF };

        private string Encrypt(string encryptString)
        {
            try
            {
                byte[] rgbKey = RgbKey;
                byte[] rgbIV = RgbIV;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        private string Decrypt(string decryptString)
        {
            try
            {
                byte[] rgbKey = RgbKey;
                byte[] rgbIV = RgbIV;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
    }
}
