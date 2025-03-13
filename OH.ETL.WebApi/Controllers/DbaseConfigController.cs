using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OH.ETL.Core.EFDbContext;
using OH.ETL.Core.Utils;
using OH.ETL.Entities.DomainModels;
using OH.ETL.Entities.Enums;

namespace OH.ETL.WebApi.Controllers;


/// <summary>
/// ETL数据清洗 Api
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class DbaseConfigController : ControllerBase
{
    private readonly SysDbContext _context;
    private readonly ILogger<DbaseConfigController> _logger;

    private const string SecurityKey = "Ouhai1314$Key#By";

    public DbaseConfigController(SysDbContext context, ILogger<DbaseConfigController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 初始化ETL数据库账套
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult BrwInitDbaseLedger()
    {
        //_context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        var OhErpHead = new SysDbConfigHead
        {
            ServerAddr = "192.168.1.8",
            Port = 1433,
            UserID = "sa",
            UserPwd = "ouhai1234$".EncryptDES(SecurityKey),
            RepositoryType = DataCategory.MsSql,
            EnvCategory = EnvironCategory.Production
        };

        var OhErpDetails = new List<SysDbConfigDetail>()
        {
            new(){DataRepository = "DB_HekeERP".EncryptDES(SecurityKey),Desciption = "*",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPBudget".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPDevelopment".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPDevelopmentFile".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPDrawFile".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFile".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFinance".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFinanceFile".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFineReport".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            //new(){DataRepository = "DB_HekeERPFineReport1".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFixedAsset".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPJpgFile".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPLogistics".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPLogisticsFile".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPMESFineReport".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HeKeERPOnline".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPProductionSales".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPQuality".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPReport".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HeKeERPSmartAnton".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},

            new(){DataRepository = "DB_HekeERPU9C".EncryptDES(SecurityKey),Desciption = "*",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeLog".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_OAdesign".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_OAdesignBak_ONE".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
            new(){DataRepository = "DB_OUHAI".EncryptDES(SecurityKey),Desciption = "",SysDbConfigHead = OhErpHead},
        };
        OhErpHead.SysDbConfigDetail = OhErpDetails;
        _context.SysDbConfigHeads.Add(OhErpHead);

        var U9CTestHead = new SysDbConfigHead
        {
            ServerAddr = "192.168.1.22",
            Port = 1433,
            UserID = "sa",
            UserPwd = "OUHAI1234".EncryptDES(SecurityKey),
            RepositoryType = DataCategory.MsSql,
            EnvCategory = EnvironCategory.Test
        };
        var U9CTestDetails = new List<SysDbConfigDetail>()
        {
            new(){DataRepository = "0509wdk".EncryptDES(SecurityKey), Desciption = "", SysDbConfigHead = U9CTestHead},
            new(){DataRepository = "U9testC".EncryptDES(SecurityKey), Desciption = "", SysDbConfigHead = U9CTestHead},
            new(){DataRepository = "U9test0121".EncryptDES(SecurityKey), Desciption="*", SysDbConfigHead = U9CTestHead}
        };
        U9CTestHead.SysDbConfigDetail = U9CTestDetails;
        _context.SysDbConfigHeads.Add(U9CTestHead);

        var DataETLHead = new SysDbConfigHead
        {
            ServerAddr = "192.168.1.50",
            Port = 3306,
            UserID = "root",
            UserPwd = "Efc9679".EncryptDES(SecurityKey),
            RepositoryType = DataCategory.MySql,
            EnvCategory = EnvironCategory.Development
        };

        var DataETLDetails = new List<SysDbConfigDetail>()
        {
            new(){DataRepository = "DataETLDb".EncryptDES(SecurityKey), Desciption="*", SysDbConfigHead = DataETLHead}
        };
        DataETLHead.SysDbConfigDetail = DataETLDetails;
        _context.SysDbConfigHeads.Add(DataETLHead);

        _context.SaveChanges();

        return Ok(new
        {
            code = "200",
            msg = "OK"
        });
    }

    /// <summary>
    /// 获取连接字符串
    /// </summary>
    /// <param name="dataRepository">数据仓库名</param>
    /// <returns></returns>
    [HttpGet(Name = "GetDbConnString")]
    public IActionResult Get(string dataRepository)
    {
        //U9test0121
        var queryDBbaseConfig = (from a in _context.SysDbConfigHeads.ToList()
                                 join b in _context.SysDbConfigDetails.ToList() on a.Id equals b.HeadId
                                 where b.DataRepository == dataRepository.EncryptDES(SecurityKey)
                                 select new SqlConnectionStringBuilder
                                 {
                                     DataSource = a.RepositoryType == DataCategory.MsSql ? a.ServerAddr : string.Concat(a.ServerAddr, ":", a.Port),
                                     InitialCatalog = b.DataRepository.DecryptDES(SecurityKey),
                                     UserID = a.UserID,
                                     Password = a.UserPwd.DecryptDES(SecurityKey),
                                     MultipleActiveResultSets = true,
                                     TrustServerCertificate = true
                                 }).First();

        return Ok(new
        {
            code = "200",
            msg = "OK",
            data = queryDBbaseConfig.ConnectionString
        });
    }
}
