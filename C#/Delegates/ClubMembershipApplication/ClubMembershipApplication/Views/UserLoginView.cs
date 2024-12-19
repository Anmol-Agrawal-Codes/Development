using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubMembershipApplication.Data;
using ClubMembershipApplication.FieldValidators;
using ClubMembershipApplication.Models;

namespace ClubMembershipApplication.Views
{
    public class UserLoginView : IView
    {
        ILogin _loginUser = null;
        IFieldValidator _fieldValidator = null;

        public IFieldValidator FieldValidator => null;

        public UserLoginView(ILogin login, IFieldValidator fieldValidator)
        {
            _fieldValidator = fieldValidator;
            _loginUser = login;
        }


        public void RunView()
        {
            CommonOutputText.WriteMainHeading();
            CommonOutputText.WriteLoginHeading();

            _fieldValidator.FieldArray[(int)FieldConstants.UserLoginField.EmailAddress] = GetInputFromUser(FieldConstants.UserLoginField.EmailAddress, "Please enter your email address: ");
            _fieldValidator.FieldArray[(int)FieldConstants.UserLoginField.Password] = GetInputFromUser(FieldConstants.UserLoginField.Password, "Please enter your password: ");

            Login();
        }

        private void Login()
        {
            User user = _loginUser.Login(_fieldValidator.FieldArray);
            if(user != null)
            {
                WelcomeUserView welcomeUserView = new WelcomeUserView(user);
                welcomeUserView.RunView();
            }
            else
            {
                Console.Clear();
                CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
                Console.WriteLine("Credentials are incorrect.");
                CommonOutputFormat.ChangeFontColor(FontTheme.Default);
                Console.ReadKey();
            }
        }

        private string GetInputFromUser(FieldConstants.UserLoginField field, string promptText)
        {
            string fieldVal = "";

            do
            {
                Console.WriteLine(promptText);
                fieldVal = Console.ReadLine();
            } while (!FieldValid(field, fieldVal));
            return fieldVal;
        }

        private bool FieldValid(FieldConstants.UserLoginField field, string fieldValue)
        {
            if (!_fieldValidator.ValidatorDel((int)field, fieldValue, _fieldValidator.FieldArray, out string invalidMessage))
            {
                CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
                Console.WriteLine(invalidMessage);

                CommonOutputFormat.ChangeFontColor(FontTheme.Default);
                return false;
            }
            return true;
        }
    }
}
