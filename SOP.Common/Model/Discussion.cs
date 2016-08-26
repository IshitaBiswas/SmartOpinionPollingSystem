using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Common.Model
{
   public class Discussion
    {
       public string UserID { get; set; }

       public int? QuestionID { get; set; }

       public string DiscussionText { get; set; }

       public DateTime? DateDiscussionCreated { get; set; }

       //Added extra Field 
       public string UserFName { get; set; }
    }
}
