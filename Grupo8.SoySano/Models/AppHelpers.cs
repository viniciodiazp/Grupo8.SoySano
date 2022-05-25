using System;
using System.Collections.Generic;
using System.Text;

namespace Grupo8.SoySano.Models
{
    public class AppHelpers
    {
        private User _currentUser;

        public static User CurrentUser { get; set; }
        public static Activity CurrentActivity { get; set; }
    }
}
