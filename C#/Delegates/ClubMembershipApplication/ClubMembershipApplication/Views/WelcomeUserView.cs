using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubMembershipApplication.Models;

namespace ClubMembershipApplication.Views
{
    public class WelcomeUserView
    {
        User _user = null;

        public WelcomeUserView(User user)
        {
            _user = user;
        }

        public void RunView()
        {
            Console.Clear();
            CommonOutputText.WriteMainHeading();
            CommonOutputFormat.ChangeFontColor(FontTheme.Success);
            Console.WriteLine($"Hi {_user.FirstName}!!{Environment.NewLine}Welcome to the Cycling Club.");
            CommonOutputFormat.ChangeFontColor(FontTheme.Success);
            Console.ReadKey();
        }
    }
}
