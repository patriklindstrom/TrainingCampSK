using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace TrainingCamp.Web.Repository
{
    public static class DocumentStoreHolder
    {
        private static IDocumentStore _documentStore;

        public static IDocumentStore DocumentStore
        {
            get
            {
                if (_documentStore != null)
                    return _documentStore;

                lock (typeof(DocumentStoreHolder))
                {
                    if (_documentStore != null)
                        return _documentStore;

                    _documentStore = new DocumentStore
                    {
                        ConnectionStringName = ConnectionStringName
                    };

                    _documentStore.Initialize();
                    _documentStore.Conventions.IdentityPartsSeparator = "-";
                    IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), _documentStore);
                    IndexCreation.CreateIndexes(typeof(LeftJoinPageTextElement).Assembly, _documentStore);
                }

                return _documentStore;
            }
        }

        private static string ConnectionStringName
        {
            get
            {
                string connectionStringName = "RavenHQ";
                //var customConnection1 = ConfigurationManager.ConnectionStrings["RavenHQ"];
                //var customConnection = ConfigurationManager.ConnectionStrings[Environment.MachineName] != null;
                //var connectionStringName = customConnection ? Environment.MachineName : "RavenDB";
                Debug.Assert(!string.IsNullOrEmpty(connectionStringName),"Raven DB connectionstring is empty");

                return connectionStringName;
            }
        }
    }
}