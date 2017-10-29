using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORDesignerWalktrough
{
    public partial class Form1 : Form
    {
        private Baan4DataContext baan4datcont = new Baan4DataContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ttccom020100BindingSource.DataSource = baan4datcont.ttccom020100;
        }
    }
}
