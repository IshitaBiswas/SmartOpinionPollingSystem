using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SOP.Common;
using SOP.Data.Interfaces;
using SOP.Common.Model;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace SOP.Data
{
  public  class GenericAccessor : IGenericAccessor
    {
      public GenericAccessor() {}
      public IEnumerable<VotingCategoryDesc> ChooseUserVotingCategories()
      {
          using (var _db = new SOPDbDataContext())
          {
              return _db.tblVotingCategoryDescs.Select(s => new VotingCategoryDesc
              {
                  VotingCategoryID = s.VotingCategoryID,
                  CategoryDescription = s.CategoryDescription

              }).ToArray();
          }

      }

      public DataSet ChooseOrgVotingCategories()
      {
          string sql = "Select CategoryDescription,VotingCategoryID from tblVotingCategoryDesc";
          string ConnectionString = ConfigurationManager.ConnectionStrings["dbSmartOpinionConnectionString"].ConnectionString;
          using (SqlConnection connection = new SqlConnection(ConnectionString))
          {
              SqlDataAdapter sda = new SqlDataAdapter(sql,connection);
              DataSet ds = new DataSet();
              sda.Fill(ds);
              //Console.WriteLine(ds.);
              return ds;
          }
      }

      public LoginTypeEnum ValidateLogin(GenericLogin genlogin)
      {
          //int count = 0;
          //string sql = "Select count(OrgID) from  tblOrganization where OrgID = @OrgID and OrgRegPassword = @OrgPwd ";
          ////and OrgRegPassword = @OrgPwd
          //string ConnectionString = ConfigurationManager.ConnectionStrings["dbSmartOpinionConnectionString"].ConnectionString;

          //using (SqlConnection con = new SqlConnection(ConnectionString))
          //{
          //    SqlCommand cmd = new SqlCommand(sql, con);
          //    con.Open();

          //    cmd.Parameters.AddWithValue("@OrgID", genlogin.LoginID);
          //    cmd.Parameters.AddWithValue("@OrgPwd", genlogin.Password);
          //    cmd.CommandType = CommandType.Text;

          //    count = Convert.ToInt32(cmd.ExecuteScalar());
          //}



          using (var _db = new SOPDbDataContext())
          {
              return _db.tblUsers
                     .Any(u => u.UserID == genlogin.LoginID && u.Password == genlogin.Password)
                     ? LoginTypeEnum.User
                     : (_db.tblOrganizations.Any(o => o.OrgID == genlogin.LoginID && o.OrgRegPassword == genlogin.Password)
                          ? LoginTypeEnum.Organization 
                          : LoginTypeEnum.Anonymous);
             
          }

          




      }

    }
}
