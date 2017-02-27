using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FBGEMSystem.LiveDataShow
{
    public partial class FBGData1 : Form
    {
        public FBGData1()
        {
            InitializeComponent();
            this.listView1.View = System.Windows.Forms.View.Details;
            string ColumnsName = "";
            for (int i = 1; i < 9; i++)
            {
                ColumnsName = "FBG" + i.ToString();
                this.listView1.Columns.Add(ColumnsName);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
