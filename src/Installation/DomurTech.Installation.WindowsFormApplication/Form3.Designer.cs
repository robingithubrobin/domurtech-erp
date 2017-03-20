using DomurTech.Globalization;

namespace DomurTech.Installation.WindowsFormApplication
{
    partial class Form3
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
            this.labelErrorEmail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelErrorConfirmPassword = new System.Windows.Forms.Label();
            this.textBoxConfirmPassword = new System.Windows.Forms.TextBox();
            this.labelConfirmPassword = new System.Windows.Forms.Label();
            this.labelErrorPassword = new System.Windows.Forms.Label();
            this.labelErrorUsername = new System.Windows.Forms.Label();
            this.labelErrorLastName = new System.Windows.Forms.Label();
            this.labelErrorFirstName = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.labelLastName = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBack);
            this.groupBox1.Controls.Add(this.labelErrorEmail);
            this.groupBox1.Controls.Add(this.textBoxEmail);
            this.groupBox1.Controls.Add(this.labelEmail);
            this.groupBox1.Controls.Add(this.labelErrorConfirmPassword);
            this.groupBox1.Controls.Add(this.textBoxConfirmPassword);
            this.groupBox1.Controls.Add(this.labelConfirmPassword);
            this.groupBox1.Controls.Add(this.labelErrorPassword);
            this.groupBox1.Controls.Add(this.labelErrorUsername);
            this.groupBox1.Controls.Add(this.labelErrorLastName);
            this.groupBox1.Controls.Add(this.labelErrorFirstName);
            this.groupBox1.Controls.Add(this.buttonNext);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Controls.Add(this.labelPassword);
            this.groupBox1.Controls.Add(this.textBoxLastName);
            this.groupBox1.Controls.Add(this.labelLastName);
            this.groupBox1.Controls.Add(this.textBoxUsername);
            this.groupBox1.Controls.Add(this.labelUsername);
            this.groupBox1.Controls.Add(this.textBoxFirstName);
            this.groupBox1.Controls.Add(this.labelFirstName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(738, 240);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yönetici Bilgileri";
           
            // 
            // labelErrorEmail
            // 
            this.labelErrorEmail.AutoSize = true;
            this.labelErrorEmail.Location = new System.Drawing.Point(502, 175);
            this.labelErrorEmail.Name = "labelErrorEmail";
            this.labelErrorEmail.Size = new System.Drawing.Size(0, 13);
            this.labelErrorEmail.TabIndex = 20;
          
            // 
            // labelErrorConfirmPassword
            // 
            this.labelErrorConfirmPassword.AutoSize = true;
            this.labelErrorConfirmPassword.Location = new System.Drawing.Point(502, 149);
            this.labelErrorConfirmPassword.Name = "labelErrorConfirmPassword";
            this.labelErrorConfirmPassword.Size = new System.Drawing.Size(0, 13);
            this.labelErrorConfirmPassword.TabIndex = 19;

            // 
            // labelErrorPassword
            // 
            this.labelErrorPassword.AutoSize = true;
            this.labelErrorPassword.Location = new System.Drawing.Point(502, 123);
            this.labelErrorPassword.Name = "labelErrorPassword";
            this.labelErrorPassword.Size = new System.Drawing.Size(0, 13);
            this.labelErrorPassword.TabIndex = 18;
            // 
            // labelErrorUsername
            // 
            this.labelErrorUsername.AutoSize = true;
            this.labelErrorUsername.Location = new System.Drawing.Point(502, 97);
            this.labelErrorUsername.Name = "labelErrorUsername";
            this.labelErrorUsername.Size = new System.Drawing.Size(0, 13);
            this.labelErrorUsername.TabIndex = 17;
            // 
            // labelErrorLastName
            // 
            this.labelErrorLastName.AutoSize = true;
            this.labelErrorLastName.Location = new System.Drawing.Point(503, 71);
            this.labelErrorLastName.Name = "labelErrorLastName";
            this.labelErrorLastName.Size = new System.Drawing.Size(0, 13);
            this.labelErrorLastName.TabIndex = 16;
            // 
            // labelErrorFirstName
            // 
            this.labelErrorFirstName.AutoSize = true;
            this.labelErrorFirstName.Location = new System.Drawing.Point(503, 45);
            this.labelErrorFirstName.Name = "labelErrorFirstName";
            this.labelErrorFirstName.Size = new System.Drawing.Size(0, 13);
            this.labelErrorFirstName.TabIndex = 15;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(107, 198);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(75, 23);
            this.buttonBack.TabIndex = 14;
            this.buttonBack.Text = "Geri";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(421, 198);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 13;
            this.buttonNext.Text = "İleri";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(107, 172);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(389, 20);
            this.textBoxEmail.TabIndex = 12;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(28, 175);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(32, 13);
            this.labelEmail.TabIndex = 11;
            this.labelEmail.Text = "Email";
            // 
            // textBoxConfirmPassword
            // 
            this.textBoxConfirmPassword.Location = new System.Drawing.Point(107, 146);
            this.textBoxConfirmPassword.Name = "textBoxConfirmPassword";
            this.textBoxConfirmPassword.Size = new System.Drawing.Size(389, 20);
            this.textBoxConfirmPassword.TabIndex = 10;
            // 
            // labelConfirmPassword
            // 
            this.labelConfirmPassword.AutoSize = true;
            this.labelConfirmPassword.Location = new System.Drawing.Point(28, 149);
            this.labelConfirmPassword.Name = "labelConfirmPassword";
            this.labelConfirmPassword.Size = new System.Drawing.Size(64, 13);
            this.labelConfirmPassword.TabIndex = 9;
            this.labelConfirmPassword.Text = "Şifre Tekrarı";


            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(107, 120);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(389, 20);
            this.textBoxPassword.TabIndex = 8;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(28, 123);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(28, 13);
            this.labelPassword.TabIndex = 7;
            this.labelPassword.Text = "Şifre";
           
           
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(107, 94);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(389, 20);
            this.textBoxUsername.TabIndex = 6;
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(28, 97);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(64, 13);
            this.labelUsername.TabIndex = 5;
            this.labelUsername.Text = "Kullanıcı Adı";
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(107, 68);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(389, 20);
            this.textBoxLastName.TabIndex = 4;
            // 
            // labelLastName
            // 
            this.labelLastName.AutoSize = true;
            this.labelLastName.Location = new System.Drawing.Point(28, 71);
            this.labelLastName.Name = "labelLastName";
            this.labelLastName.Size = new System.Drawing.Size(39, 13);
            this.labelLastName.TabIndex = 3;
            this.labelLastName.Text = "Soyadı";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(107, 42);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(389, 20);
            this.textBoxFirstName.TabIndex = 2;
            // 
            // labelFirstName
            // 
            this.labelFirstName.AutoSize = true;
            this.labelFirstName.Location = new System.Drawing.Point(28, 45);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(20, 13);
            this.labelFirstName.TabIndex = 1;
            this.labelFirstName.Text = "Ad";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(15, 261);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(0, 13);
            this.labelMessage.TabIndex = 0;

            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.labelMessage);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelErrorPassword;
        private System.Windows.Forms.Label labelErrorUsername;
        private System.Windows.Forms.Label labelErrorLastName;
        private System.Windows.Forms.Label labelErrorFirstName;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Label labelLastName;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Label labelErrorEmail;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelErrorConfirmPassword;
        private System.Windows.Forms.TextBox textBoxConfirmPassword;
        private System.Windows.Forms.Label labelConfirmPassword;
        private System.Windows.Forms.Button buttonBack;
    }
}