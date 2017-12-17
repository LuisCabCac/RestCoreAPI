using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestCore.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCore.Infrastructure
{
    public static class IocConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCustomDependencyInjection(this IServiceCollection services)
        {

            services.AddTransient<IUserManager, UserManager>();
        }
    }
}
