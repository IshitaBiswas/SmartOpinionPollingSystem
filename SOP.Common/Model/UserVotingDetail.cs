using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Common.Model
{
  public  class UserVotingDetail
    {
      public string UserID { get; set; }

        public int QuestionID { get; set; }

        public string B_UserVote { get; set; }

        public System.DateTime DtVoteCasted { get; set; }

      //Additional attributes

        public int VotingQuestionCategoryID { get; set; }

        public string OrgID { get; set; }

        public string OrgName { get; set; }

        public string QuestionText { get; set; }

        public System.Nullable<int> VotedYes { get; set; }

        //Another way to declare nullable variable
        public int? VotedNo { get; set; }

        public System.DateTime VotingStartDate { get; set; }

        public System.DateTime VotingEndDate { get; set; }
        public string CategoryDescription { get; set; }


    }
}
