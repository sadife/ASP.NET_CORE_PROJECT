using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Autofac.Core;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection collection)
        {
            collection.AddMemoryCache();
            collection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            collection.AddSingleton<ICacheManager, MemoryCacheManager>();
            collection.AddSingleton<Stopwatch>();  //Performance aspecti için eklendi
        }
    }
}
