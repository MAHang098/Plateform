using HWb2bAccess.BLL;
using HWb2bAccess.Model;
using HWb2bAccess.Model.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B2BPlatform.Forms
{
    public partial class TestPOApi : Form
    {
        PoBLL bll = new PoBLL();
        int totalPages;
        public TestPOApi()
        {
            InitializeComponent();
        }

        private void TestPOApi_Load(object sender, EventArgs e)
        {
            cbPoStatus.SelectedIndex = 0;
            cbPoSubType.SelectedIndex = 1;
            cbShipmentStatus.SelectedIndex = 0;
            dgvData.AutoGenerateColumns = false;
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            int pageNum = Convert.ToInt16(txtPageNum.Text);
            int pageSize = Convert.ToInt16(txtPageSize.Text);
            dgvData.DataSource = null;
            Application.DoEvents();
            EPoStatus poStatus = (EPoStatus)cbPoStatus.SelectedIndex;
            
            EPoSubType subType = (EPoSubType)cbPoSubType.SelectedIndex;
            EShipmentStatus shipmentStatus = (EShipmentStatus)cbShipmentStatus.SelectedIndex;
            PoLineListOutput poLine = bll.GetPoLineList(pageNum, poStatus, subType, shipmentStatus, pageSize);
            if(poLine!=null)
            {
                totalPages = poLine.PageVO.TotalPages;
                lbTotalPages.Text = "/" + totalPages;
                txtTotalRows.Text = poLine.PageVO.TotalRows.ToString();
               
                dgvData.DataSource = poLine.Result;
            }
        }

        
        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnDecrease_Click(object sender, EventArgs e)
        {
            int pageNum = Convert.ToInt16(txtPageNum.Text);
            if(pageNum!=1)
            {
                pageNum--;
                txtPageNum.Text = pageNum.ToString();
                BtnQuery_Click(sender, e);
            }
        }

        private void BtnIncrease_Click(object sender, EventArgs e)
        {
            int pageNum = Convert.ToInt16(txtPageNum.Text);
            if (pageNum != totalPages)
            {
                pageNum++;
                txtPageNum.Text = pageNum.ToString();
                BtnQuery_Click(sender, e);
            }
        }

        private void BtnPrintPo_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog
            {
                Filter= "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*",
                DefaultExt = "pdf",
                RestoreDirectory = true,
                InitialDirectory = Environment.CurrentDirectory
            };
            if(fd.ShowDialog()==DialogResult.OK)
            {
                List<string> poNums = new List<string>();
                foreach (DataGridViewRow r in dgvData.SelectedRows)
                {
                    poNums.Add(r.Cells["ColPoNum"].Value.ToString());
                    
                }
               
                if( bll.DownloadPoPdf(fd.FileName, true, ELang.zh_CN,EInstanceId.Huawei, poNums.ToArray()))
                {
                    Process.Start(fd.FileName);
                }
                else
                {
                    MessageBox.Show("下载文件失败！","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void DgvData_SelectionChanged(object sender, EventArgs e)
        {
            btnPrintPo.Enabled = dgvData.SelectedRows.Count > 0;
            btnSignBack.Enabled = dgvData.SelectedRows.Count > 0;
        }

        private void BtnSignBack_Click(object sender, EventArgs e)
        {

            SignBackPoListInput input = new SignBackPoListInput();
            foreach (DataGridViewRow r in dgvData.SelectedRows)
            {
                ColTaskQuery colTask = new ColTaskQuery()
                {
                    lineLocationId = Convert.ToInt32(r.Cells["ColLineLocationId"].Value)
                };
                if (r.Cells["ColTaskId"].Value != null)
                    colTask.taskId = Convert.ToInt32(r.Cells["ColTaskId"].Value);
                input.colTaskQueries.Add(colTask);

            }
            if( bll.SignBackPoList(input))
            {
                MessageBox.Show("订单签返成功！","提示",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("错误码："+bll.ErrorCode+"错误信息："+bll.ErrorMsg,"签返失败",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
