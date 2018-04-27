using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryptoApp
{
    public partial class CryptFrm : Form
    {
        public CryptFrm()
        {
            InitializeComponent();
        }
                       
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(textBox1.Text == string.Empty))
                {
                    string strEncoded = String.Empty;

                    if (DecryptRb.Checked)
                    {
                        strEncoded = Util.CryptCls.decryptString(textBox1.Text);
                        textBox2.Text = strEncoded;
                    }

                    if (EncryptRb.Checked)
                    {
                        strEncoded = Util.CryptCls.encryptString(textBox1.Text);
                        textBox2.Text = strEncoded;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a warning: "+ Environment.NewLine + ex.Message, "Warning", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {
                Clipboard.SetDataObject(textBox2.Text);
            }
            else {
                MessageBox.Show("There is no string to copy!", "Advice",MessageBoxButtons.OK);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            //Clipboard.Clear();
        }
    }
}
