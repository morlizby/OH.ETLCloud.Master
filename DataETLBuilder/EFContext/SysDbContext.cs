using DataETLBuilder.Entity;
using DataETLBuilder.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Google.Protobuf.Reflection.FeatureSet.Types;

namespace DataETLBuilder.EFContext;

public class SysDbContext : DbContext
{
    /*
     * 逆向工程: DbFirst
     * Scaffold-DbContext 'Server=localhost;Database=您的数据库名;charset=utf8;uid=账号;pwd=密码;port=端口;' MySql.EntityFrameworkCore -OutputDir "输出的目录文件夹"
     * 
     * dotnet ef dbcontext scaffold "server=localhost;uid=root;pwd=123456;database=eftests" "Pomelo.EntityFrameworkCore.Mysql" -o Models
     * 
     * 
     * 
     * 
     * 
     * 
     */
    //根据配置文件读取数据库连接
    private IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

    public SysDbContext() { }

    public SysDbContext(DbContextOptions<DbContext> options) : base(options) { }

    public virtual DbSet<SysDbaseConfigHead> SysDbaseConfigHeads { get; set; } = null!;
    public virtual DbSet<SysDbaseConfigDetail> SysDbaseConfigDetails { get; set; } = null!;
    public virtual DbSet<QuartzOption> QuartzOptions { get; set; } = null!;
    public virtual DbSet<QuartzLog> QuartzLogs { get; set; } = null!;

    public static readonly ILoggerFactory logger = LoggerFactory.Create(
        builder => { builder.AddConsole(); });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //根据配置文件读取数据库连接
        var dbConnString = configuration.GetConnectionString("DbConnectionString");

        optionsBuilder.UseMySQL(dbConnString);
        //使用日志记录sql语句
        optionsBuilder.UseLoggerFactory(logger)
            .EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SysDbaseConfigHead>(entity =>
        {
            entity.ToTable("Sys_DbaseConfigHead");

            entity.HasKey(e => e.Id)
                    .HasName("PK_DbaseConfigHead");

            entity.HasIndex(e => e.ServerAddr, "AK_KEY_ServerAddr_DbaseConfig")
                .IsUnique();

            entity.Property(e => e.ServerAddr)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("服务器地址");

            entity.Property(e => e.Port)
                .IsRequired()
                .HasComment("端口");

            entity.Property(e => e.UserID)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasComment("登录账号");

            entity.Property(e => e.UserPwd)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasComment("密钥");

            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("创建时间");

            entity.Property(e => e.RepositoryType)
                .HasColumnType("varchar(20)")
                .HasConversion(
                    v => v.ToString(),
                    v => (DataCategory)Enum.Parse(typeof(DataCategory), v))
                .HasComment("存储库类型");

            entity.Property(e => e.EnvCategory)
                .HasColumnType("varchar(20)")
                .HasConversion(
                    v => v.ToString(),
                    v => (EnvironCategory)Enum.Parse(typeof(EnvironCategory), v))
                .HasComment("环境类型");

        });

        modelBuilder.Entity<SysDbaseConfigDetail>(entity =>
        {
            entity.ToTable("Sys_DbaseConfigDetail");

            entity.HasKey(e => e.DetailId)
                    .HasName("PK_DbaseConfigDetail");

            entity.Property(e => e.DataRepository)
                .IsRequired()
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasComment("数据仓库名");

            entity.Property(e => e.Desciption)
                .HasColumnType("text")
                .IsUnicode(false)
                .HasComment("描述");

            entity.HasOne(d => d.SysDbaseConfigHead)
                .WithMany(p => p.SysDbaseConfigDetail)
                .HasForeignKey(d => d.HeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DbaseConfigD_REFERENCE_DbaseConfigH");
        });

        modelBuilder.Entity<QuartzOption>(entity =>
        {
            entity.ToTable("Sys_QuartzOption");

            //entity.HasComment("定时任务");

            entity.Property(e => e.TaskId).HasDefaultValueSql("(uuid())");

            entity.HasIndex(e => e.TaskName, "AK_KEY_TaskName_QuartzOption")
                .IsUnique();

            entity.Property(e => e.TaskName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("任务名称");

            entity.Property(e => e.GroupName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("组名");

            entity.Property(e => e.Describe)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("任务描述");

            entity.Property(e => e.TaskType)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComment("任务类型 0：Simple,1: Cron");

            entity.Property(e => e.RequestMode)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("请求方式");

            entity.Property(e => e.PostData)
                .HasColumnType("JSON")
                .HasComment("post参数");

            entity.Property(e => e.ApiUrl)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasComment("接口地址");

            entity.Property(e => e.AuthKey)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("身份验证秘钥值名");

            entity.Property(e => e.AuthValue)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasComment("身份验证秘钥值");

            entity.Property(e => e.RepeatInterval)
                .HasComment("间隔时间");

            entity.Property(e => e.RepeatCount)
                .HasComment("重复次数:-1无限");

            entity.Property(e => e.FiredCount)
                .HasComment("触发次数");

            entity.Property(e => e.CronExpression)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("Corn表达式");

            entity.Property(e => e.NextFireTime)
                .HasComment("下次触发时间");

            entity.Property(e => e.BeginTime)
                .HasComment("开始时间");

            entity.Property(e => e.EndTime)
                .HasComment("结束时间");

            entity.Property(e => e.LastRunTime)
                .HasColumnType("datetime")
                .HasComment("最后执行时间");

            entity.Property(e => e.TimeOut)
                .HasComment("超时(秒)");

            entity.Property(e => e.Status)
                .HasComment("运行状态:0正常, 1暂停, 2停止");

            entity.Property(e => e.ExcuteTime)
                .HasComment("执行时间");

            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("创建时间");
        });

        modelBuilder.Entity<QuartzLog>(entity =>
        {
            entity.ToTable("Sys_QuartzLog");

            //entity.HasComment("任务日志");

            entity.Property(e => e.LogId).HasDefaultValueSql("(uuid())");

            entity.Property(e => e.TaskName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("任务名称");

            entity.Property(e => e.ElapsedTime)
                .HasComment("耗时(秒)");

            entity.Property(e => e.StratDate)
                .HasComment("开始时间");

            entity.Property(e => e.EndDate)
                .HasComment("结束时间");

            entity.Property(e => e.Result)
                .HasComment("是否成功");

            entity.Property(e => e.ResponseContent)
                .HasColumnType("text")
                .HasComment("返回内容");

            entity.Property(e => e.ErrorMsg)
                .HasColumnType("text")
                .HasComment("异常信息");

            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("创建时间");

            entity.HasOne(d => d.QuartzOption)
            .WithMany(p => p.QuartzLog)
            .HasForeignKey(d => d.TaskId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_TaskId_QuartzOption");
        });

        base.OnModelCreating(modelBuilder);
        //base.OnModelCreating(modelBuilder, typeof(SysEntity));
    }
}
