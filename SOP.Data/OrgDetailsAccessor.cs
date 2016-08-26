using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Data.Interfaces;
using SOP.Common.Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using  SOP.Common;


namespace SOP.Data
{
    public class OrgDetailsAccessor : IOrgDetailsAccessor
    {

        public OrgDetailsAccessor(){}

        public void AddOrganization(Organization org)
        {
                //string sql = "INSERT INTO tblOrganization (OrgID,OrgName,OrgWebsite,OrgCategory,OrgRegPassword ) VALUES (@OrgID,@OrgName,@OrgWebsite,@OrgCategory,@OrgRegPassword)";
                string ConnectionString = ConfigurationManager.ConnectionStrings["dbSmartOpinionConnectionString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                        string sql = "INSERT INTO tblOrganization (OrgID,OrgName,OrgWebsite,OrgCategory,OrgRegPassword ) VALUES (@OrgID,@OrgName,@OrgWebsite,@OrgCategory,@OrgRegPassword)";
                        SqlCommand cmd = new SqlCommand(sql, connection);
                        connection.Open();
                        SqlTransaction sqlTran = connection.BeginTransaction();

                        cmd.Transaction = sqlTran;
                        SqlCommand cmd1 = new SqlCommand("spInsertOrgVotingCategory", connection);
                        cmd1.Transaction = sqlTran;
                        try
                        {
                            //using (SqlCommand cmd = new SqlCommand(sql, connection))
                            //{
                                cmd.Parameters.AddWithValue("@OrgID", org.OrgID);
                                cmd.Parameters.AddWithValue("@OrgName", org.OrgName);
                                cmd.Parameters.AddWithValue("@OrgWebsite", org.OrgWebsite);
                                cmd.Parameters.AddWithValue("@OrgCategory", org.OrgCategory);
                                cmd.Parameters.AddWithValue("@OrgRegPassword", org.OrgRegPassword);

                                cmd.CommandType = CommandType.Text;
                                cmd.ExecuteNonQuery();
                            //}

                            
                            //using (SqlCommand cmd1 = new SqlCommand("spInsertOrgVotingCategory", connection))
                            //{
                                var VotingIDTable = new DataTable();
                                VotingIDTable.Columns.Add("OrgVotingCategoryID", typeof(int));
                                cmd1.CommandType = CommandType.StoredProcedure;

                                foreach (var x in org.OrgVotingCategoryIDs)
                                    VotingIDTable.Rows.Add(x);

                                var VotingCatgoryList = new SqlParameter("@list", SqlDbType.Structured);
                                VotingCatgoryList.TypeName = "dbo.OrgVotingCategoryIDList";
                                VotingCatgoryList.Value = VotingIDTable;


                                cmd1.Parameters.Add(new SqlParameter("@OrgID", org.OrgID));
                                cmd1.Parameters.Add(VotingCatgoryList);
                                var rowsAffected = cmd1.ExecuteNonQuery();
                                if (rowsAffected == 0) throw new ApplicationException("Error in Org Registration");
                            
                            sqlTran.Commit();
                        }
                        catch(Exception ex)
                        {
                            sqlTran.Rollback();
                            throw new ApplicationException("There is some problem with the database");

                        }
            }
           
        }
        
