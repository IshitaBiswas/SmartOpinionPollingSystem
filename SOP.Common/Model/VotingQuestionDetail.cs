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

        public System.Nullable<int> VotedNo { get; set; }

        public System.DateTime VotingStartDate { get; set; }

        public System.DateTime VotingEndDate { get; set; }

    }
}
