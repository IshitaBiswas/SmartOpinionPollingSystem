using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SOP.Common;
using SOP.Common.Model;
using SOP.Data.Interfaces;

namespace SOP.Data
{
  public   class ReportingAccessor : IReportingAccessor
    {

      public IEnumerable<OrgCategoryBreakup> GetOrgCategoryBreakup()
      {
          using (var _db = new SOPDbDataContext())
          {
              return _db.tblOrganizations
                  .GroupBy(o => o.OrgCategory)
                  .Select(g => new OrgCategoryBreakup { 
                                          OrgCategory =  g.Key,
                                          NumberOfOrganizations = g.Count() 
                        })
                  .OrderBy(x => x.OrgCategory).ToArray();
          }

      }

    }
}
