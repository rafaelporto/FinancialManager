using System;
using FinancialManager.Identity;
using Microsoft.AspNetCore.Identity;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;

namespace FinancialManager.Infra.Data
{
	internal class DocumentStoreHolder
    {
        private readonly static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        internal static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            DocumentStore store = new()
            {
                Urls = new[] { "http://172.17.0.1:8080" },
                Conventions =
                {
                    MaxNumberOfRequestsPerSession = 10,
                    UseOptimisticConcurrency = true,
                    FindCollectionName = GetCollectionName,
                },
                Database = "FinancialManager",
            };

            return store.Initialize();
        }

        private static string GetCollectionName(Type type)
        {
            if (typeof(IdentityRole).IsAssignableFrom(type))
                return "Roles";

            if (typeof(ApplicationUser).IsAssignableFrom(type))
                return "Users";

            return DocumentConventions.DefaultGetCollectionName(type);
        }
    }
}
