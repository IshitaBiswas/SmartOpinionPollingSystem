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
    }
}
