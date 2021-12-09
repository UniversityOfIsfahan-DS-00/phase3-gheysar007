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
        bool enter_value = false;

        float iCelsius, iFahrenheit, iKevin;
        char iOperation;

        bool isnumber = false;
        bool containdot = false;
        bool arithmeticafterdot = false;

        short maxstack = 0;
        LinkedStack Stack = new LinkedStack();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtStep.Text = "";
            txtDisplay.Text = "0";
            lblShowOp.Text = "";
            isnumber = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 607;
            txtDisplay.Width = 286;
            flowLayoutPanel1.Width = 286;
            flowLayoutPanel2.Visible = false;
            groupBox2.Visible = true;
            groupBox2.Location = new Point(304, 35);
        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 607;
            txtDisplay.Width = 286;
            flowLayoutPanel1.Width = 286;
            txtDisplay.Visible = true;
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel2.Visible = false;
            groupBox2.Visible = true;
            groupBox2.Location = new Point(304, 35);
            groupBox1.Visible = false;
        }

        private void scientificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 765;
            txtDisplay.Width = 448;
            flowLayoutPanel1.Width = 448;
            txtDisplay.Visible = true;
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel2.Visible = true;
            groupBox2.Visible = true;
            groupBox2.Location = new Point(466, 36);
            groupBox1.Visible = false;
        }

        private void temperatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 379;
            txtDisplay.Visible = false;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            groupBox1.Location = new Point(12, 35);
            groupBox2.Visible = false;
            groupBox1.Visible = true;
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
                if (!containdot)
                {
                    txtDisplay.Text = txtDisplay.Text + num.Text;
                    containdot = true;
                }
            }
            else
            {
                arithmeticafterdot = true;
                txtDisplay.Text = txtDisplay.Text + num.Text;
            }

            isnumber = true;
        }

        private void button_Click_1(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            txtStep.Text = "";
            lblShowOp.Text = "";
            isnumber = false;
        }

        private void buttonback_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text[txtDisplay.Text.Length - 1] == '.')
            {
                containdot = false;
            }
            if (txtDisplay.Text[txtDisplay.Text.Length - 1] == ')')
            {
                Stack.push('(');
            }
            if (txtDisplay.Text.Length > 0)
            {
                isnumber = true;
                txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1, 1);
                if (txtDisplay.Text.Length != 0)
                    switch (txtDisplay.Text[txtDisplay.Text.Length - 1])
                    {
                        case '+':
                        case '-':
                        case '×':
                        case '÷':
                        case '^':
                            isnumber = false;
                            break;
                    }
            }
            if (txtDisplay.Text == "")
            {
                txtDisplay.Text = "0";
                isnumber = false;
            }
        }

        private void arithmetic_op(object sender, EventArgs e)
        {
            Button num = (Button)sender;
            if (isnumber && txtDisplay.Text[txtDisplay.Text.Length - 1] != '.' && arithmeticafterdot
                && txtDisplay.Text[txtDisplay.Text.Length - 1] != '-' && txtDisplay.Text[txtDisplay.Text.Length - 1] != '+'
                && txtDisplay.Text[txtDisplay.Text.Length - 1] != '×' && txtDisplay.Text[txtDisplay.Text.Length - 1] != '÷')
            {
                if (txtDisplay.Text[txtDisplay.Text.Length - 1] != '(')
                {
                    txtDisplay.Text += num.Text.ToString();
                    isnumber = false;
                    containdot = false;
                }
                if (txtDisplay.Text[txtDisplay.Text.Length - 1] == '(' && num.Text == "-")
                {
                    txtDisplay.Text += num.Text.ToString();
                    isnumber = false;
                    containdot = false;
                }
            }
            if (txtDisplay.Text == "0" && num.Text == "-")
            {
                txtDisplay.Text = "-";
            }
        }

        int prec(char c)
        {
            if (c == '^')
                return 3;
            else if (c == '÷' || c == '×')
                return 2;
            else if (c == '+' || c == '-')
                return 1;
            else
                return -1;
        }

        public string infixToPostfix(string s)
        {

            LinkedStack st = new LinkedStack() ;
            string result = "";

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if (char.IsLetterOrDigit(c))
                    result += $"{c}";

                else if (c == '(')
                    st.push('(');

                else if (c == ')')
                {
                    while (st.top() != '(')
                    {
                        result += st.top();
                        st.pop();
                    }
                    st.pop();
                }

                else
                {
                    while (!st.isEmpty() && prec(s[i]) <= prec(st.top()))
                    {
                        result += st.top();
                        st.pop();
                    }
                    st.push(c);
                }
            }

            while (!st.isEmpty())
            {
                result += st.top();
                st.pop();
            }

            return result;
        }

        private void Print(doublyLinkedList<string> list)
        {
            //doublyLinkedList<string>.node<string> node1 = list.gethead();
            //doublyLinkedList<string>.node<string> node2 = list.gettail();
            //while (node1.getNext() != node2.getprev() && node1.getData() == "(" && node2.getData() == ")")
            //{
            //    node1 = node1.getNext();
            //    node2 = node2.getprev();
            //}
            //if (node1 == node2)
            //{
            //    txtStep.Text += "\n=";
            //    txtStep.Text += node1.getData();
            //    return;
            //}
            doublyLinkedList<string>.node<string> node = list.gethead().getNext();
            txtStep.Text += "\n=";
            while (node != list.gettail())
            {
                txtStep.Text += node.getData();
                node = node.getNext();
            }
        }

        private void StepByStepSoution()
        {

            //string result = infixToPostfix(txtStep.Text);

            //string finalresult = PostfixToInfix(result);
            //int s = 10;
            //s++;



            bool allowtoprint = false;
            doublyLinkedList<string> operatelist = new doublyLinkedList<string>();
            string result = "(" + txtStep.Text + ")";
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '(')
                {
                    maxstack++;
                }
                if (!char.IsNumber(result[i]) || (char.IsNumber(result[i]) && !char.IsNumber(result[i - 1])))
                {
                    operatelist.AddLast(result[i].ToString());
                }
                else
                {
                    operatelist.gettail().SetData(operatelist.gettail().getData() + result[i]);
                }
            }

            doublyLinkedList<string>.node<string> node = operatelist.gettail();
            doublyLinkedList<string>.node<string> head = operatelist.gethead();
            doublyLinkedList<string>.node<string> tail = operatelist.gettail();
            for (int i = 0; i < maxstack; i++)
            {
                while (node.getData() != "(")
                {
                    node = node.getprev();
                }
                head = node;
                while (node.getData() != ")")
                {
                    node = node.getNext();
                }
                tail = node;

                node = head.getNext();
                while (node != tail)
                {
                    if (node.getData() == "+")
                    {
                        node.getNext().SetData($"{node.getNext().getData()}");
                        node = operatelist.Deletenode(node);
                    }
                    else if (node.getData() == "-")
                    {
                        node.getNext().SetData((Convert.ToDouble(node.getNext().getData()) * (-1)).ToString());
                        node = operatelist.Deletenode(node);
                    }
                    node = node.getNext();
                }
                node = head;
                if (head == operatelist.gethead() && tail == operatelist.gettail() && operatelist.gethead() == operatelist.gettail())
                {
                    Print(operatelist);
                    break;
                }
                if (head.getNext() == tail.getprev())
                {

                    if (tail.getNext() != null && tail.getNext().getData() == "^")
                    {
                        doublyLinkedList<string>.node<string> temp = tail.getNext();
                        if (temp.getNext().getData() == "(")
                        {
                            operatelist.AddBetweenNeg(head.getNext(), (Math.Pow(Convert.ToDouble(head.getNext().getData()), Convert.ToDouble(temp.getNext().getNext().getData())).ToString()));
                        }
                        else
                        {
                            operatelist.Deletenode(temp);
                            operatelist.AddBetweenmid(head, (Math.Pow(Convert.ToDouble(head.getNext().getData()), Convert.ToDouble(tail.getNext().getData())).ToString()));
                        }
                        Print(operatelist);
                        continue;
                    }
                    head.SetData(head.getNext().getData());
                    operatelist.Deletenode(head.getNext());
                    operatelist.Deletenode(head.getNext());
                    tail = head.getNext();
                    //operatelist.AddBetween(node.getprev(), node.getNext().getData());
                    continue;
                }
                while (node.getNext() != tail)
                {
                    node = node.getNext();
                    switch (node.getData())
                    {
                        case "^":
                            operatelist.AddBetween(node.getprev(), (Math.Pow(Convert.ToDouble(node.getprev().getData()), Convert.ToDouble(node.getNext().getData())).ToString()));
                            allowtoprint = true;
                            break;

                    }
                }
                if (allowtoprint)
                {
                    Print(operatelist);
                    allowtoprint = false;
                }
                node = head;
                while (node.getNext() != tail)
                {
                    node = node.getNext();
                    switch (node.getData())
                    {
                        case "÷":
                            if (node.getNext().getData() == "(")
                            {
                                operatelist.AddBetweenNeg(node.getprev(), (Convert.ToDouble(node.getprev().getData()) / Convert.ToDouble(node.getNext().getNext().getData())).ToString());
                                tail = operatelist.gettail();
                            }
                            else
                                operatelist.AddBetween(node.getprev(), (Convert.ToDouble(node.getprev().getData()) / Convert.ToDouble(node.getNext().getData())).ToString());
                            allowtoprint = true;
                            break;
                        case "×":
                            if (node.getNext().getData() == "(")
                            {
                                operatelist.AddBetweenNeg(node.getprev(), (Convert.ToDouble(node.getprev().getData()) * Convert.ToDouble(node.getNext().getNext().getData())).ToString());
                                tail = operatelist.gettail();
                            }
                            else
                                operatelist.AddBetween(node.getprev(), (Convert.ToDouble(node.getprev().getData()) * Convert.ToDouble(node.getNext().getData())).ToString());
                            allowtoprint = true;
                            break;

                    }
                }
                if (allowtoprint)
                {
                    Print(operatelist);
                    allowtoprint = false;
                }
                node = head.getNext();
                while (node.getNext() != tail)
                {
                    node = node.getNext();
                    if(Convert.ToDouble(node.getData()) > 0)
                    {
                        operatelist.AddBetween(node.getprev(), (Convert.ToDouble(node.getprev().getData()) + Convert.ToDouble(node.getData())).ToString());
                        allowtoprint = true;
                    }
                    else
                    {
                        operatelist.AddBetween(node.getprev(), (Convert.ToDouble(node.getprev().getData()) - Convert.ToDouble(node.getData())).ToString());
                        allowtoprint = true;
                    }
                }
                if (allowtoprint)
                {
                    Print(operatelist);
                    allowtoprint = false;
                }
                int s = 0;
                s++;
            }













            //bool allsimple = false;
            //for (int i = 0; i < maxstack ; i++)
            //{
            //    doublyLinkedList<string>.node<string> node = operatelist.gettail();
            //    doublyLinkedList<string>.node<string> head = operatelist.gethead();
            //    doublyLinkedList<string>.node<string> tail = operatelist.gettail();
            //    if (!allsimple)
            //    {
            //        while (head != operatelist.gethead() && head.getNext() == tail.getprev()) 
            //        {
            //            if (head.getNext() == tail.getprev())
            //            {
            //                node = head.getprev();
            //            }
            //            while (node.getData() != "(")
            //            {
            //                node = node.getprev();
            //            }
            //            head = node;
            //            while (node.getData() != ")")
            //            {
            //                node = node.getNext();
            //            }
            //            tail = node;

            //        }
            //    }
            //    if (head == operatelist.gethead())
            //    {
            //        while (node.getData() != "(")
            //        {
            //            node = node.getprev();
            //        }
            //        head = node;
            //        while (node.getData() != ")")
            //        {
            //            node = node.getNext();
            //        }
            //        tail = node;
            //        allsimple = true;
            //    }
            //    if(txtStep.Text.EndsWith(operatelist.gethead().getNext().getData()))
            //    {
            //        break;
            //    }
            //    doublyLinkedList<string>.node<string> node1 = operatelist.gethead();
            //    doublyLinkedList<string>.node<string> node2 = operatelist.gettail();
            //    while (node1.getNext() != node2.getprev() && node1.getData() == "(" && node2.getData() == ")")
            //    {
            //        node1 = node1.getNext();
            //        node2 = node2.getprev();
            //    }
            //    if (node1 == node2)
            //    {
            //        txtStep.Text += "\n=";
            //        txtStep.Text += node1.getData();
            //        break;
            //    }
            //    node = head;
            //    while (node.getNext() != tail)
            //    {
            //        node = node.getNext();
            //        switch (node.getData())
            //        {
            //            case "^":
            //                operatelist.AddBetween(node.getprev(), (Math.Pow(Convert.ToDouble(node.getprev().getData()), Convert.ToDouble(node.getNext().getData())).ToString()));
            //                allowtoprint = true;
            //                break;

            //        }
            //    }
            //    if (allowtoprint)
            //    {
            //        Print(operatelist);
            //        allowtoprint = false;
            //    }
            //    if (txtStep.Text.EndsWith(operatelist.gethead().getNext().getData()))
            //    {
            //        break;
            //    }
            //    node1 = operatelist.gethead();
            //    node2 = operatelist.gettail();
            //    while (node1.getNext() != node2.getprev() && node1.getData() == "(" && node2.getData() == ")")
            //    {
            //        node1 = node1.getNext();
            //        node2 = node2.getprev();
            //    }
            //    if (node1 == node2)
            //    {
            //        txtStep.Text += "\n=";
            //        txtStep.Text += node1.getData();
            //        break;
            //    }
            //    node = head;
            //    while (node.getNext() != tail)
            //    {
            //        node = node.getNext();
            //        switch (node.getData())
            //        {
            //            case "÷":
            //                if (node.getNext().getData() == "(")
            //                {
            //                    operatelist.AddBetweenNeg(node.getprev(), (Convert.ToDouble(node.getprev().getData()) / Convert.ToDouble(node.getNext().getNext().getData())).ToString());
            //                    tail = operatelist.gettail();
            //                }
            //                else
            //                    operatelist.AddBetween(node.getprev(), (Convert.ToDouble(node.getprev().getData()) / Convert.ToDouble(node.getNext().getData())).ToString());
            //                allowtoprint = true;
            //                break;
            //            case "×":
            //                if(node.getNext().getData() == "(")
            //                {
            //                    operatelist.AddBetweenNeg(node.getprev(), (Convert.ToDouble(node.getprev().getData()) * Convert.ToDouble(node.getNext().getNext().getData())).ToString());
            //                    tail = operatelist.gettail();
            //                }
            //                else
            //                     operatelist.AddBetween(node.getprev(), (Convert.ToDouble(node.getprev().getData()) * Convert.ToDouble(node.getNext().getData())).ToString());
            //                allowtoprint = true;
            //                break;

            //        }
            //    }
            //    if (allowtoprint)
            //    {
            //        Print(operatelist);
            //        allowtoprint = false;
            //    }
            //    if (txtStep.Text.EndsWith(operatelist.gethead().getNext().getData()))
            //    {
            //        break;
            //    }
            //    node1 = operatelist.gethead();
            //    node2 = operatelist.gettail();
            //    while (node1.getNext() != node2.getprev() && node1.getData() == "(" && node2.getData() == ")")
            //    {
            //        node1 = node1.getNext();
            //        node2 = node2.getprev();
            //    }
            //    if (node1 == node2)
            //    {
            //        txtStep.Text += "\n=";
            //        txtStep.Text += node1.getData();
            //        break;
            //    }
            //    node = head;
            //    double sub = 0;
            //    bool neg = false;
            //    while (node.getNext() != tail)
            //    {
            //        node = node.getNext();
            //        switch (node.getData())
            //        {
            //            case "+":
            //                operatelist.AddBetween(node.getprev(), (Convert.ToDouble(node.getprev().getData()) + Convert.ToDouble(node.getNext().getData())).ToString());
            //                allowtoprint = true;
            //                break;
            //            case "-":
            //                if (node.getprev().getData() == "(")
            //                {
            //                    node.getNext().SetData((Convert.ToDouble(node.getNext().getData()) * (-1)).ToString());
            //                    operatelist.Deletenode(node);
            //                    node = head.getNext();
            //                }
            //                else
            //                {
            //                    operatelist.AddBetween(node.getprev(), (Convert.ToDouble(node.getprev().getData()) - Convert.ToDouble(node.getNext().getData())).ToString());
            //                }
            //                neg = true;
            //                allowtoprint = true;
            //                break;
            //        }
            //    }
            //    if(head.getNext().getData() != "(")
            //         sub = Convert.ToDouble(head.getNext().getData());
            //    node = head.getprev();
            //    if (txtStep.Text.EndsWith(operatelist.gethead().getNext().getData()))
            //    {
            //        break;
            //    }
            //    node1 = operatelist.gethead();
            //    node2 = operatelist.gettail();
            //    while (node1.getNext() != node2.getprev() && node1.getData() == "(" && node2.getData() == ")")
            //    {
            //        node1 = node1.getNext();
            //        node2 = node2.getprev();
            //    }
            //    if (node1 == node2)
            //    {
            //        txtStep.Text += "\n=";
            //        txtStep.Text += node1.getData();
            //        break;
            //    }
            //    if (i != maxstack && sub > 0)
            //    {
            //        operatelist.AddBetween(head, head.getNext().getData());
            //    }
            //    else if (sub < 0)
            //    {
            //        if (allowtoprint)
            //        {
            //            Print(operatelist);
            //            allowtoprint = false;

            //        }
            //        if (neg)
            //        {
            //            continue;
            //        }
            //        doublyLinkedList<string>.node<string> temp = node.getprev();
            //        switch (node.getData())
            //        {
            //            case "^":
            //                operatelist.AddBetweenNeg(node.getprev(), (Math.Pow(Convert.ToDouble(node.getprev().getData()), Convert.ToDouble(node.getNext().getNext().getData())).ToString()));
            //                allowtoprint = true;
            //                break;
            //            case "÷":
            //                operatelist.AddBetweenNeg(node.getprev(), (Convert.ToDouble(node.getprev().getData()) / Convert.ToDouble(node.getNext().getNext().getData())).ToString());
            //                allowtoprint = true;
            //                break;
            //            case "×":
            //                operatelist.AddBetweenNeg(node.getprev(), (Convert.ToDouble(node.getprev().getData()) * Convert.ToDouble(node.getNext().getNext().getData())).ToString());
            //                allowtoprint = true;
            //                break;
            //            case "+":
            //                operatelist.AddBetweenNeg(node.getprev(), (Convert.ToDouble(node.getprev().getData()) + Convert.ToDouble(node.getNext().getNext().getData())).ToString());
            //                allowtoprint = true;
            //                break;
            //            case "-":
            //                operatelist.AddBetweenNeg(node.getprev(), (Convert.ToDouble(node.getprev().getData()) - Convert.ToDouble(node.getNext().getNext().getData())).ToString());
            //                allowtoprint = true;
            //                break;
            //        }
            //        if (temp.getprev().getData() == "-" && Convert.ToDouble(temp.getData()) < 0)
            //        {
            //            temp.getprev().SetData("+");
            //            temp.SetData((Convert.ToDouble(temp.getData()) * (-1)).ToString());
            //        }

            //    }
            //    if (allowtoprint)
            //    {
            //        Print(operatelist);
            //        allowtoprint = false;
            //    }

            //}
            //int s = 0;
            //s++;
        }

        private void btnequal_click(object sender, EventArgs e)
        {
            lblShowOp.Text = "";
            for (int i = 0; i < 2; i++)
                switch (txtDisplay.Text[txtDisplay.Text.Length - 1])
                {
                    case '+':
                    case '-':
                    case '×':
                    case '÷':
                    case '.':
                        txtDisplay.Text = txtDisplay.Text.Remove(txtDisplay.Text.Length - 1, 1);
                        break;
                    case '^':
                        txtDisplay.Text = "Syntax Error";
                        break;
                }
            if (!Stack.isEmpty())
            {
                int sizeofstack = Stack.size();
                for (int i = 0; i < sizeofstack; i++)
                {
                    txtDisplay.Text += ")";
                    Stack.pop();
                }
            }
            txtStep.Text = txtDisplay.Text;
            txtDisplay.Text = "0";
            isnumber = false;
            // if(txtDisplay.Text != "0" && txtDisplay.Text != "." && txtDisplay.Text != "-")
            StepByStepSoution();
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
            txtStep.Text = Convert.ToString(sqrt);
            txtDisplay.Text = "";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            double sin = double.Parse(txtDisplay.Text);
            lblShowOp.Text = Convert.ToString("Sin" + "(" + txtDisplay.Text + ")");
            sin = Math.Sin(sin);
            txtDisplay.Text = "";
            txtStep.Text = Convert.ToString(sin);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            double Cos = double.Parse(txtDisplay.Text);
            lblShowOp.Text = Convert.ToString("Cos" + "(" + txtDisplay.Text + ")");
            Cos = Math.Cos(Cos);
            txtDisplay.Text = "";
            txtStep.Text = Convert.ToString(Cos);
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
                    txtConvert.Text = (((iFahrenheit - 32) * 5) / 9).ToString();
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

        private void paranthes1(object sender, EventArgs e)
        {
            if (!isnumber || txtDisplay.Text[txtDisplay.Text.Length - 1] == '(')
            {

                if (txtDisplay.Text == "0")
                {
                    txtDisplay.Text = "(";
                }
                else
                {
                    Button num = (Button)sender;
                    txtDisplay.Text += num.Text.ToString();
                }
                Stack.push('(');
            }
            isnumber = true;
        }

        private void paranthes2(object sender, EventArgs e)
        {
            char comp = txtDisplay.Text[txtDisplay.Text.Length - 1];
            if (!Stack.isEmpty() && comp != '+' && comp != '-' && comp != '×' && comp != '÷' && txtDisplay.Text[txtDisplay.Text.Length - 1] != '(')
            {
                Button num = (Button)sender;
                txtDisplay.Text += num.Text.ToString();
                Stack.pop();
            }
            isnumber = true;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            double Tan = double.Parse(txtDisplay.Text);
            lblShowOp.Text = Convert.ToString("Tan" + "(" + txtDisplay.Text + ")");
            Tan = Math.Tan(Tan);
            txtDisplay.Text = "";
            txtStep.Text = Convert.ToString(Tan);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            double a;
            a = Convert.ToDouble(txtDisplay.Text) / Convert.ToDouble(100);
            txtDisplay.Text = "";
            txtStep.Text = Convert.ToString(a);
        }
    }
}
