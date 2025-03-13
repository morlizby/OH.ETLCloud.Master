using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using OH.ETL.Core.DBManager;
using OH.ETL.Core.Extensions.AutofacManager;
using OH.ETL.Entities.SystemModels;
using OH.ETL.Entities.U9Erp;


namespace OH.ETL.Core.EFDbContext;

public class U9ErpContext : BaseDbContext, IDependency
{
    protected override string ConnectionString
    {
        get
        {
            return DBServerProvider.U9CErpDbConnectionString;
        }
    }
    public U9ErpContext() : base() { }

    public U9ErpContext(DbContextOptions<BaseDbContext> options) : base(options) { }


    public virtual DbSet<BaseContact> BaseContacts { get; set; }

    public virtual DbSet<BaseContactTrl> BaseContactTrls { get; set; }

    public virtual DbSet<BaseOrganization> BaseOrganizations { get; set; }

    public virtual DbSet<BaseOrganizationTrl> BaseOrganizationTrls { get; set; }

    public virtual DbSet<BaseUser> BaseUsers { get; set; }

    public virtual DbSet<BaseUserTrl> BaseUserTrls { get; set; }

    public virtual DbSet<CboApplyForInfo> CboApplyForInfos { get; set; }

    public virtual DbSet<CboEmployCategory> CboEmployCategories { get; set; }

    public virtual DbSet<CboEmployCategoryTrl> CboEmployCategoryTrls { get; set; }

    public virtual DbSet<CboEmployeeArchive> CboEmployeeArchives { get; set; }

    public virtual DbSet<CboEmployeeArchiveTrl> CboEmployeeArchiveTrls { get; set; }

    public virtual DbSet<CboEmployeeAssignment> CboEmployeeAssignments { get; set; }

    public virtual DbSet<CboEmployeeAssignmentTrl> CboEmployeeAssignmentTrls { get; set; }

    public virtual DbSet<CboEmpolyeeCateAlterRecord> CboEmpolyeeCateAlterRecords { get; set; }

    public virtual DbSet<CboEmpolyeeCateAlterRecordTrl> CboEmpolyeeCateAlterRecordTrls { get; set; }

    public virtual DbSet<CboHrattachRelation> CboHrattachRelations { get; set; }

    public virtual DbSet<CboHrattachRelationTrl> CboHrattachRelationTrls { get; set; }

    public virtual DbSet<CboJob> CboJobs { get; set; }

    public virtual DbSet<CboJobHistory> CboJobHistories { get; set; }

    public virtual DbSet<CboJobHistoryTrl> CboJobHistoryTrls { get; set; }

    public virtual DbSet<CboJobTrl> CboJobTrls { get; set; }

    public virtual DbSet<CboOperator> CboOperators { get; set; }

    public virtual DbSet<CboOperatorLine> CboOperatorLines { get; set; }

    public virtual DbSet<CboPerson> CboPersons { get; set; }

    public virtual DbSet<CboPersonTrl> CboPersonTrls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
        //默认禁用实体跟踪
        optionsBuilder = optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaseContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Base_Contact");

            entity.ToTable("Base_Contact");

