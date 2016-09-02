using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Common.Model
{
    public class VotingQuestionDetail
    {
        public int QuestionID { get; set; }

        public int VotingQuestionCategoryID { get; set; }

        public string OrgID { get; set; }

        public string QuestionText { get; set; }

        public System.Nullable<int> VotedYes { get; set; }

        //Another way to declare nullable variable
        public int? VotedNo { get; set; }

        public System.DateTime VotingStartDate { get; set; }

        public System.DateTime VotingEndDate { get; set; }

        public System.Nullable<int> MinVotingAge;

        public System.Nullable<int> MaxVotingAge;

        public string TargetAudienceGender;

        //Additional field
        

        private List<int> _orgAddVotingCategoryIDs;

        public List<int> OrgAddVotingCategoryIDs
        {
            get
            {
                return (this._orgAddVotingCategoryIDs ?? (_orgAddVotingCategoryIDs = new List<int>()));
            }
            set
            {
                this._orgAddVotingCategoryIDs = value;
            }
        }

        


        public string CategoryDescription { get; set; }

        public IEnumerable<string> AllCategories { get; set; }

    }
}
