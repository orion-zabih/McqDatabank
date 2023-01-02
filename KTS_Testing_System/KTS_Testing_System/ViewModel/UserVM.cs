using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KTS_Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace KTS_Testing_System.ViewModel
{
    public class UserVM:ResponseMessage
    {
        public User User { get; set; }
        public List<Role> Roles { get; set; }
        public List<String> SelectedRoles { get; set; }
        public UserVM()
        {
            Roles = new List<Role>();
            SelectedRoles = new List<string>();
        }
    }
    public class UserIndexVM : ResponseMessage
    {
        public IEnumerable<User> UsersList { get; set; }

        public UserIndexVM()
        {
            UsersList = null;
        }
    }
    public class FilteredUser
    {
        public decimal user_id { get; set; }
        public string user_name { get; set; }
        public string first_names { get; set; }
        public string last_name { get; set; }
        public string full_name { get; set; }
        public string status { get; set; }


        public FilteredUser()
        {

        }
    }
    public class ChangePasswordByAdminModel : ResponseMessage
    {

        public long user_id { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(256)]
        [System.ComponentModel.DataAnnotations.Required()]
        //[RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Invalid New Password\n   PASSWORD POLICY \n" +
        //    "Passwords will contain at least: \n" +
        //    "1 upper case letter \n" +
        //    "1 lower case letter \n" +
        //    "1 number or special character \n" +
        //    "8 characters in length \n" +
        //    "Password maximum length should not be arbitrarily limited")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d))(?![.\n])(?=.*[a-zA-Z]).*$", ErrorMessage = "Invalid New Password\n   PASSWORD POLICY \n" +
            "Passwords will contain at least: \n" +
            "1 one letter \n" +
            "1 number \n" +
            "8 characters in length \n" +
            "Password maximum length should not be arbitrarily limited")]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [System.ComponentModel.DataAnnotations.StringLength(256)]
        [System.ComponentModel.DataAnnotations.Required()]
        //[RegularExpression(@"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Invalid Confrim Password\n   PASSWORD POLICY \n" +
        //    "Passwords will contain at least: \n" +
        //    "1 upper case letter \n" +
        //    "1 lower case letter \n" +
        //    "1 number or special character \n" +
        //    "8 characters in length \n" +
        //    "Password maximum length should not be arbitrarily limited")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d))(?![.\n])(?=.*[a-zA-Z]).*$", ErrorMessage = "Invalid New Password\n   PASSWORD POLICY \n" +
            "Passwords will contain at least: \n" +
            "1 one letter \n" +
            "1 number \n" +
            "8 characters in length \n" +
            "Password maximum length should not be arbitrarily limited")]
        [DisplayName("Confirm Password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password does not match with New Password.")]
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserType { get; set; }

        public ChangePasswordByAdminModel()
        {

        }
    }
}