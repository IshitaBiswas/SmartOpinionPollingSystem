using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Common;
using SOP.Common.Model;
using System.Data;

namespace SOP.Data
{
    public interface IGenericAccessor
    {
        IEnumerable<VotingCategoryDesc> ChooseUserVotingCategories();

        DataSet ChooseOrgVotingCategories();
        
        LoginTypeEnum ValidateLogin(GenericLogin genlogin);

    }
}
