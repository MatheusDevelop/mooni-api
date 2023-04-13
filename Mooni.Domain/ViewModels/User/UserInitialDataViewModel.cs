using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooni.Domain.ViewModels.User
{
    public class UserInitialDataViewModel
    {
        public UserInitialDataViewModel(string id, string firstName, string lastName, string avatarUrl)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AvatarUrl = avatarUrl;
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl  { get; set; }
    }
}
