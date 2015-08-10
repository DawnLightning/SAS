using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SAS.Forms
{
    public partial class frmPlanSearch : Form
    {
        private DateTime CurrentTime; 
        public frmPlanSearch(DateTime dt)
        {
            this.CurrentTime = dt;
            InitializeComponent();
        }

        private void frmPlanSearch_Load(object sender, EventArgs e)
        {
            MessageBox.Show(CurrentTime.ToString());
        }
    }
}
