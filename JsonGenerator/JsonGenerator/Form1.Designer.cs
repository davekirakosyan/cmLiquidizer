namespace JsonGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.boughtText = new System.Windows.Forms.TextBox();
            this.selectedText = new System.Windows.Forms.TextBox();
            this.priceText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label2.Location = new System.Drawing.Point(11, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "bought:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label3.Location = new System.Drawing.Point(11, 132);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "selected:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.label4.Location = new System.Drawing.Point(11, 195);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "price:";
            // 
            // nameText
            // 
            this.nameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.nameText.Location = new System.Drawing.Point(133, 5);
            this.nameText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(995, 35);
            this.nameText.TabIndex = 4;
            // 
            // boughtText
            // 
            this.boughtText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.boughtText.Location = new System.Drawing.Point(133, 67);
            this.boughtText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.boughtText.Name = "boughtText";
            this.boughtText.Size = new System.Drawing.Size(995, 35);
            this.boughtText.TabIndex = 5;
            // 
            // selectedText
            // 
            this.selectedText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.selectedText.Location = new System.Drawing.Point(133, 130);
            this.selectedText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.selectedText.Name = "selectedText";
            this.selectedText.Size = new System.Drawing.Size(995, 35);
            this.selectedText.TabIndex = 6;
            // 
            // priceText
            // 
            this.priceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.priceText.Location = new System.Drawing.Point(133, 193);
            this.priceText.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.priceText.Name = "priceText";
            this.priceText.Size = new System.Drawing.Size(995, 35);
            this.priceText.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(388, 418);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(359, 63);
            this.button1.TabIndex = 8;
            this.button1.Text = "Generate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(16, 243);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1112, 150);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 504);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.priceText);
            this.Controls.Add(this.selectedText);
            this.Controls.Add(this.boughtText);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.TextBox boughtText;
        private System.Windows.Forms.TextBox selectedText;
        private System.Windows.Forms.TextBox priceText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

