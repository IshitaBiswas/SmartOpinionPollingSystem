using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Data.Interfaces;
using SOP.Data;
using SOP.Common.Model;
using SOP.Services.Interfaces;


namespace SOP.Services
{
    public class PollingStatusService : IPollingStatusService
    {
        IUserDetailsAccessor udAccesor;
        public List<User> GetRegisteredUsers()
        {
           udAccesor = new UserDetailsAccessor();
           return udAccesor.GetUsers().ToList();
          }
    }
}