        public bool DoesOrgExist(string OrgID)
        {
            int count = 0;
            string sql = "Select count(OrgID) from tblOrganization where OrgID = @OrgID";
            string ConnectionString = ConfigurationManager.ConnectionStrings["dbSmartOpinionConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                connection.Open();
                
                cmd.Parameters.AddWithValue("@OrgID", OrgID);
                
                cmd.CommandType = CommandType.Text;
                count = Convert.ToInt32( cmd.ExecuteScalar());
            }

            return(count > 0);
               
        }
        public bool OrgLogin(Organization org)
        {
            int count = 0;
            string sql = "Select count(OrgID) from  tblOrganization where OrgID = @OrgID and OrgRegPassword = @OrgPwd ";
            //and OrgRegPassword = @OrgPwd
            string ConnectionString = ConfigurationManager.ConnectionStrings["dbSmartOpinionConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();

                cmd.Parameters.AddWithValue("@OrgID", org.OrgID);
                cmd.Parameters.AddWithValue("@OrgPwd", org.OrgRegPassword);
                cmd.CommandType = CommandType.Text;

                count = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return (count > 0);
        }

        public Organization GetOrganization(string orgID)
        {
            using (var _db = new SOPDbDataContext())
            {
                return _db.tblOrganizations
                           .Where(or => or.OrgID == orgID)
                           .Select(s => new Organization
                           {
                               OrgID = orgID,
                               OrgName = s.OrgName,
                               OrgWebsite = s.OrgWebsite,
                               OrgCategory = s.OrgCategory,
                               OrgVotingCategoryDescriptions = GetOrgPollingQuestionCategories(orgID).Tables[0].AsEnumerable()
                                                               .Select(dataRow => new VotingCategoryDesc {
                                                                                         VotingCategoryID = dataRow.Field<int>("OrgVotingCategoryID") ,
                                                                                         CategoryDescription = dataRow.Field<string>("CategoryDescription")}).ToList()
                           }).FirstOrDefault();
            }

        }

        public DataSet GetOrgPollingQuestionCategories(String orgID)
        {
            string sql = "SELECT OrgVotingCategoryID,CategoryDescription FROM tblOrgVotingCategory vcat inner join tblVotingCategoryDesc vdes on vcat.OrgVotingCategoryID = vdes.VotingCategoryID where OrgID = @orgID";
            string ConnectionString = ConfigurationManager.ConnectionStrings["dbSmartOpinionConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, connection);
                sda.SelectCommand.Parameters.AddWithValue("@orgID", orgID);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                //Console.WriteLine(ds.);
                return ds;
            }
        }

        public void AddOrgPollingQuestionDetails(VotingQuestionDetail orgqstndetail)
        {
            using (var _db = new SOPDbDataContext())
            {
                var singleAddQuestionRecord = new tblVotingQuestionDetail
                {
                    QuestionText = orgqstndetail.QuestionText,
                    VotingStartDate = orgqstndetail.VotingStartDate,
                    VotingEndDate = orgqstndetail.VotingEndDate,
                    MinVotingAge = orgqstndetail.MinVotingAge,
                    MaxVotingAge = orgqstndetail.MaxVotingAge,
                    TargetAudienceGender = orgqstndetail.TargetAudienceGender

                };

                _db.tblVotingQuestionDetails.InsertOnSubmit(singleAddQuestionRecord);

                _db.SubmitChanges();

                orgqstndetail.OrgAddVotingCategoryIDs.ForEach(v =>
                                                    {
                                                        var singleTargetaudienceCategoryrecord = new tblOrgQuestionTargetAudience
                                                        {
                                                          OrgID =  orgqstndetail.OrgID,
                                                          QuestionID =  _db.tblVotingQuestionDetails.Max(q => q.QuestionID),
                                                          VotingQuestionCategoryID = v
                                                        };
                                                        _db.tblOrgQuestionTargetAudiences.InsertOnSubmit(singleTargetaudienceCategoryrecord);

                });

                _db.SubmitChanges();
            }
        }

        public void EditOrgProfile(Organization org)
        {
            using (var _db = new SOPDbDataContext())
            {
                //Update User table
                tblOrganization or = _db.tblOrganizations.First(r => r.OrgID == org.OrgID);

                or.OrgName = org.OrgName;
                or.OrgWebsite = org.OrgWebsite;
                or.OrgCategory = org.OrgCategory;


                //delete all existing voting category records for this user from tblUserVotingCategories
                var orgvc = _db.tblOrgVotingCategories.Where(s => s.OrgID == org.OrgID);

                if (orgvc.Any())
                {
                    _db.tblOrgVotingCategories.DeleteAllOnSubmit(orgvc);

                }

                //Insert new voting category records for this user into tblUserVotingCategories

                org.OrgVotingCategoryIDs.ForEach(v =>
                {
                    var singleVotingCategoryRecord = new tblOrgVotingCategory
                    {
                        OrgID = org.OrgID,
                        OrgVotingCategoryID = v
                    };
                    _db.tblOrgVotingCategories.InsertOnSubmit(singleVotingCategoryRecord);
                });


                _db.SubmitChanges();


            }
        }
    }
}
