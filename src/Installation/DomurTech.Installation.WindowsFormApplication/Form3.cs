using System;
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
    public partial class Form3 : Form
    {
        private UserInstaller _userInstaller;
        private RoleInstaller _roleInstaller;
        private RoleUserLineInstaller _roleUserLineInstaller;
        private ActionInstaller _actionInstaller;
        private ApplicationSettingInstaller _applicationSettingInstaller;
        public Form3()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Hide();
            var form2 = new Form2();
            form2.Closed += (s, args) => Close();
            form2.Show();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            var model = new AdminModel
            {
                FirstName = textBoxFirstName.Text,
                LastName = textBoxLastName.Text,
                Username = textBoxUsername.Text,
                Password = textBoxPassword.Text,
                ConfirmPassword = textBoxConfirmPassword.Text,
                Email = textBoxEmail.Text
            };
            try
            {
                var validator = new FluentBaseValidator<AdminModel, AdminRules>(model);
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
                    _userInstaller = new UserInstaller(new Repository<User>(context), new Repository<Language>(context), new Repository<UserHistory>(context), new Repository<Person>(context), new Repository<PersonHistory>(context));



                    Form4 form4;
                    if (_userInstaller.Exists())
                    {
                        Hide();
                        form4 = new Form4();
                        form4.Closed += (s, args) => Close();
                        form4.Show();
                    }
                    _userInstaller.Set(model);

                    var message = string.Empty;


                    _roleInstaller = new RoleInstaller(new Repository<Role>(context), new Repository<RoleHistory>(context), new Repository<RoleLanguageLine>(context), new Repository<RoleLanguageLineHistory>(context), new Repository<Language>(context), new Repository<User>(context));
                    message += _roleInstaller.Set();

                    _roleUserLineInstaller = new RoleUserLineInstaller(new Repository<RoleUserLine>(context), new Repository<RoleUserLineHistory>(context), new Repository<Role>(context), new Repository<User>(context));
                    _roleUserLineInstaller.Set();

                    _actionInstaller = new ActionInstaller(new Repository<ERP.Data.Entities.Concrete.Action>(context));
                    _actionInstaller.Set();

                    _applicationSettingInstaller = new ApplicationSettingInstaller(new Repository<ApplicationSetting>(context), new Repository<ApplicationSettingHistory>(context), new Repository<User>(context));
                    _applicationSettingInstaller.Set();

                    model.Message = message;

                    Hide();
                    form4 = new Form4();
                    form4.Closed += (s, args) => Close();
                    form4.Show();
                }




            }
            catch (CustomValidationException exception)
            {
                var validationResult = exception.ValidationResult;
                labelErrorFirstName.Text = validationResult.FirstOrDefault(x => x.PropertyName == "FirstName")?.ErrorMessage;
                labelErrorLastName.Text = validationResult.FirstOrDefault(x => x.PropertyName == "LastName")?.ErrorMessage;
                labelErrorUsername.Text = validationResult.FirstOrDefault(x => x.PropertyName == "Username")?.ErrorMessage;
                labelErrorPassword.Text = validationResult.FirstOrDefault(x => x.PropertyName == "Password")?.ErrorMessage;
                labelErrorConfirmPassword.Text = validationResult.FirstOrDefault(x => x.PropertyName == "ConfirmPassword")?.ErrorMessage;
                labelErrorEmail.Text = validationResult.FirstOrDefault(x => x.PropertyName == "Email")?.ErrorMessage;

            }

            catch (Exception exception)
            {
                model.Message = exception.ToString();
                labelMessage.Text = model.Message;

            }
        }
    }
}
