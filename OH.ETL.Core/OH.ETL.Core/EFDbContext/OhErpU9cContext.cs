using Microsoft.EntityFrameworkCore;
using OH.ETL.Core.DBManager;
using OH.ETL.Core.Extensions.AutofacManager;
using OH.ETL.Entities.OhErp;
using OH.ETL.Entities.SystemModels;

namespace OH.ETL.Core.EFDbContext
{
    public class OhErpU9cContext : BaseDbContext, IDependency
    {
        protected override string ConnectionString
        {
            get
            {
                return DBServerProvider.OhErpU9cDbConnectionString;
            }
        }

        public OhErpU9cContext() { }

        public OhErpU9cContext(DbContextOptions<BaseDbContext> options) : base(options) { }

        public virtual DbSet<BranchMapper> BranchMappers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);


            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchMapper>(entity =>
            {
                entity.ToTable("C_Branch_Mapper");

                entity.Property(e => e.CreateTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.OrgCode).HasMaxLength(50);
                entity.Property(e => e.OrgId).HasColumnName("OrgID");
                entity.Property(e => e.U9dept).HasColumnName("U9Dept");

                entity.Property(e => e.U9deptId)
                    .HasMaxLength(20)
                    .HasColumnName("U9DeptId");
                entity.Property(e => e.U9deptName)
                    .HasMaxLength(20)
                    .HasColumnName("U9DeptName");
                entity.Property(e => e.U9deptType)
                    .HasMaxLength(20)
                    .HasColumnName("U9DeptType");
            });

            base.OnModelCreating(modelBuilder, typeof(OhErpEntity));
        }
    }
}
