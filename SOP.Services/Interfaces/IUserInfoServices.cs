using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Common.Model;

namespace SOP.Services.Interfaces
{
    public interface IUserInfoServices
    {
        void RegisterUser(User user);
        void SaveUserVote(UserVotingDetail vd);
        User GetUser(string userID);
        void EditUserProfile(User user);

    }
}
