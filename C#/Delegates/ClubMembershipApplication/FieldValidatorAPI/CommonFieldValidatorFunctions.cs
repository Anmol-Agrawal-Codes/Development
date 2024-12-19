using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FieldValidatorAPI
{
    public delegate bool RequiredValidDel(string fieldVal); // ensures form field is not empty
    public delegate bool StringLengthValidDel(string fieldVal, int min, int max); // ensures string Length
    public delegate bool DateValidDel(string fieldVal, out DateTime ValidDateTime); // ensures Date is valid
    public delegate bool PatternMatchValidDel(string fieldVal,  string pattern); // validates field value with regex
    public delegate bool CompareFieldsValidDel(string fieldVal, string fieldValCompare); // Compare two text field values
    
    public class CommonFieldValidatorFunctions
    {
        private static RequiredValidDel _requiredValidDel = null;
        private static StringLengthValidDel _stringLengthValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchValidDel _patternMatchValidDel = null;
        private static CompareFieldsValidDel _compareFieldsValidDel = null;

        public static RequiredValidDel RequiredFieldValidDel
        {
            get
            {
                if (_requiredValidDel == null)
                {
                    _requiredValidDel = RequiredFieldValid;
                }
                return _requiredValidDel;
            }
        }

        public static StringLengthValidDel StringLengthValidDel
        {
            get
            {
                if (_stringLengthValidDel == null)
                {
                    _stringLengthValidDel = StringLengthValid;
                }
                return _stringLengthValidDel;
            }
        }

        public static DateValidDel DateFieldValidDel
        {
            get
            {
                if (_dateValidDel == null)
                {
                    _dateValidDel = DateFieldValid;
                }
                return _dateValidDel;
            }
        }

        public static PatternMatchValidDel FieldPatternValidDel
        {
            get
            {
                if (_patternMatchValidDel == null)
                {
                    _patternMatchValidDel = FieldPatternValid;
                }
                return _patternMatchValidDel;
            }
        }
        
        public static CompareFieldsValidDel FieldComparisonValidDel
        {
            get
            {
                if (_compareFieldsValidDel == null)
                {
                    _compareFieldsValidDel = FieldComparisonValid;
                }
                return _compareFieldsValidDel;
            }
        }

        private static bool RequiredFieldValid(string fieldVal)
        {
            if (string.IsNullOrEmpty(fieldVal))
            {
                return false;
            }
            return true;
        }

        private static bool StringLengthValid(string fieldVal, int min, int max)
        {
            if (fieldVal.Length < min || fieldVal.Length > max)
            {
                return false;
            }
            return true;
        }

        private static bool DateFieldValid(string dateTime, out DateTime validDateTime)
        {
            if(!DateTime.TryParse(dateTime, out validDateTime))
            {
                return false;
            }
            return true;
        }

        private static bool FieldPatternValid(string fieldVal, string regexPattern)
        {
            Regex regex = new Regex(regexPattern);

            if(!regex.IsMatch(fieldVal))
            {
                return false;
            }
            return true;
        }

        private static bool FieldComparisonValid(string field1, string field2)
        {
            if (!field1.Equals(field2))
            {
                return false;
            }
            return true;
        }
    }
}
