using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SOP.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SOP.Data.Interfaces;
using SOP.Common.Model;

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

        public User GetUser(string userID)
        {
            using (var _db = new SOPDbDataContext())
            {
                return _db.tblUsers
                           .Where( us => us.UserID == userID)
                           .Select(s => new User 
                            {
                                UserID = s.UserID,
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                Gender = s.Gender,
                                Age = s.Age,
                                Email = s.Email,
                                Phoneno = s.Phoneno,
                                Occupation = s.Occupation,
                                UserVotingCategoryDescriptions = GetUserPollingQuestionCategories(userID).ToList()

                            }).FirstOrDefault();
            }

        }

        public IEnumerable<VotingCategoryDesc> GetUserPollingQuestionCategories(String userID)
        {
            
            using (var _db = new SOPDbDataContext())
            {
                return _db.tblUserVotingCategories
                          .Join(_db.tblVotingCategoryDescs,
                           uvc => uvc.UserVotingCategoryID,
                           vcd => vcd.VotingCategoryID,
                           (uvc, vcd) => new { uvc, vcd })
                          .Where(z => z.uvc.UserID == userID)
                          .Select(z => new VotingCategoryDesc
                                    {
                                        VotingCategoryID = z.uvc.UserVotingCategoryID,
                                        CategoryDescription = z.vcd.CategoryDescription
                                    }
                          ).ToArray();

            }
            
            //string sql = "SELECT OrgVotingCategoryID,CategoryDescription FROM tblOrgVotingCategory vcat inner join tblVotingCategoryDesc vdes on vcat.OrgVotingCategoryID = vdes.VotingCategoryID where OrgID = @orgID";
            //string ConnectionString = ConfigurationManager.ConnectionStrings["dbSmartOpinionConnectionString"].ConnectionString;
            //using (SqlConnection connection = new SqlConnection(ConnectionString))
            //{
            //    SqlDataAdapter sda = new SqlDataAdapter(sql, connection);
            //    sda.SelectCommand.Parameters.AddWithValue("@orgID", orgID);
            //    DataSet ds = new DataSet();
            //    sda.Fill(ds);
            //    //Console.WriteLine(ds.);
            //    return ds;
            //}
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


               // user.UserVotingCategoryIDs.ForEach(v => { });

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


        public void SaveUserVote(UserVotingDetail vd)
        {
            using (var _db = new SOPDbDataContext())
            {
                var singleUserVotingDetail = new tblUserVotingDetail
                {
                    UserID = vd.UserID,
                    QuestionID = vd.QuestionID,
                    B_UserVote = Convert.ToBoolean(vd.B_UserVote),
                    DtVoteCasted = DateTime.Now
                
                };

                //Insert 
                _db.tblUserVotingDetails.InsertOnSubmit(singleUserVotingDetail);

                //Update 
                tblVotingQuestionDetail vqd = _db.tblVotingQuestionDetails.First(r => r.QuestionID == vd.QuestionID);
                if (Convert.ToBoolean(vd.B_UserVote))
                     vqd.VotedYes = (vqd.VotedYes??0) + 1;
                 else 
                     vqd.VotedNo = (vqd.VotedNo ?? 0) + 1;


             _db.SubmitChanges();  //Insert & update happen on SubmitChanges()
            }


        }

        public void EditUserProfile(User user)
        {
            using (var _db = new SOPDbDataContext())
            {
                //Update User table
                tblUser us = _db.tblUsers.First(r => r.UserID == user.UserID);

                us.FirstName = user.FirstName;
                us.LastName = user.LastName;
                us.Phoneno = user.Phoneno;
                us.Occupation = user.Occupation;
                us.Email = user.Email;
                us.Age = user.Age;


                //delete all existing voting category records for this user from tblUserVotingCategories
                var uservc = _db.tblUserVotingCategories.Where(s => s.UserID == user.UserID);

                if (uservc.Any())
                {
                    _db.tblUserVotingCategories.DeleteAllOnSubmit(uservc);

                }

                //Insert new voting category records for this user into tblUserVotingCategories

                user.UserVotingCategoryIDs.ForEach(v =>
                {
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


    }
}
