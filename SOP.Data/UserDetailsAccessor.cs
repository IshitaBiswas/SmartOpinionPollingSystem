using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SOP.Common;
using SOP.Data.Interfaces;

namespace SOP.Data
{
    public class UserDetailsAccessor: IUserDetailsAccessor
    {
        public UserDetailsAccessor() {}
        public IEnumerable<User> GetUsers()
        {
            using (var _db = new SOPDbDataContext())
            {
                return _db.tblUsers.Select(s => new User
                {
                    UserID = s.UserID,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Gender = s.Gender,
                    Age = s.Age,
                    Email = s.Email,
                    Password = s.Password,
                    Phoneno = s.Phoneno,
                    Occupation = s.Occupation

                }).ToArray();
            }
           
        }
        public void RegisterUser(User user)
        {
            using (var _db = new SOPDbDataContext())
            {
                var singleUserRecord = new tblUser
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    Age = user.Age,
                    Email = user.Email,
                    Password = user.Password,
                    Phoneno = user.Phoneno,
                    Occupation = user.Occupation

                };

                _db.tblUsers.InsertOnSubmit(singleUserRecord);

                user.UserVotingCategoryIDs.ForEach(v =>  {
                                                      var singleVotingCategoryRecord = new tblUserVotingCategory
                                                        {
                                                            UserID = user.UserID,
                                                            UserVotingCategoryID = v
                                                        };
                                                    _db.tblUserVotingCategories.InsertOnSubmit(singleVotingCategoryRecord);
                });



                _db.SubmitChanges();


            }
        }
        public bool DoesUserExist(User user)
        {
            var _db = new SOPDbDataContext();
            return _db.tblUsers.Any(u =>  u.UserID == user.UserID);
        }


    }
}