            entity.HasIndex(e => e.Code, "IX_Base_Contact").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.ContactType).HasDefaultValue(1);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValue(new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime");
            entity.Property(e => e.DefaultEmail).HasMaxLength(50);
            entity.Property(e => e.DefaultFaxNum).HasMaxLength(50);
            entity.Property(e => e.DefaultIms)
                .HasMaxLength(50)
                .HasColumnName("DefaultIMs");
            entity.Property(e => e.DefaultMobilNum).HasMaxLength(50);
            entity.Property(e => e.DefaultPhoneNum).HasMaxLength(50);
            entity.Property(e => e.DefaultUrladdr)
                .HasMaxLength(50)
                .HasColumnName("DefaultURLAddr");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.EffectiveDisableDate)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("Effective_DisableDate");
            entity.Property(e => e.EffectiveEffectiveDate)
                .HasDefaultValue(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("Effective_EffectiveDate");
            entity.Property(e => e.EffectiveIsEffective)
                .HasDefaultValue(true)
                .HasColumnName("Effective_IsEffective");
            entity.Property(e => e.Gender).HasDefaultValue(0);
            entity.Property(e => e.IsCustomerContact).HasDefaultValue(false);
            entity.Property(e => e.IsEmailNotify).HasDefaultValue(true);
            entity.Property(e => e.IsEmployeeContact).HasDefaultValue(false);
            entity.Property(e => e.IsMessageNotify).HasDefaultValue(true);
            entity.Property(e => e.IsOrgContact).HasDefaultValue(false);
            entity.Property(e => e.IsShowUserName).HasDefaultValue(true);
            entity.Property(e => e.IsSupplierContact).HasDefaultValue(false);
            entity.Property(e => e.Job).HasMaxLength(50);
            entity.Property(e => e.MasterSite).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValue(new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime");
            entity.Property(e => e.PersonNameDisplayName)
                .HasMaxLength(50)
                .HasColumnName("PersonName_DisplayName");
            entity.Property(e => e.PersonNameFirstName)
                .HasMaxLength(50)
                .HasColumnName("PersonName_FirstName");
            entity.Property(e => e.PersonNameLastName)
                .HasMaxLength(50)
                .HasColumnName("PersonName_LastName");
            entity.Property(e => e.PersonNameMiddleName)
                .HasMaxLength(50)
                .HasColumnName("PersonName_MiddleName");
            entity.Property(e => e.PersonNameNickName)
                .HasMaxLength(50)
                .HasColumnName("PersonName_NickName");
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<BaseContactTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_Base_Contact_Trl");

            entity.ToTable("Base_Contact_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<BaseOrganization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Base_Organization");

            entity.ToTable("Base_Organization");

            entity.HasIndex(e => e.Code, "IX_Base_Organization").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BomrefOrg).HasColumnName("BOMRefOrg");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CompanyType).HasDefaultValue(0);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValue(new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.EffectiveDisableDate)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("Effective_DisableDate");
            entity.Property(e => e.EffectiveEffectiveDate)
                .HasDefaultValue(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("Effective_EffectiveDate");
            entity.Property(e => e.EffectiveIsEffective)
                .HasDefaultValue(true)
                .HasColumnName("Effective_IsEffective");
            entity.Property(e => e.IsProfitCenter).HasDefaultValue(false);
            entity.Property(e => e.ManageType).HasDefaultValue(0);
            entity.Property(e => e.MasterSite).HasMaxLength(500);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValue(new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime");
            entity.Property(e => e.OrgClassify).HasDefaultValue(1);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<BaseOrganizationTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_Base_Organization_Trl");

            entity.ToTable("Base_Organization_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.DescFlexFieldCombineName)
                .HasMaxLength(4000)
                .HasColumnName("DescFlexField_CombineName");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<BaseUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Base_User");

            entity.ToTable("Base_User", tb => tb.HasTrigger("Base_PreventBatchUpdateUserPasswordTigger"));

            entity.HasIndex(e => e.Code, "IX_Base_User").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValue(new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime");
            entity.Property(e => e.Cust4SeeyonA8loginName)
                .HasMaxLength(50)
                .HasColumnName("Cust4SeeyonA8LoginName");
            entity.Property(e => e.CustomerCode).HasMaxLength(50);
            entity.Property(e => e.CustomerId)
                .HasDefaultValue(-1L)
                .HasColumnName("CustomerID");
            entity.Property(e => e.CustomerName).HasMaxLength(50);
            entity.Property(e => e.DomainName).HasMaxLength(50);
            entity.Property(e => e.EffectiveDisableDate)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("Effective_DisableDate");
            entity.Property(e => e.EffectiveEffectiveDate)
                .HasDefaultValue(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("Effective_EffectiveDate");
            entity.Property(e => e.EffectiveIsEffective)
                .HasDefaultValue(true)
                .HasColumnName("Effective_IsEffective");
            entity.Property(e => e.IsAlive).HasDefaultValue(true);
            entity.Property(e => e.IsCaenabled).HasColumnName("IsCAEnabled");
            entity.Property(e => e.IsEnableMfa)
                .HasDefaultValue(false)
                .HasColumnName("IsEnableMFA");
            entity.Property(e => e.MasterSite).HasMaxLength(50);
            entity.Property(e => e.MaxUserCount).HasDefaultValue(1);
            entity.Property(e => e.Mfakey)
                .HasMaxLength(50)
                .HasColumnName("MFAKey");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn)
                .HasDefaultValue(new DateTime(2005, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.NodeCode).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PasswordState).HasDefaultValue(0);
            entity.Property(e => e.Photo).HasMaxLength(4000);
            entity.Property(e => e.PortalManager).HasDefaultValue(false);
            entity.Property(e => e.PublicKey).HasMaxLength(50);
            entity.Property(e => e.Salt).HasMaxLength(36);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.Style).HasDefaultValue(1);
            entity.Property(e => e.SupplierCode).HasMaxLength(50);
            entity.Property(e => e.SupplierId)
                .HasDefaultValue(-1L)
                .HasColumnName("SupplierID");
            entity.Property(e => e.SupplierName).HasMaxLength(50);
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
            entity.Property(e => e.Tip).HasMaxLength(50);
            entity.Property(e => e.TipAnswer).HasMaxLength(50);
            entity.Property(e => e.UuId)
                .HasMaxLength(50)
                .HasColumnName("UuID");
            entity.Property(e => e.YhtUserCode)
                .HasMaxLength(50)
                .HasColumnName("YHT_UserCode");
            entity.Property(e => e.YhtUserId)
                .HasMaxLength(50)
                .HasColumnName("YHT_UserId");
        });

        modelBuilder.Entity<BaseUserTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_Base_User_Trl");

            entity.ToTable("Base_User_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.DisplayName).HasMaxLength(50);
        });

        modelBuilder.Entity<CboApplyForInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_ApplyForInfo");

            entity.ToTable("CBO_ApplyForInfo");

            entity.HasIndex(e => new { e.Person, e.ApplyDate, e.ApplyOrg, e.ApplyJob, e.ApplyPosition, e.ApplyStatus, e.ApplyDept, e.ApplyBusinessOrg }, "UFIDA_U9_CBO_HR_Person_ApplyForInfo_BusinessKey_Index").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ApplyDate).HasColumnType("datetime");
            entity.Property(e => e.ApplyDoc).HasDefaultValue(0L);
            entity.Property(e => e.ApplyStatus).HasDefaultValue(-1);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<CboEmployCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_EmployCategory");

            entity.ToTable("CBO_EmployCategory");

            entity.HasIndex(e => new { e.Code, e.Org }, "UFIDA_U9_CBO_HR_EmploymentCategory_EmployCategory_BusinessKey_Index").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.EffectiveRangeDisableDate)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("EffectiveRange_DisableDate");
            entity.Property(e => e.EffectiveRangeEffectiveDate)
                .HasDefaultValue(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("EffectiveRange_EffectiveDate");
            entity.Property(e => e.EffectiveRangeIsEffective)
                .HasDefaultValue(true)
                .HasColumnName("EffectiveRange_IsEffective");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Segment1).HasMaxLength(50);
            entity.Property(e => e.Segment10).HasMaxLength(50);
            entity.Property(e => e.Segment11).HasMaxLength(50);
            entity.Property(e => e.Segment12).HasMaxLength(50);
            entity.Property(e => e.Segment13).HasMaxLength(50);
            entity.Property(e => e.Segment14).HasMaxLength(50);
            entity.Property(e => e.Segment15).HasMaxLength(50);
            entity.Property(e => e.Segment16).HasMaxLength(50);
            entity.Property(e => e.Segment17).HasMaxLength(50);
            entity.Property(e => e.Segment18).HasMaxLength(50);
            entity.Property(e => e.Segment19).HasMaxLength(50);
            entity.Property(e => e.Segment2).HasMaxLength(50);
            entity.Property(e => e.Segment20).HasMaxLength(50);
            entity.Property(e => e.Segment3).HasMaxLength(50);
            entity.Property(e => e.Segment4).HasMaxLength(50);
            entity.Property(e => e.Segment5).HasMaxLength(50);
            entity.Property(e => e.Segment6).HasMaxLength(50);
            entity.Property(e => e.Segment7).HasMaxLength(50);
            entity.Property(e => e.Segment8).HasMaxLength(50);
            entity.Property(e => e.Segment9).HasMaxLength(50);
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
            entity.Property(e => e.WorkCardBbg).HasColumnName("WorkCardBBg");
        });

        modelBuilder.Entity<CboEmployCategoryTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_CBO_EmployCategory_Trl");

            entity.ToTable("CBO_EmployCategory_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.AliasName).HasMaxLength(50);
            entity.Property(e => e.CombineName).HasMaxLength(250);
            entity.Property(e => e.DescFlexFieldCombineName)
                .HasMaxLength(4000)
                .HasColumnName("DescFlexField_CombineName");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(50);
        });

        modelBuilder.Entity<CboEmployeeArchive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_EmployeeArchive");

            entity.ToTable("CBO_EmployeeArchive");

            entity.HasIndex(e => new { e.Person, e.OwnerOrg, e.WorkingOrg, e.EntranceDate }, "UFIDA_U9_CBO_HR_Person_EmployeeArchive_BusinessKey_Index").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Amende)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)");
            entity.Property(e => e.BuilderEmpCode).HasMaxLength(50);
            entity.Property(e => e.Compensate)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.DimissionDate).HasColumnType("datetime");
            entity.Property(e => e.DimissionType).HasDefaultValue(-1);
            entity.Property(e => e.EmployeeCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.EndCheckTime).HasColumnType("datetime");
            entity.Property(e => e.EndPositionActive).HasDefaultValue(-1);
            entity.Property(e => e.EntranceChannel).HasDefaultValue(-1);
            entity.Property(e => e.EntranceDate).HasColumnType("datetime");
            entity.Property(e => e.EntranceDoc).HasDefaultValue(0L);
            entity.Property(e => e.EntranceEndDate).HasColumnType("datetime");
            entity.Property(e => e.EntranceType).HasDefaultValue(0);
            entity.Property(e => e.FirstTimes).HasDefaultValue(1);
            entity.Property(e => e.InnerAge)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)");
            entity.Property(e => e.InnerAgeValue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)");
            entity.Property(e => e.IsApplicant).HasDefaultValue(false);
            entity.Property(e => e.IsFirst).HasDefaultValue(true);
            entity.Property(e => e.IsProbation).HasDefaultValue(false);
            entity.Property(e => e.IsUnder6wTax).HasDefaultValue(false);
            entity.Property(e => e.LeaveDoc).HasDefaultValue(0L);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PositionActive).HasDefaultValue(-1);
            entity.Property(e => e.ProEndDate).HasColumnType("datetime");
            entity.Property(e => e.SalaryAblanceTime).HasColumnType("datetime");
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<CboEmployeeArchiveTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_CBO_EmployeeArchive_Trl");

            entity.ToTable("CBO_EmployeeArchive_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.DescFlexFieldCombineName)
                .HasMaxLength(4000)
                .HasColumnName("DescFlexField_CombineName");
        });

        modelBuilder.Entity<CboEmployeeAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_EmployeeAssignment");

            entity.ToTable("CBO_EmployeeAssignment");

            entity.HasIndex(e => new { e.Sequence, e.Employee, e.AssgnBeginDate, e.BusinessOrg, e.Dept, e.Job, e.Position }, "UFIDA_U9_CBO_HR_Person_EmployeeAssignment_BusinessKey_Index").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AssgnBeginDate).HasColumnType("datetime");
            entity.Property(e => e.AssgnEndDate).HasColumnType("datetime");
            entity.Property(e => e.AssignActive).HasDefaultValue(-1);
            entity.Property(e => e.AssignBeforeAlter).HasDefaultValue(0L);
            entity.Property(e => e.AssignDoc).HasDefaultValue(0L);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.EndAssignActive).HasDefaultValue(-1);
            entity.Property(e => e.IsMain).HasDefaultValue(false);
            entity.Property(e => e.IsProbation).HasDefaultValue(false);
            entity.Property(e => e.IsTakeUp).HasDefaultValue(false);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.PlanChangeDate).HasColumnType("datetime");
            entity.Property(e => e.PlanEndDate).HasColumnType("datetime");
            entity.Property(e => e.PlanEndDoc).HasDefaultValue(0L);
            entity.Property(e => e.ProBeginDate).HasColumnType("datetime");
            entity.Property(e => e.ProDoc).HasDefaultValue(0L);
            entity.Property(e => e.ProMonth)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)");
            entity.Property(e => e.ProOverDate).HasColumnType("datetime");
            entity.Property(e => e.ProbationDelayDate).HasColumnType("datetime");
            entity.Property(e => e.ProbationStatus).HasDefaultValue(0);
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<CboEmployeeAssignmentTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_CBO_EmployeeAssignment_Trl");

            entity.ToTable("CBO_EmployeeAssignment_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.DescFlexFieldCombineName)
                .HasMaxLength(4000)
                .HasColumnName("DescFlexField_CombineName");
            entity.Property(e => e.PositionReasons).HasMaxLength(50);
            entity.Property(e => e.ProSumUp).HasMaxLength(50);
            entity.Property(e => e.Reasons).HasMaxLength(50);
        });

        modelBuilder.Entity<CboEmpolyeeCateAlterRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_EmpolyeeCateAlterRecord");

            entity.ToTable("CBO_EmpolyeeCateAlterRecord");

            entity.HasIndex(e => new { e.StartDate, e.Employee }, "UFIDA_U9_CBO_HR_Person_EmpolyeeCateAlterRecord_BusinessKey_Index").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ActiveEnd).HasDefaultValue(-1);
            entity.Property(e => e.CreateActive).HasDefaultValue(-1);
            entity.Property(e => e.CreateDoc).HasDefaultValue(0L);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.EndDoc).HasDefaultValue(0L);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<CboEmpolyeeCateAlterRecordTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_CBO_EmpolyeeCateAlterRecord_Trl");

            entity.ToTable("CBO_EmpolyeeCateAlterRecord_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.DescFlexFieldCombineName)
                .HasMaxLength(4000)
                .HasColumnName("DescFlexField_CombineName");
            entity.Property(e => e.EndResonal).HasMaxLength(50);
        });

        modelBuilder.Entity<CboHrattachRelation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_HRAttachRelation");

            entity.ToTable("CBO_HRAttachRelation");

            entity.HasIndex(e => new { e.Person, e.StartDate }, "UFIDA_U9_CBO_HR_Person_HRAttachRelation_BusinessKey_Index").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.EndActivity).HasDefaultValue(-1);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<CboHrattachRelationTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_CBO_HRAttachRelation_Trl");

            entity.ToTable("CBO_HRAttachRelation_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.EndReason).HasMaxLength(50);
        });

        modelBuilder.Entity<CboJob>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_Job");

            entity.ToTable("CBO_Job");

            entity.HasIndex(e => new { e.Org, e.Code }, "UFIDA_U9_CBO_HR_Job_Job_BusinessKey_Index").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AuditingStatus).HasDefaultValue(0);
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.DimissionBeforeDay).HasDefaultValue(0);
            entity.Property(e => e.EffectiveRangeDisableDate)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("EffectiveRange_DisableDate");
            entity.Property(e => e.EffectiveRangeEffectiveDate)
                .HasDefaultValue(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("EffectiveRange_EffectiveDate");
            entity.Property(e => e.EffectiveRangeIsEffective)
                .HasDefaultValue(true)
                .HasColumnName("EffectiveRange_IsEffective");
            entity.Property(e => e.IsAllowDeformity).HasDefaultValue(true);
            entity.Property(e => e.IsAllowForeigner).HasDefaultValue(true);
            entity.Property(e => e.IsAllowRetire).HasDefaultValue(true);
            entity.Property(e => e.IsBudgetControl).HasDefaultValue(false);
            entity.Property(e => e.IsGovControl).HasDefaultValue(false);
            entity.Property(e => e.IsHead).HasDefaultValue(false);
            entity.Property(e => e.JobOverLapTerm)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)");
            entity.Property(e => e.JobOverLapTermType).HasDefaultValue(1);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.ProbationTerm)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)");
            entity.Property(e => e.SalaryCalculateStyle).HasDefaultValue(0);
            entity.Property(e => e.Segment1).HasMaxLength(50);
            entity.Property(e => e.Segment10).HasMaxLength(50);
            entity.Property(e => e.Segment11).HasMaxLength(50);
            entity.Property(e => e.Segment12).HasMaxLength(50);
            entity.Property(e => e.Segment13).HasMaxLength(50);
            entity.Property(e => e.Segment14).HasMaxLength(50);
            entity.Property(e => e.Segment15).HasMaxLength(50);
            entity.Property(e => e.Segment16).HasMaxLength(50);
            entity.Property(e => e.Segment17).HasMaxLength(50);
            entity.Property(e => e.Segment18).HasMaxLength(50);
            entity.Property(e => e.Segment19).HasMaxLength(50);
            entity.Property(e => e.Segment2).HasMaxLength(50);
            entity.Property(e => e.Segment20).HasMaxLength(50);
            entity.Property(e => e.Segment3).HasMaxLength(50);
            entity.Property(e => e.Segment4).HasMaxLength(50);
            entity.Property(e => e.Segment5).HasMaxLength(50);
            entity.Property(e => e.Segment6).HasMaxLength(50);
            entity.Property(e => e.Segment7).HasMaxLength(50);
            entity.Property(e => e.Segment8).HasMaxLength(50);
            entity.Property(e => e.Segment9).HasMaxLength(50);
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
            entity.Property(e => e.UpProportion)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)");
            entity.Property(e => e.WfcurrentState)
                .HasDefaultValue(-1)
                .HasColumnName("WFCurrentState");
            entity.Property(e => e.WforiginalState)
                .HasDefaultValue(-1)
                .HasColumnName("WFOriginalState");
        });

        modelBuilder.Entity<CboJobHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_JobHistory");

            entity.ToTable("CBO_JobHistory");

            entity.HasIndex(e => new { e.Person, e.StartDate }, "UFIDA_U9_CBO_HR_Person_JobHistoryInfo_BusinessKey_Index").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.Employee).HasDefaultValue(0L);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<CboJobHistoryTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_CBO_JobHistory_Trl");

            entity.ToTable("CBO_JobHistory_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.CompanyName).HasMaxLength(50);
            entity.Property(e => e.DescFlexFieldCombineName)
                .HasMaxLength(4000)
                .HasColumnName("DescFlexField_CombineName");
            entity.Property(e => e.DimissionReason).HasMaxLength(50);
            entity.Property(e => e.Job).HasMaxLength(50);
            entity.Property(e => e.JobGrade).HasMaxLength(50);
            entity.Property(e => e.MajorAchievement).HasMaxLength(50);
            entity.Property(e => e.Voucher).HasMaxLength(50);
            entity.Property(e => e.VoucherContact).HasMaxLength(50);
        });

        modelBuilder.Entity<CboJobTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_CBO_Job_Trl");

            entity.ToTable("CBO_Job_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.AliasName).HasMaxLength(50);
            entity.Property(e => e.CombineName).HasMaxLength(250);
            entity.Property(e => e.DescFlexFieldCombineName)
                .HasMaxLength(4000)
                .HasColumnName("DescFlexField_CombineName");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(50);
        });

        modelBuilder.Entity<CboOperator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_Operators");

            entity.ToTable("CBO_Operators");

            entity.HasIndex(e => new { e.Org, e.Code }, "Operators_BK_Idx").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.EffectiveDisableDate)
                .HasDefaultValue(new DateTime(9999, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("Effective_DisableDate");
            entity.Property(e => e.EffectiveEffectiveDate)
                .HasDefaultValue(new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnType("datetime")
                .HasColumnName("Effective_EffectiveDate");
            entity.Property(e => e.EffectiveIsEffective)
                .HasDefaultValue(true)
                .HasColumnName("Effective_IsEffective");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<CboOperatorLine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_OperatorLine");

            entity.ToTable("CBO_OperatorLine");

            entity.HasIndex(e => new { e.OperatorType, e.Operators }, "I_OperatorLine_BK").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
        });

        modelBuilder.Entity<CboPerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_CBO_Person");

            entity.ToTable("CBO_Person");

            entity.HasIndex(e => new { e.CertificateType, e.PersonId, e.Nationality }, "UFIDA_U9_CBO_HR_Person_Person_BusinessKey_Index").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Age)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 1)");
            entity.Property(e => e.ApplyStatus).HasDefaultValue(-1);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.BloodType).HasDefaultValue(-1);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DescFlexFieldContextValue)
                .HasMaxLength(50)
                .HasColumnName("DescFlexField_ContextValue");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg1");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg10");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg11");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg12");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg13");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg14");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg15");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg16");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg17");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg18");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg19");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg2");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg20");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg21");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg22");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg23");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg24");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg25");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg26");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg27");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg28");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg29");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg3");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg30");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg4");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg5");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg6");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg7");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg8");
            entity.Property(e => e.DescFlexFieldPrivateDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PrivateDescSeg9");
            entity.Property(e => e.DescFlexFieldPubDescSeg1)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg1");
            entity.Property(e => e.DescFlexFieldPubDescSeg10)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg10");
            entity.Property(e => e.DescFlexFieldPubDescSeg11)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg11");
            entity.Property(e => e.DescFlexFieldPubDescSeg12)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg12");
            entity.Property(e => e.DescFlexFieldPubDescSeg13)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg13");
            entity.Property(e => e.DescFlexFieldPubDescSeg14)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg14");
            entity.Property(e => e.DescFlexFieldPubDescSeg15)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg15");
            entity.Property(e => e.DescFlexFieldPubDescSeg16)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg16");
            entity.Property(e => e.DescFlexFieldPubDescSeg17)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg17");
            entity.Property(e => e.DescFlexFieldPubDescSeg18)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg18");
            entity.Property(e => e.DescFlexFieldPubDescSeg19)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg19");
            entity.Property(e => e.DescFlexFieldPubDescSeg2)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg2");
            entity.Property(e => e.DescFlexFieldPubDescSeg20)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg20");
            entity.Property(e => e.DescFlexFieldPubDescSeg21)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg21");
            entity.Property(e => e.DescFlexFieldPubDescSeg22)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg22");
            entity.Property(e => e.DescFlexFieldPubDescSeg23)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg23");
            entity.Property(e => e.DescFlexFieldPubDescSeg24)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg24");
            entity.Property(e => e.DescFlexFieldPubDescSeg25)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg25");
            entity.Property(e => e.DescFlexFieldPubDescSeg26)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg26");
            entity.Property(e => e.DescFlexFieldPubDescSeg27)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg27");
            entity.Property(e => e.DescFlexFieldPubDescSeg28)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg28");
            entity.Property(e => e.DescFlexFieldPubDescSeg29)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg29");
            entity.Property(e => e.DescFlexFieldPubDescSeg3)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg3");
            entity.Property(e => e.DescFlexFieldPubDescSeg30)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg30");
            entity.Property(e => e.DescFlexFieldPubDescSeg31)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg31");
            entity.Property(e => e.DescFlexFieldPubDescSeg32)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg32");
            entity.Property(e => e.DescFlexFieldPubDescSeg33)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg33");
            entity.Property(e => e.DescFlexFieldPubDescSeg34)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg34");
            entity.Property(e => e.DescFlexFieldPubDescSeg35)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg35");
            entity.Property(e => e.DescFlexFieldPubDescSeg36)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg36");
            entity.Property(e => e.DescFlexFieldPubDescSeg37)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg37");
            entity.Property(e => e.DescFlexFieldPubDescSeg38)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg38");
            entity.Property(e => e.DescFlexFieldPubDescSeg39)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg39");
            entity.Property(e => e.DescFlexFieldPubDescSeg4)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg4");
            entity.Property(e => e.DescFlexFieldPubDescSeg40)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg40");
            entity.Property(e => e.DescFlexFieldPubDescSeg41)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg41");
            entity.Property(e => e.DescFlexFieldPubDescSeg42)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg42");
            entity.Property(e => e.DescFlexFieldPubDescSeg43)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg43");
            entity.Property(e => e.DescFlexFieldPubDescSeg44)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg44");
            entity.Property(e => e.DescFlexFieldPubDescSeg45)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg45");
            entity.Property(e => e.DescFlexFieldPubDescSeg46)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg46");
            entity.Property(e => e.DescFlexFieldPubDescSeg47)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg47");
            entity.Property(e => e.DescFlexFieldPubDescSeg48)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg48");
            entity.Property(e => e.DescFlexFieldPubDescSeg49)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg49");
            entity.Property(e => e.DescFlexFieldPubDescSeg5)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg5");
            entity.Property(e => e.DescFlexFieldPubDescSeg50)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg50");
            entity.Property(e => e.DescFlexFieldPubDescSeg6)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg6");
            entity.Property(e => e.DescFlexFieldPubDescSeg7)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg7");
            entity.Property(e => e.DescFlexFieldPubDescSeg8)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg8");
            entity.Property(e => e.DescFlexFieldPubDescSeg9)
                .HasMaxLength(1000)
                .HasColumnName("DescFlexField_PubDescSeg9");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("EMail");
            entity.Property(e => e.Fax).HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.HrchangeOrg)
                .HasDefaultValue(0)
                .HasColumnName("HRChangeOrg");
            entity.Property(e => e.InStoreDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeformity).HasDefaultValue(false);
            entity.Property(e => e.IsDirectNode).HasDefaultValue(true);
            entity.Property(e => e.IsSecrecy).HasDefaultValue(false);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.LabourContractOrg).HasDefaultValue(0);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.LogisticsOrg).HasDefaultValue(1);
            entity.Property(e => e.LosreviseValue)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)")
                .HasColumnName("LOSReviseValue");
            entity.Property(e => e.MarriageStatus).HasDefaultValue(-1);
            entity.Property(e => e.MiddleName).HasMaxLength(50);
            entity.Property(e => e.MobilePhone).HasMaxLength(50);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.NativePlace).HasMaxLength(50);
            entity.Property(e => e.NickName).HasMaxLength(50);
            entity.Property(e => e.OccupationDate).HasColumnType("datetime");
            entity.Property(e => e.PayOrg).HasDefaultValue(0);
            entity.Property(e => e.PersonId)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("PersonID");
            entity.Property(e => e.PersonalStatus).HasDefaultValue(-1);
            entity.Property(e => e.PoliticalStatus).HasDefaultValue(-1);
            entity.Property(e => e.RegisteredResidence).HasMaxLength(50);
            entity.Property(e => e.RegisteredResidenceType).HasDefaultValue(-1);
            entity.Property(e => e.Religion).HasDefaultValue(-1);
            entity.Property(e => e.RetirTime).HasColumnType("datetime");
            entity.Property(e => e.RetireAge)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 9)");
            entity.Property(e => e.Sex).HasDefaultValue(-1);
            entity.Property(e => e.StatusStartDate).HasColumnType("datetime");
            entity.Property(e => e.SysVersion).HasDefaultValue(0L);
            entity.Property(e => e.TalentLevel).HasDefaultValue(-1);
            entity.Property(e => e.TalentSource).HasDefaultValue(-1);
            entity.Property(e => e.TelPhone).HasMaxLength(50);
            entity.Property(e => e.WfcurrentState)
                .HasDefaultValue(-1)
                .HasColumnName("WFCurrentState");
            entity.Property(e => e.WforiginalState)
                .HasDefaultValue(-1)
                .HasColumnName("WFOriginalState");
            entity.Property(e => e.WorkAge)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(24, 1)");
            entity.Property(e => e.WorkQualification).HasDefaultValue(-1);
        });

        modelBuilder.Entity<CboPersonTrl>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.SysMlflag }).HasName("pk_CBO_Person_Trl");

            entity.ToTable("CBO_Person_Trl");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SysMlflag)
                .HasMaxLength(20)
                .HasColumnName("SysMLFlag");
            entity.Property(e => e.DescFlexFieldCombineName)
                .HasMaxLength(4000)
                .HasColumnName("DescFlexField_CombineName");
        });

        base.OnModelCreating(modelBuilder, typeof(U9ErpEntity));
    }
}
