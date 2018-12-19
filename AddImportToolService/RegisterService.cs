using BaseCore.Cache;
using BaseCore.Untity;
using BusinessLibrary;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace AddRegistService
{    
    public static class RegisterService
    {       
        public static  void InjectionService(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ConfigHelper>();
            services.AddTransient<MemoryCacheHelper>();        
        }
    }
}
