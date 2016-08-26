using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Services.Interfaces;
using SOP.Data;
using SOP.Data.Interfaces;
using SOP.Common;
using SOP.Common.Model;

namespace SOP.Services
{
    public class UserInfoServices : IUserInfoServices
    {
        IUserDetailsAccessor _udAcessor;
        public UserInfoServices()
        {
            _udAcessor = new UserDetailsAccessor();
        }
        public void RegisterUser(User user)
        {
            //Business Valoidation...Start
            if (_udAcessor.DoesUserExist(user))
                throw new ApplicationException("An User with this ID is already registered.");

            //Business Valoidation...End
            _udAcessor.RegisterUser(user);
        }

        public void SaveUserVote(UserVotingDetail vd)
        {
            _udAcessor.SaveUserVote(vd);
        }

        public User GetUser(string userID)
        {
            return _udAcessor.GetUser(userID);

        }

        public void EditUserProfile(User user)
        {
            _udAcessor.EditUserProfile(user);
        }
    }
}
