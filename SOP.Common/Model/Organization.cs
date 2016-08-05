using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Common.Model
{
    public class Organization
    {
        public string OrgID {get; set; }

        public string OrgName {get; set; }

        public string OrgWebsite {get; set; }

        public string OrgCategory {get; set; }

        public string OrgRegPassword {get; set; }

        //Additional properties
 
        private List<int> _orgVotingCategoryIDs ;

        public List<int> OrgVotingCategoryIDs
        {
            get
            {
                return (this._orgVotingCategoryIDs ?? (_orgVotingCategoryIDs = new List<int>()));
            }
            set
            {
                this._orgVotingCategoryIDs = value;
            }
        }
    }
}
