using Autofac;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using OH.ETL.Core.Configuration;
using OH.ETL.Core.Const;
using OH.ETL.Core.Enums;
using OH.ETL.Core.EFDbContext;
using OH.ETL.Core.DBManager;

namespace OH.ETL.Core.Extensions.AutofacManager;

public static class AutofacContainerModuleExtension
{
    public static IServiceCollection AddModule(this IServiceCollection services, ContainerBuilder builder, IConfiguration configuration)
    {
        //初始化配置文件
        AppSetting.Init(services, configuration);
        Type baseType = typeof(IDependency);
        var compilationLibrary = DependencyContext.Default.RuntimeLibraries
            .Where(x => !x.Serviceable && x.Type == "project")
            .ToList();

        var count1 = compilationLibrary.Count;
        List<Assembly> assemblyList = [];
        foreach (var _compilation in compilationLibrary)
        {
            try
            {
                assemblyList.Add(AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(_compilation.Name)));
            }
            catch (Exception ex)
            {
                Console.WriteLine(_compilation.Name + ex.Message);
            }
        }

        builder.RegisterAssemblyTypes(assemblyList.ToArray())
         .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
         .AsSelf().AsImplementedInterfaces()
         .InstancePerLifetimeScope();

        //string connectionString = DBServerProvider.GetConnectionString(null);
        if (DBType.Name == DbCurrentType.MySql.ToString())
        {
            services.AddDbContextPool<SysDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseMySQL(DBServerProvider.SysConnectingString);
            }, 64);

            services.AddDbContextPool<OhErpContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(DBServerProvider.OhErpDbConnectionString);
            }, 128);

            services.AddDbContextPool<OhErpU9cContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(DBServerProvider.OhErpU9cDbConnectionString);
            }, 128);

            services.AddDbContextPool<U9ErpContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(DBServerProvider.U9CErpDbConnectionString);
            }, 128);
        }

        return services;
    }
}
