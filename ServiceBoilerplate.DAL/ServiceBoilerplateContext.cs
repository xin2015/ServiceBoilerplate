using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace ServiceBoilerplate.DAL
{
    public class ServiceBoilerplateContext : OpenAccessContext, IUnitOfWork
    {
        static string connectionStringName = "ServiceBoilerplateConnection";
        static MetadataContainer metadataContainer = new ServiceBoilerplateMetadataSource().GetModel();
        static BackendConfiguration backendConfiguration = new BackendConfiguration()
        {
            Backend = "MsSql",
            ProviderName = "System.Data.SqlClient"
        };

        public ServiceBoilerplateContext() : base(connectionStringName, backendConfiguration, metadataContainer) { }

        public void UpdateSchema()
        {
            var handler = GetSchemaHandler();
            string script = null;
            try
            {
                script = handler.CreateUpdateDDLScript(null);
            }
            catch
            {
                bool throwException = false;
                try
                {
                    handler.CreateDatabase();
                    script = handler.CreateDDLScript();
                }
                catch
                {
                    throwException = true;
                }
                if (throwException)
                    throw;
            }

            if (string.IsNullOrEmpty(script) == false)
            {
                handler.ExecuteDDLScript(script);
            }
        }
    }
}
