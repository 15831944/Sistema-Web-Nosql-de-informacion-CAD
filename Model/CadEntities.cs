using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class CadEntities : DbContext
    {
        public CadEntities(string connectionString)
                : base(((EntityConnectionStringBuilder)new EntityConnectionStringBuilder()
                {
                    Provider = "System.Data.SqlClient",
                    ProviderConnectionString = @"data source=pi-tfs-01;initial catalog=Cad;persist security info=True;user id=loginCad;password=loginCad;multipleactiveresultsets=True;application name=EntityFramework",
                    Metadata = @"metadata=res://*/CAD.csdl|res://*/CAD.ssdl|res://*/CAD.msl"
                }).ToString())
        {
        }
    }
}
