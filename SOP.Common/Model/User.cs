using System;
using System.Collections.Generic ;
using System.Linq;
using System.Text ;
using System.Threading.Tasks;
using System.Data.Linq;


namespace SOP.Common.Model
{
    public class User
    {
        public string UserID {get; set; }

        public string FirstName {get; set; }

        public string LastName {get; set; }

        public string Gender {get; set; }

        public int Age {get; set; }

        public string Email {get; set; }

        public string Password { get; set; }

        public string Phoneno {get; set; }

        public string Occupation {get; set; }

        //new propertiesa added

        private List<int> _userVotingCategoryIDs;
        public List<int> UserVotingCategoryIDs
        {
            get { return this._userVotingCategoryIDs ?? (this._userVotingCategoryIDs = new List<int>()); }
            set { this._userVotingCategoryIDs = value; }
        }

        private List<VotingCategoryDesc> _userVotingCategoryDescriptions;
        public List<VotingCategoryDesc> UserVotingCategoryDescriptions
        {
            get { return this._userVotingCategoryDescriptions ?? (this._userVotingCategoryDescriptions = new List<VotingCategoryDesc>()); }
            set { this._userVotingCategoryDescriptions = value; }
        }
    }
}
