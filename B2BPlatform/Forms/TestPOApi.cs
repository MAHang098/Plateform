using HWb2bAccess.BLL;
using HWb2bAccess.Model;
using HWb2bAccess.Model.PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    }
}
