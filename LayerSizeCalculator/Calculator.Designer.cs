namespace LayerSizeCalculator
{
    partial class Calculator
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calculator));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPW = new System.Windows.Forms.TextBox();
            this.TxtPH = new System.Windows.Forms.TextBox();
            this.TxtKW = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtKH = new System.Windows.Forms.TextBox();
            this.TxtST = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cp = new System.Windows.Forms.Button();
            this.TxtLH = new System.Windows.Forms.TextBox();
            this.TxtLW = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cl = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "前层宽";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "前层高";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TxtPW
            // 
            this.TxtPW.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TxtPW.Location = new System.Drawing.Point(73, 12);
            this.TxtPW.Name = "TxtPW";
            this.TxtPW.Size = new System.Drawing.Size(100, 21);
            this.TxtPW.TabIndex = 2;
            this.TxtPW.Text = "12";
            this.TxtPW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IsNumber);
            this.TxtPW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectAll);
            // 
            // TxtPH
            // 
            this.TxtPH.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TxtPH.Location = new System.Drawing.Point(73, 39);
            this.TxtPH.Name = "TxtPH";
            this.TxtPH.Size = new System.Drawing.Size(100, 21);
            this.TxtPH.TabIndex = 3;
            this.TxtPH.Text = "12";
            this.TxtPH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IsNumber);
            this.TxtPH.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectAll);
            // 
            // TxtKW
            // 
            this.TxtKW.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TxtKW.Location = new System.Drawing.Point(73, 79);
            this.TxtKW.Name = "TxtKW";
            this.TxtKW.Size = new System.Drawing.Size(100, 21);
            this.TxtKW.TabIndex = 4;
            this.TxtKW.Text = "3";
            this.TxtKW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IsNumber);
            this.TxtKW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectAll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "核宽";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "核高";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TxtKH
            // 
            this.TxtKH.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TxtKH.Location = new System.Drawing.Point(73, 106);
            this.TxtKH.Name = "TxtKH";
            this.TxtKH.Size = new System.Drawing.Size(100, 21);
            this.TxtKH.TabIndex = 7;
            this.TxtKH.Text = "3";
            this.TxtKH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IsNumber);
            this.TxtKH.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectAll);
            // 
            // TxtST
            // 
            this.TxtST.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TxtST.Location = new System.Drawing.Point(73, 133);
            this.TxtST.Name = "TxtST";
            this.TxtST.Size = new System.Drawing.Size(100, 21);
            this.TxtST.TabIndex = 8;
            this.TxtST.Text = "1";
            this.TxtST.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IsNumber);
            this.TxtST.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectAll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "步长";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cp
            // 
            this.cp.Location = new System.Drawing.Point(179, 12);
            this.cp.Name = "cp";
            this.cp.Size = new System.Drawing.Size(138, 48);
            this.cp.TabIndex = 10;
            this.cp.Text = "由前层计算后层尺寸";
            this.cp.UseVisualStyleBackColor = true;
            this.cp.Click += new System.EventHandler(this.cp_Click);
            // 
            // TxtLH
            // 
            this.TxtLH.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TxtLH.Location = new System.Drawing.Point(73, 200);
            this.TxtLH.Name = "TxtLH";
            this.TxtLH.Size = new System.Drawing.Size(100, 21);
            this.TxtLH.TabIndex = 14;
            this.TxtLH.Text = "10";
            this.TxtLH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IsNumber);
            this.TxtLH.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectAll);
            // 
            // TxtLW
            // 
            this.TxtLW.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TxtLW.Location = new System.Drawing.Point(73, 173);
            this.TxtLW.Name = "TxtLW";
            this.TxtLW.Size = new System.Drawing.Size(100, 21);
            this.TxtLW.TabIndex = 13;
            this.TxtLW.Text = "10";
            this.TxtLW.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IsNumber);
            this.TxtLW.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectAll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "后层高";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "后层宽";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cl
            // 
            this.cl.Location = new System.Drawing.Point(179, 173);
            this.cl.Name = "cl";
            this.cl.Size = new System.Drawing.Size(138, 48);
            this.cl.TabIndex = 15;
            this.cl.Text = "由后层计算前层尺寸";
            this.cl.UseVisualStyleBackColor = true;
            this.cl.Click += new System.EventHandler(this.cl_Click);
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 231);
            this.Controls.Add(this.cl);
            this.Controls.Add(this.TxtLH);
            this.Controls.Add(this.TxtLW);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cp);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TxtST);
            this.Controls.Add(this.TxtKH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtKW);
            this.Controls.Add(this.TxtPH);
            this.Controls.Add(this.TxtPW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Calculator";
            this.Text = "层尺寸计算器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPW;
        private System.Windows.Forms.TextBox TxtPH;
        private System.Windows.Forms.TextBox TxtKW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtKH;
        private System.Windows.Forms.TextBox TxtST;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cp;
        private System.Windows.Forms.TextBox TxtLH;
        private System.Windows.Forms.TextBox TxtLW;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cl;
    }
}

