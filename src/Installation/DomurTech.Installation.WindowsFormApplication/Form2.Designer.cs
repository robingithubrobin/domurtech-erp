﻿namespace DomurTech.Installation.WindowsFormApplication
{
    partial class Form2
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
            this.labelMessage = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBack = new System.Windows.Forms.Button();
            this.labelErrorPassword = new System.Windows.Forms.Label();
            this.labelErrorUserId = new System.Windows.Forms.Label();
            this.labelErrorInitialCatalog = new System.Windows.Forms.Label();
            this.labelErrorDataSource = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxInitialCatalog = new System.Windows.Forms.TextBox();
            this.labelInitialCatalog = new System.Windows.Forms.Label();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.labelUserId = new System.Windows.Forms.Label();
            this.textBoxDataSource = new System.Windows.Forms.TextBox();
            this.labelDataSource = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout(); 
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBack);
            this.groupBox1.Controls.Add(this.labelErrorPassword);
            this.groupBox1.Controls.Add(this.labelErrorUserId);
            this.groupBox1.Controls.Add(this.labelErrorInitialCatalog);
            this.groupBox1.Controls.Add(this.labelErrorDataSource);
            this.groupBox1.Controls.Add(this.buttonNext);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Controls.Add(this.labelPassword);
            this.groupBox1.Controls.Add(this.textBoxInitialCatalog);
            this.groupBox1.Controls.Add(this.labelInitialCatalog);
            this.groupBox1.Controls.Add(this.textBoxUserId);
            this.groupBox1.Controls.Add(this.labelUserId);
            this.groupBox1.Controls.Add(this.textBoxDataSource);
            this.groupBox1.Controls.Add(this.labelDataSource);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(738, 210);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Veritabanına Bağlan";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(12, 229);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(0, 13);
            this.labelMessage.TabIndex = 3;
           
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(107, 147);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 15;
            this.buttonBack.Text = "Geri";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // labelErrorPassword
            // 
            this.labelErrorPassword.AutoSize = true;
            this.labelErrorPassword.Location = new System.Drawing.Point(502, 123);
            this.labelErrorPassword.Name = "labelErrorPassword";
            this.labelErrorPassword.Size = new System.Drawing.Size(0, 13);
            this.labelErrorPassword.TabIndex = 14;
            // 
            // labelErrorUserId
            // 
            this.labelErrorUserId.AutoSize = true;
            this.labelErrorUserId.Location = new System.Drawing.Point(502, 97);
            this.labelErrorUserId.Name = "labelErrorUserId";
            this.labelErrorUserId.Size = new System.Drawing.Size(0, 13);
            this.labelErrorUserId.TabIndex = 13;
            // 
            // labelErrorInitialCatalog
            // 
            this.labelErrorInitialCatalog.AutoSize = true;
            this.labelErrorInitialCatalog.Location = new System.Drawing.Point(503, 71);
            this.labelErrorInitialCatalog.Name = "labelErrorInitialCatalog";
            this.labelErrorInitialCatalog.Size = new System.Drawing.Size(0, 13);
            this.labelErrorInitialCatalog.TabIndex = 12;
            // 
            // labelErrorDataSource
            // 
            this.labelErrorDataSource.AutoSize = true;
            this.labelErrorDataSource.Location = new System.Drawing.Point(503, 45);
            this.labelErrorDataSource.Name = "labelErrorDataSource";
            this.labelErrorDataSource.Size = new System.Drawing.Size(0, 13);
            this.labelErrorDataSource.TabIndex = 11;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(420, 147);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 10;
            this.buttonNext.Text = "İleri";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(107, 120);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.ReadOnly = true;
            this.textBoxPassword.Size = new System.Drawing.Size(389, 20);
            this.textBoxPassword.TabIndex = 9;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(28, 123);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(28, 13);
            this.labelPassword.TabIndex = 8;
            this.labelPassword.Text = "Şifre";
            // 
            // textBoxInitialCatalog
            // 
            this.textBoxInitialCatalog.Location = new System.Drawing.Point(107, 68);
            this.textBoxInitialCatalog.Name = "textBoxInitialCatalog";
            this.textBoxInitialCatalog.ReadOnly = true;
            this.textBoxInitialCatalog.Size = new System.Drawing.Size(389, 20);
            this.textBoxInitialCatalog.TabIndex = 7;
            // 
            // labelInitialCatalog
            // 
            this.labelInitialCatalog.AutoSize = true;
            this.labelInitialCatalog.Location = new System.Drawing.Point(28, 71);
            this.labelInitialCatalog.Name = "labelInitialCatalog";
            this.labelInitialCatalog.Size = new System.Drawing.Size(54, 13);
            this.labelInitialCatalog.TabIndex = 6;
            this.labelInitialCatalog.Text = "Veritabanı";
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Location = new System.Drawing.Point(107, 94);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.ReadOnly = true;
            this.textBoxUserId.Size = new System.Drawing.Size(389, 20);
            this.textBoxUserId.TabIndex = 3;
            // 
            // labelUserId
            // 
            this.labelUserId.AutoSize = true;
            this.labelUserId.Location = new System.Drawing.Point(28, 97);
            this.labelUserId.Name = "labelUserId";
            this.labelUserId.Size = new System.Drawing.Size(64, 13);
            this.labelUserId.TabIndex = 2;
            this.labelUserId.Text = "Kullanıcı Adı";
            // 
            // textBoxDataSource
            // 
            this.textBoxDataSource.Location = new System.Drawing.Point(107, 42);
            this.textBoxDataSource.Name = "textBoxDataSource";
            this.textBoxDataSource.ReadOnly = true;
            this.textBoxDataSource.Size = new System.Drawing.Size(389, 20);
            this.textBoxDataSource.TabIndex = 1;
            // 
            // labelDataSource
            // 
            this.labelDataSource.AutoSize = true;
            this.labelDataSource.Location = new System.Drawing.Point(28, 45);
            this.labelDataSource.Name = "labelDataSource";
            this.labelDataSource.Size = new System.Drawing.Size(44, 13);
            this.labelDataSource.TabIndex = 0;
            this.labelDataSource.Text = "Sunucu";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelErrorPassword;
        private System.Windows.Forms.Label labelErrorUserId;
        private System.Windows.Forms.Label labelErrorInitialCatalog;
        private System.Windows.Forms.Label labelErrorDataSource;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxInitialCatalog;
        private System.Windows.Forms.Label labelInitialCatalog;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.Label labelUserId;
        private System.Windows.Forms.TextBox textBoxDataSource;
        private System.Windows.Forms.Label labelDataSource;
        private System.Windows.Forms.Button buttonBack;
    }
}