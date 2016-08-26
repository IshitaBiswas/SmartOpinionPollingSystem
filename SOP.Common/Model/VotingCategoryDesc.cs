using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP.Common.Model
{
  public  class VotingCategoryDesc
    {
        public int VotingCategoryID { get; set; }
        public string CategoryDescription { get; set; }
    }


  public class VotingCategoryDescComparer : IEqualityComparer<VotingCategoryDesc>
  {

      public bool Equals(VotingCategoryDesc x, VotingCategoryDesc y)
      {
          //Check whether the objects are the same object. 
          if (Object.ReferenceEquals(x, y)) return true;

          //Check whether the products' properties are equal. 
          return x != null && y != null && x.VotingCategoryID.Equals(y.VotingCategoryID) && x.CategoryDescription.Equals(y.CategoryDescription);
      }

      public int GetHashCode(VotingCategoryDesc obj)
      {
          //Get hash code for the Name field if it is not null. 
          int hashCategoryDescription = obj.CategoryDescription == null ? 0 : obj.CategoryDescription.GetHashCode();

          //Get hash code for the Code field. 
          int hashVotingCategoryID = obj.VotingCategoryID.GetHashCode();

          //Calculate the hash code for the product. 
          return hashVotingCategoryID ^ hashCategoryDescription;
      }
  }




}
