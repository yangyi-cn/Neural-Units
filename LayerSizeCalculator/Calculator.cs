using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LayerSizeCalculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }

        private void IsNumber(object sender, KeyPressEventArgs e)
        {
            char input = e.KeyChar;

            if (input == '\b')
            {
                e.Handled = false;
            }
            else if (Char.IsDigit(input))
            {
                TextBox textBox = (TextBox)sender;

                if (textBox.Text.Length >= 10)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void SelectAll(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            textBox.SelectAll();
        }

        private void cp_Click(object sender, EventArgs e)
        {
            double pw = double.Parse(TxtPW.Text);
            double ph = double.Parse(TxtPH.Text);

            double kw = double.Parse(TxtKW.Text);
            double kh = double.Parse(TxtKH.Text);
            double st = double.Parse(TxtST.Text);

            double lw = (pw - kw) / st + 1;
            double lh = (ph - kh) / st + 1;

            TxtLW.Text = lw.ToString();
            TxtLH.Text = lh.ToString();
        }

        private void cl_Click(object sender, EventArgs e)
        {
            double lw = double.Parse(TxtLW.Text);
            double lh = double.Parse(TxtLH.Text);

            double kw = double.Parse(TxtKW.Text);
            double kh = double.Parse(TxtKH.Text);
            double st = double.Parse(TxtST.Text);

            double pw = (lw - 1) * st + kw;
            double ph = (lh - 1) * st + kh;

            TxtPW.Text = pw.ToString();
            TxtPH.Text = ph.ToString();
        }
    }
}
