using Common;
using HWb2bAccess.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B2BPlatform
{
    public partial class FrmSystem : Form
    {
        public FrmSystem()
        {
            InitializeComponent();
        }
        MyConfiguration cfg = new MyConfiguration(false);
        private void BtnOK_Click(object sender, EventArgs e)
        {
            
            cfg.WriteString(SettingItems.baseUrl, txtBaseUrl.Text);
            cfg.WriteString(SettingItems.appKey, txtAppKey.Text);
            cfg.WriteString(SettingItems.appSecret, txtAppSecret.Text);
            cfg.Save(SettingItems.settingFile);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FrmSystem_Load(object sender, EventArgs e)
        {
            if (File.Exists(SettingItems.settingFile))
            {
                cfg.Load(SettingItems.settingFile);
                if (cfg.ContainsKey(SettingItems.baseUrl))
                    txtBaseUrl.Text = cfg.ReadString(SettingItems.baseUrl);
                if (cfg.ContainsKey(SettingItems.appKey))
                    txtAppKey.Text = cfg.ReadString(SettingItems.appKey);
                if (cfg.ContainsKey(SettingItems.appSecret))
                    txtAppSecret.Text = cfg.ReadString(SettingItems.appSecret);
            }
        }
    }
}
