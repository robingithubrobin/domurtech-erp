using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using DomurTech.Exceptions;
using DomurTech.Globalization;
using DomurTech.Installation.Common.Models;
using DomurTech.Installation.Common.ValidationRules;
using DomurTech.Validation.FluentValidation;

namespace DomurTech.Installation.WindowsFormApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection) config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["InstallationDatabaseContext"].ConnectionString =
                    $"Data Source={model.DataSource}; Initial Catalog={model.InitialCatalog}; User ID={model.UserId}; Password={model.Password}";
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                labelErrorDataSource.Visible = false;
                labelErrorInitialCatalog.Visible = false;
                labelErrorUserId.Visible = false;
                labelErrorPassword.Visible = false;
                this.Hide();
                var form2 = new Form2();
                form2.Closed += (s, args) => this.Close();
                form2.Show();
            }

            catch (CustomValidationException exception)
            {
                var validationResult = exception.ValidationResult;
                labelErrorDataSource.Text = validationResult.FirstOrDefault(x=>x.PropertyName== "DataSource")?.ErrorMessage;
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
