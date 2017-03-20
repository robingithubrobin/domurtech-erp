using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using DomurTech.ERP.Data.Access.EntityFramework;
using DomurTech.ERP.Data.Entities.Concrete;
using DomurTech.Exceptions;
using DomurTech.Globalization;
using DomurTech.Installation.Common.Installlers;
using DomurTech.Installation.Common.Models;
using DomurTech.Installation.Common.ValidationRules;
using DomurTech.Validation.FluentValidation;

namespace DomurTech.Installation.WindowsFormApplication
{
    public partial class Form2 : Form
    {
        private LanguageInstaller _languageInstaller;

        public Form2()
        {
            InitializeComponent();
            var connectionString = ConfigurationManager.ConnectionStrings["InstallationDatabaseContext"].ConnectionString;
            var keyValuePairs = connectionString.Split(';').Select(s => s.Split('=')).Select(connectionStringArray => new KeyValuePair<string, string>(connectionStringArray[0].Trim(), connectionStringArray[1].Trim())).ToList();

            var model = new DatabaseConnectionModel
            {
                DataSource = keyValuePairs.FirstOrDefault(x => x.Key == "Data Source").Value,
                InitialCatalog = keyValuePairs.FirstOrDefault(x => x.Key == "Initial Catalog").Value,
                UserId = keyValuePairs.FirstOrDefault(x => x.Key == "User ID").Value,
                Password = keyValuePairs.FirstOrDefault(x => x.Key == "Password").Value,
                Message = ""
            };

            textBoxDataSource.Text = model.DataSource;
            textBoxInitialCatalog.Text = model.InitialCatalog;
            textBoxUserId.Text = model.UserId;
            textBoxPassword.Text = model.Password;

            
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            Hide();
            var form1= new Form1();
            form1.Closed += (s, args) => Close();
            form1.Show();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            var model = new DatabaseConnectionModel
            {
                DataSource = textBoxDataSource.Text,
                InitialCatalog = textBoxInitialCatalog.Text,
                UserId = textBoxUserId.Text,
                Password = textBoxPassword.Text
            };

            try
            {
                var validator = new FluentBaseValidator<DatabaseConnectionModel, DatabaseConnectionRules>(model);
                var validationResults = validator.Validate();
                if (!validator.IsValid)
                {
                    throw new CustomValidationException(Messages.DangerInvalidEntitiy)
                    {
                        ValidationResult = validationResults
                    };
                }

                using (var context = new InstallationDatabaseContext())
                {
                    _languageInstaller = new LanguageInstaller(new Repository<Language>(context));
                    Form3 form3;
                    if (_languageInstaller.Exists())
                    {
                        Hide();
                        form3 = new Form3();
                        form3.Closed += (s, args) => Close();
                        form3.Show();
                    }
                    labelMessage.Text = _languageInstaller.Set();
                    Hide();
                    form3 = new Form3();
                    form3.Closed += (s, args) => Close();
                    form3.Show();
                }


                    labelErrorDataSource.Visible = false;
                labelErrorInitialCatalog.Visible = false;
                labelErrorUserId.Visible = false;
                labelErrorPassword.Visible = false;
            }
            catch (CustomValidationException exception)
            {
                var validationResult = exception.ValidationResult;
                labelErrorDataSource.Text = validationResult.FirstOrDefault(x => x.PropertyName == "DataSource")?.ErrorMessage;
                labelErrorInitialCatalog.Text = validationResult.FirstOrDefault(x => x.PropertyName == "InitialCatalog")?.ErrorMessage;
                labelErrorUserId.Text = validationResult.FirstOrDefault(x => x.PropertyName == "UserId")?.ErrorMessage;
                labelErrorPassword.Text = validationResult.FirstOrDefault(x => x.PropertyName == "Password")?.ErrorMessage;

            }

            catch (Exception exception)
            {
                model.Message = exception.ToString();
                labelMessage.Text = model.Message;

            }
        }

       
    }
}
