using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldValidatorAPI
{
    public class CommonRegexValidationPatterns
    {
        public const string Email_Address_RegEx_Pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        public const string In_PhoneNumber_RegEx_Pattern = @"^(\+91[\-\s]?)?[6-9]\d{9}$";

        public const string In_Post_Code_RegEx_Pattern = @"^[1-9][0-9]{5}$";

        public const string Strong_Password_RegEx_Pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
    }
}
