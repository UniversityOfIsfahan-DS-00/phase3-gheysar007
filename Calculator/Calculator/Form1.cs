using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double results = 0;
        string operation = "";
        bool enter_value = false;

        float iCelsius, iFahrenheit, iKevin;
        char iOperation;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            lblShowOp.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 326;
            txtDisplay.Width = 286;
        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 326;
            txtDisplay.Width = 286;
        }

        private void scientificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 488;
            txtDisplay.Width = 448;
        }

        private void temperatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 851;
            txtDisplay.Width = 448;
        }

        private void unitConversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 851;
            txtDisplay.Width = 448;
        }

        private void multiplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 851;
            txtDisplay.Width = 448;
        }

        private void button_click(object sender, EventArgs e)
        {
           if ((txtDisplay.Text == "0") || (enter_value))
            {
                txtDisplay.Text = "";
            }
            enter_value = false;
            Button num = (Button)sender;
            if (num.Text == ".")
            {
                if (!txtDisplay.Text.Contains("."))
                {
                    txtDisplay.Text = txtDisplay.Text + num.Text;
                }
            }
            else
                txtDisplay.Text = txtDisplay.Text + num.Text;
        }

        private void button_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            lblShowOp.Text = "";
        }

        private void buttonback_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 0)
            {
                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1, 1);
            }
            if (txtDisplay.Text == "")
            {
                txtDisplay.Text = "0"; 
            }
        }

        private void arithmetic_op(object sender, EventArgs e)
        {
            Button num = (Button)sender;
            string temp = num.Text;
            if (num.Text == "×")
            {
                temp = "*";
            }
            else if (num.Text == "÷")
            {
                temp = "/";
            }
            operation = temp;
            results = double.Parse(txtDisplay.Text);
            txtDisplay.Text = "";
            lblShowOp.Text = Convert.ToString(results) + " " + operation;
        }

        private void btnequal_click(object sender, EventArgs e)
        {
            lblShowOp.Text = "";
            switch (operation)
            {
                case "+":
                    txtDisplay.Text = (results + double.Parse(txtDisplay.Text)).ToString();
                    break;
                case "-":
                    txtDisplay.Text = (results - double.Parse(txtDisplay.Text)).ToString();
                    break;
                case "*":
                    txtDisplay.Text = (results * double.Parse(txtDisplay.Text)).ToString();
                    break;
                case "/":
                    txtDisplay.Text = (results / double.Parse(txtDisplay.Text)).ToString();
                    break;
                case "^":
                    txtDisplay.Text = (Math.Pow(results, double.Parse(txtDisplay.Text))).ToString();
                    break;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "3.141592653589976323";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            double ilog = double.Parse(txtDisplay.Text);
            lblShowOp.Text = Convert.ToString("log" + "(" + txtDisplay.Text + ")");
            ilog = Math.Log10(ilog);
            txtDisplay.Text = Convert.ToString(ilog);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            double sqrt = double.Parse(txtDisplay.Text);
            lblShowOp.Text = Convert.ToString("√" + "(" + txtDisplay.Text + ")");
            sqrt = Math.Sqrt(sqrt);
            txtDisplay.Text = Convert.ToString(sqrt);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            double sin = double.Parse(txtDisplay.Text);
            lblShowOp.Text = Convert.ToString("Sin" + "(" + txtDisplay.Text + ")");
            sin = Math.Sin(sin);
            txtDisplay.Text = Convert.ToString(sin);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            double Cos = double.Parse(txtDisplay.Text);
            lblShowOp.Text = Convert.ToString("Cos" + "(" + txtDisplay.Text + ")");
            Cos = Math.Cos(Cos);
            txtDisplay.Text = Convert.ToString(Cos);
        }

        private void rbCeltoFah_CheckedChanged(object sender, EventArgs e)
        {
            iOperation = 'C';
        }

        private void rbFahtoCel_CheckedChanged(object sender, EventArgs e)
        {
            iOperation = 'F';

        }

        private void rbKevin_CheckedChanged(object sender, EventArgs e)
        {
            iOperation = 'K';

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            switch (iOperation)
            {
                case 'C':
                    iCelsius = float.Parse(txttemp.Text);
                    txtConvert.Text = (((9 * iCelsius) / 5) + 32).ToString();
                    break;
                case 'F':
                    iFahrenheit = float.Parse(txttemp.Text);
                    txtConvert.Text = (((iFahrenheit - 32) * 5) /9).ToString();
                    break;
                case 'K':
                    iKevin = float.Parse(txttemp.Text);
                    txtConvert.Text = (((9 * iKevin) / 5) + 305.15).ToString();
                    break;
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            txtConvert.Clear();
            txttemp.Clear();
            rbCeltoFah.Checked = false;
            rbFahtoCel.Checked = false;
            rbKevin.Checked = false;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            double Tan = double.Parse(txtDisplay.Text);
            lblShowOp.Text = Convert.ToString("Tan" + "(" + txtDisplay.Text + ")");
            Tan = Math.Tan(Tan);
            txtDisplay.Text = Convert.ToString(Tan);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            double a;
            a = Convert.ToDouble(txtDisplay.Text) / Convert.ToDouble(100);
            txtDisplay.Text = Convert.ToString(a);
        }
    }
}
