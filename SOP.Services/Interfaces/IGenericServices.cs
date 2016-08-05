using System;
using System.Data;
using SOP.Common.Model;
using SOP.Common;

namespace SOP.Services.Interfaces
{
    public interface IGenericServices
    {
        System.Collections.Generic.IEnumerable<SOP.Common.Model.VotingCategoryDesc> GetVotingCategories();

        DataSet ChooseOrgVotingCategories();

        LoginTypeEnum ValidateLogin(GenericLogin genlogin);

    }

}
