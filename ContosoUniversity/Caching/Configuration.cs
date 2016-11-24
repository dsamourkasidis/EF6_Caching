using EFCache;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ContosoUniversity.Controllers;
using System.Data.Entity.Core.Common;
using ContosoUniversity.Caching;


namespace ContosoUniversity.Caching
{
    public class Configuration : DbConfiguration
    {
        internal static readonly InMemoryCache Cache = new InMemoryCache();
        public Configuration()
        {
            var transactionHandler = new CacheTransactionHandler(Cache);

            AddInterceptor(transactionHandler);
            var cachingPolicy = new NewCachingPolicy();
            Loaded +=
              (sender, args) => args.ReplaceService<DbProviderServices>(
                (s, _) => new CachingProviderServices(s, transactionHandler,cachingPolicy
                  ));
        }
    }
}