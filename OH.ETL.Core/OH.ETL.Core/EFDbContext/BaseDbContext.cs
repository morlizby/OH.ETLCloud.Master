using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using OH.ETL.Core.Extensions;
using OH.ETL.Core.Utils;

namespace OH.ETL.Core.EFDbContext;

public abstract class BaseDbContext : DbContext
{
    protected abstract string ConnectionString { get; }
    public bool QueryTracking
    {
        set
        {
            ChangeTracker.QueryTrackingBehavior = value ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
        }
    }

    public BaseDbContext() : base() { }
    public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {


        // 日志记录器
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Warning);
        //.EnableSensitiveDataLogging()
        //默认禁用实体跟踪
        optionsBuilder = optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected void OnModelCreating(ModelBuilder modelBuilder, Type type)
    {
        try
        {
            //获取所有类库
            var compilationLibrary = DependencyContext.Default.RuntimeLibraries
                .Where(x => !x.Serviceable && x.Type != "package" && x.Type == "project");
            foreach (var _compilation in compilationLibrary)
            {
                //加载指定类
                AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(_compilation.Name))
                    .GetTypes().Where(x => x.GetTypeInfo().BaseType != null && x.BaseType == type).ToList()
                    .ForEach(t => { modelBuilder.Entity(t); });
            }

            base.OnModelCreating(modelBuilder);
        }
        catch (Exception ex)
        {
            string mapPath = ($"Log/").MapPath();
            FileHelper.WriteFile(mapPath, $"syslog_{DateTime.Now:yyyyMMddHHmmss}.txt", ex.Message + ex.StackTrace + ex.Source);
        }
    }
}
