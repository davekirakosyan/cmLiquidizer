using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BlowFishCS;

namespace JsonGenerator
{
    public partial class Form1 : Form
    {
        BlowFish bf = new BlowFish("04B915BA43FEB5B6");
        const string quote = "\"";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] names = nameText.Text.Split(',');
            string[] bought = boughtText.Text.Split(',');
            string[] selected = selectedText.Text.Split(',');
            string[] price = priceText.Text.Split(',');
            //{\"Items\":[{\"name\":\"red-\n elixire\",\"bought\":true,\"selected\":true,\"price\":100},{\"name\":\" yellow- \nelixire\",\"bought\":false,\"selected\":false,\"price\":100},{\"name\":\"orange- \nelixire \",\"bought\":false,\"selected\":false,\"price\":200}]}"
            string jsonFile = quote + @"{\" + quote + @"Items\" + quote + @":[";
            if (!(names.Length == bought.Length && names.Length == selected.Length && names.Length == price.Length))
            {
                MessageBox.Show("Inputs are wrong");
            }
            else
            {
                for (int i = 0; i < names.Length; i++)
                {
                    jsonFile += "{";
                    jsonFile += @"\" + quote + @"name\" + quote + ":";
                    jsonFile += @"\" + quote + bf.Encrypt_CBC(names[i]) + @"\" + quote + ",";
                    jsonFile += @"\" + quote + @"bought\" + quote + ":";
                    jsonFile += @"\" + quote + bf.Encrypt_CBC(bought[i]) + @"\" + quote + ",";
                    jsonFile += @"\" + quote + @"selected\" + quote + ":";
                    jsonFile += @"\" + quote + bf.Encrypt_CBC(selected[i]) + @"\" + quote + ",";
                    jsonFile += @"\" + quote + @"price\" + quote + ":";
                    jsonFile += @"\" + quote + bf.Encrypt_CBC(price[i]) + @"\" + quote;
                    jsonFile += "}";
                    if (i < names.Length - 1)
                    {
                        jsonFile += ",";
                    }
                }
                jsonFile += "]}" + quote;
            }
            richTextBox1.Text = jsonFile;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            MainScreen mainScreen = new MainScreen();
            mainScreen.Show();
            this.Hide();
        }
    }
}
