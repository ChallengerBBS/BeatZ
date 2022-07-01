using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatZ.Domain.Entities.Auth
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { 
                Username = "sevdovasilev95",
                EmailAddress = "sevdo@abv.bg",
                Password = "qwebsrty",
                GivenName = "Sevdo",
                Surname = "Vasilev",
                Role = "Administrator"
            },
             new UserModel() {
                Username = "mjackson",
                EmailAddress = "mjackson@abv.bg",
                Password = "qwebsrty",
                GivenName = "Mike",
                Surname = "Jackson",
                Role = "Author"
            },
               new UserModel() {
                Username = "realfan99",
                EmailAddress = "fanboy@abv.bg",
                Password = "qwebsrty",
                GivenName = "Fan",
                Surname = "Boy",
                Role = "User"
            },
        };
    }
}
