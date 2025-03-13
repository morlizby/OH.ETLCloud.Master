using Microsoft.EntityFrameworkCore;
using OH.ETL.Core.DBManager;
using OH.ETL.Core.Extensions.AutofacManager;
using OH.ETL.Entities.OhErp;
using OH.ETL.Entities.SystemModels;

namespace OH.ETL.Core.EFDbContext
{
    public class OhErpContext : BaseDbContext, IDependency
    {
        protected override string ConnectionString
        {
            get
            {
                return DBServerProvider.OhErpDbConnectionString;
            }
        }
        public OhErpContext() : base() { }

        public OhErpContext(DbContextOptions<BaseDbContext> options) : base(options) { }

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<HrJbzlInfo> HrJbzlInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("C_Branch");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .HasColumnName("ID");

                entity.HasIndex(e => e.Name, "name");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ParentId)
                    .HasMaxLength(20)
                    .HasColumnName("ParentID");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("HN_Employee", tb => tb.HasTrigger("trg_update_HN_Employee"));
                
                entity.HasIndex(e => e.Id, "id");

                entity.HasKey(e => e.UserId).HasName("userID");
                entity.Property(e => e.UserId)
                    .HasMaxLength(20)
                    .HasColumnName("userID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasIndex(e => e.Branchid, "branchid");

                entity.HasIndex(e => e.Job, "job");

                entity.HasIndex(e => e.Position, "position");

                entity.Property(e => e.Sex)
                    .HasMaxLength(50)
                    .HasColumnName("sex");

                entity.Property(e => e.Birthday).HasColumnType("datetime");
                entity.Property(e => e.Branchid)
                    .HasMaxLength(50)
                    .HasColumnName("branchid");

                entity.Property(e => e.Cardid)
                    .HasMaxLength(50)
                    .HasColumnName("cardid");
                
                entity.Property(e => e.Department).HasMaxLength(50);
                
                entity.Property(e => e.Job)
                    .HasMaxLength(50)
                    .HasColumnName("job");

                entity.Property(e => e.Lastupdate).HasColumnType("datetime");
                entity.Property(e => e.Lastuser)
                    .HasMaxLength(50)
                    .HasColumnName("lastuser");

                entity.Property(e => e.LzDate)
                    .HasColumnType("datetime")
                    .HasColumnName("lzDate");

                entity.Property(e => e.Lzmemo)
                    .HasMaxLength(100)
                    .HasColumnName("lzmemo");
                entity.Property(e => e.Lztype)
                    .HasMaxLength(50)
                    .HasColumnName("lztype");
                entity.Property(e => e.Marry).HasMaxLength(50);

                entity.Property(e => e.MovePhone)
                    .HasMaxLength(50)
                    .HasColumnName("movePhone");
                
                entity.Property(e => e.Mz)
                    .HasMaxLength(50)
                    .HasColumnName("mz");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Position)
                    .HasMaxLength(50)
                    .HasColumnName("position");
                
                entity.Property(e => e.Speciality).HasMaxLength(50);
                
            });

            modelBuilder.Entity<HrJbzlInfo>(entity =>
            {
                entity.ToTable("HR_JbzlInfo", tb => tb.HasTrigger("trg_update_HR_JbzlInfo"));

                entity.HasIndex(e => e.Type, "type");

                entity.Property(e => e.Id).HasColumnName("id");
                
                entity.Property(e => e.Cdefine2)
                    .HasMaxLength(50)
                    .HasColumnName("cdefine2");

                entity.Property(e => e.Memo)
                    .HasMaxLength(500)
                    .HasColumnName("memo");
            });

            base.OnModelCreating(modelBuilder, typeof(OhErpEntity));
        }
    }
}
