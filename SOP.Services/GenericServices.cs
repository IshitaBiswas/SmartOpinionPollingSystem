using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Data;
using SOP.Common.Model;
using SOP.Data.Interfaces;
using SOP.Services.Interfaces;
using System.Data;
using SOP.Common;

namespace SOP.Services
{
    
    public class GenericServices : IGenericServices
    {
        IGenericAccessor _igAccessor;

        public GenericServices() 
        {
            _igAccessor = new GenericAccessor();
        }

        public IEnumerable<VotingCategoryDesc> GetVotingCategories()
        {
            return _igAccessor.ChooseUserVotingCategories(); 
        }
        public DataSet ChooseOrgVotingCategories()
        {
            return _igAccessor.ChooseOrgVotingCategories(); 
        
        }

        public LoginTypeEnum ValidateLogin(GenericLogin genlogin)
        {
            return _igAccessor.ValidateLogin(genlogin);

        }
    }
}
