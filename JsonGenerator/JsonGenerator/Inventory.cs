using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlowFishCS;

namespace JsonGenerator
{
    public partial class Inventory : Form
    {
        BlowFish bf = new BlowFish("04B915BA43FEB5B6");
        const string quote = "\"";
        public Inventory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string jsonFile = "";
            jsonFile = quote + @"{\" + quote + @"Level\" + quote + ":" + @"\" + quote + bf.Encrypt_CBC("0") + @"\" + quote + ",";
            jsonFile += @"\" + quote + @"World\" + quote + ":" + @"\" + quote + bf.Encrypt_CBC("0") + @"\" + quote + ",";
            jsonFile += @"\" + quote + @"Color blind mode\" + quote + ":" + @"\" + quote + bf.Encrypt_CBC("1") + @"\" + quote + ",";
            jsonFile += @"\" + quote + @"Tutorial completed\" + quote + ":" + @"\" + quote + bf.Encrypt_CBC("0") + @"\" + quote + ",";
            jsonFile += @"\" + quote + @"Completed Levels\" + quote + ":" + @"\" + quote + bf.Encrypt_CBC("") + @"\" + quote + ",";
            jsonFile += @"\" + quote + @"Cinematic watched\" + quote + ":" + @"\" + quote + bf.Encrypt_CBC("0") + @"\" + quote;
            jsonFile += "]}" + quote;
            richTextBox1.Text = jsonFile;
        }
    }
}
