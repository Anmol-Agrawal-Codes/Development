using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubMembershipApplication.Data;
using FieldValidatorAPI;

namespace ClubMembershipApplication.FieldValidators
{
    public class UserLoginValidator: IFieldValidator
    {
        ILogin _login = null;

        FieldValidatorDel _fieldValidator = null;

        RequiredValidDel _requiredValidDel = null;
        PatternMatchValidDel _patternMatchValidDel = null;

        public UserLoginValidator(ILogin login) 
        {
            _login = login;
        }

        private string[] _fieldArray = null;

        public string[] FieldArray
        {
            get
            {
                if (_fieldArray == null)
                {
                    _fieldArray = new string[Enum.GetValues(typeof(FieldConstants.UserLoginField)).Length];
                }
                return _fieldArray;
            }
        }

        public FieldValidatorDel ValidatorDel => _fieldValidator;

        public void InitialiseValidatorDelegates()
        {
            _fieldValidator = ValidField;

            _requiredValidDel = CommonFieldValidatorFunctions.RequiredFieldValidDel;
            _patternMatchValidDel = CommonFieldValidatorFunctions.FieldPatternValidDel;
        }

        private bool ValidField(int fieldIndex, string fieldValue, string[] fieldArray, out string fieldInvalidMessage)
        {
            fieldInvalidMessage = "";

            FieldConstants.UserLoginField userLoginField = (FieldConstants.UserLoginField)fieldIndex;
            switch (userLoginField)
            {
                case FieldConstants.UserLoginField.EmailAddress:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserLoginField), userLoginField)}{Environment.NewLine}" : fieldInvalidMessage;
                    fieldInvalidMessage = (fieldInvalidMessage == "" && !_patternMatchValidDel(fieldValue, CommonRegexValidationPatterns.Email_Address_RegEx_Pattern)) ? $"You must enter a valid email: {Enum.GetName(typeof(FieldConstants.UserLoginField), userLoginField)}{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                case FieldConstants.UserLoginField.Password:
                    fieldInvalidMessage = (!_requiredValidDel(fieldValue)) ? $"You must enter a value for field: {Enum.GetName(typeof(FieldConstants.UserLoginField), userLoginField)}{Environment.NewLine}" : fieldInvalidMessage;
                    break;
                default:
                    throw new ArgumentException("This field does not exists");
            }
            return (fieldInvalidMessage == "");
        }
    }
}
