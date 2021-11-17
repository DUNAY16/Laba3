using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        byte[] arrAfterXOR;
        bool flag = false;

        void myShowToolTip(TextBox tB, byte[] arr)
        {
            string[] b = arr.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')).ToArray();
            string hexValues = string.Join(" ", b);
            toolTip_HEX.SetToolTip(tB, hexValues);
        }

        byte[] myXOR(byte[] arr_text, byte[] arr_key)
        {
            int len_text = arr_text.Length;
            int len_key = arr_key.Length;
            byte[] arr_cipher = new byte[len_text];
            for (int i = 0; i < len_text; i++)
            {
                byte p = arr_text[i];
                byte k = arr_key[i % len_key]; // mod
                byte c = (byte)(p ^ k); // XOR

                arr_cipher[i] = c;
            }
            return arr_cipher;
        }

        string myCipher(TextBox tb_text, TextBox tb_Key, TextBox tb_cipher, string cipher = "") 
        {
            string text = tb_text.Text;
            byte[] arr_text;
            if (flag == false)
            {
                arr_text = Encoding.UTF32.GetBytes(text);
                flag = true;
            }
            else
            {
                arr_text = arrAfterXOR;
                flag = false;
            }

            myShowToolTip(tb_text, arr_text); // Створити підказку

            string key = tb_Key.Text;
            byte[] arr_key = UnicodeEncoding.UTF32.GetBytes(key);
            BitArray array_key = new BitArray(UnicodeEncoding.UTF32.GetBytes(key));
            myShowToolTip(tb_Key, arr_key); // Створити підказку

            byte[] arr_cipher = myXOR(arr_text, arr_key);
            arrAfterXOR = arr_cipher;

            cipher = UnicodeEncoding.UTF32.GetString(arr_cipher);
            tb_cipher.Text = cipher;
            BitArray cipherr = new BitArray(UnicodeEncoding.UTF8.GetBytes(cipher));
            myShowToolTip(tb_cipher, arr_cipher); // Створити підказку

            return cipher;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBoxKey.Text == "")
            {
                textBoxC.Text = textBoxP.Text;
                textBoxP1.Text = textBoxP.Text;
                textBoxC1.Text = textBoxP.Text;
                //MessageBox.Show("Ви забули ввести ключ?");
                //textBox_Key_IN.Focus();
            }
            else
            {
                string cipher = myCipher(textBoxP, textBoxKey, textBoxC); // зашифрування
                textBoxP1.Text = textBoxC.Text;
                textBoxKey1.Text = textBoxKey.Text;
                myCipher(textBoxP1, textBoxKey1, textBoxC1, cipher); // розшифрування
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxP.Text = "";
            textBoxKey.Text = "";
            textBoxC.Text = "";

            textBoxP1.Text = "";
            textBoxKey1.Text = "";
            textBoxC1.Text = "";
        }
    }
}
