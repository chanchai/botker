using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinEventReceiver
{
    public partial class Form1 : Form
    {
        override protected void OnClick(EventArgs e)
        {
            MessageBox.Show("test");
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("button" + sender.ToString());
            
        }

        override protected void OnClosed(EventArgs e)
        {
            MessageBox.Show("Closed");
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.textBox1.Text = e.X.ToString();
            this.textBox2.Text = e.Y.ToString();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
