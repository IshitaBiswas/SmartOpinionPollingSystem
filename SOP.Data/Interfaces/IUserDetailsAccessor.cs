using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Common.Model;

namespace SOP.Data.Interfaces
{
   public interface IUserDetailsAccessor
    {
       IEnumerable<User> GetUsers();
       void RegisterUser(User user);
       bool DoesUserExist(User user);
       bool DoesEmailIdExist(User user);
       void SaveUserVote(UserVotingDetail vd);
       User GetUser(string userID);
       void EditUserProfile(User user);
    }
}
