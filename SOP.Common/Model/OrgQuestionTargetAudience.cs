using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Common.Model
{
    public class OrgQuestionTargetAudience
    {
        public string OrgID { get; set; }

        public int QuestionID { get; set; }

        public int VotingQuestionCategoryID { get; set; }
    }
}
