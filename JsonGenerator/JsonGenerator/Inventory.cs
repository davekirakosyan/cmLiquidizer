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
            jsonFile = "{Level:" + bf.Encrypt_CBC("0") + ",";
            jsonFile += "World:" + bf.Encrypt_CBC("0") + ",";
            jsonFile += "Color blind mode:" + bf.Encrypt_CBC("1") + ",";
            jsonFile += "Tutorial completed:" + bf.Encrypt_CBC("0") + ",";
            jsonFile += "Completed Levels:" + bf.Encrypt_CBC("") + ",";
            jsonFile += "Cinematic watched:" + bf.Encrypt_CBC("0") + @"\" + "]}";
            richTextBox1.Text = jsonFile;
        }
    }
}
