using Microsoft.AspNetCore.Mvc;
using DataETLBuilder.EFContext;
using Microsoft.Data.SqlClient;
using DataETLBuilder.Entity;
using DataETLBuilder.Enums;
using DataETLBuilder.Utils;
namespace DataETLBuilder.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        //_context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        var OhErpHead = new SysDbaseConfigHead
        {
            ServerAddr = "192.168.1.8",
            Port = 1433,
            UserID = "sa",
            UserPwd = "ouhai1234$".EncryptDES(SecurityKey),
            RepositoryType = DataCategory.MsSql,
            EnvCategory = EnvironCategory.Production
        };

        var OhErpDetails = new List<SysDbaseConfigDetail>()
        {
            new(){DataRepository = "DB_HekeERP".EncryptDES(SecurityKey),Desciption = "*",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPBudget".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPDevelopment".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPDevelopmentFile".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPDrawFile".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFile".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFinance".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFinanceFile".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFineReport".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            //new(){DataRepository = "DB_HekeERPFineReport1".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPFixedAsset".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPJpgFile".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPLogistics".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPLogisticsFile".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPMESFineReport".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HeKeERPOnline".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPProductionSales".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPQuality".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeERPReport".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HeKeERPSmartAnton".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},

            new(){DataRepository = "DB_HekeERPU9C".EncryptDES(SecurityKey),Desciption = "*",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_HekeLog".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_OAdesign".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_OAdesignBak_ONE".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
            new(){DataRepository = "DB_OUHAI".EncryptDES(SecurityKey),Desciption = "",SysDbaseConfigHead = OhErpHead},
        };
        OhErpHead.SysDbaseConfigDetail = OhErpDetails;
        _context.SysDbaseConfigHeads.Add(OhErpHead);
        
        var U9CTestHead = new SysDbaseConfigHead
        {
            ServerAddr = "192.168.1.22",
            Port = 1433,
            UserID = "sa",
            UserPwd = "OUHAI1234".EncryptDES(SecurityKey),
            RepositoryType = DataCategory.MsSql,
            EnvCategory = EnvironCategory.Test
        };
        var U9CTestDetails = new List<SysDbaseConfigDetail>()
        {
            new(){DataRepository = "0509wdk".EncryptDES(SecurityKey), Desciption = "", SysDbaseConfigHead = U9CTestHead},
            new(){DataRepository = "U9testC".EncryptDES(SecurityKey), Desciption = "", SysDbaseConfigHead = U9CTestHead},
            new(){DataRepository = "U9test0121".EncryptDES(SecurityKey), Desciption="*", SysDbaseConfigHead = U9CTestHead}
        };
        U9CTestHead.SysDbaseConfigDetail = U9CTestDetails;
        _context.SysDbaseConfigHeads.Add(U9CTestHead);

        var DataETLHead = new SysDbaseConfigHead
        {
            ServerAddr = "192.168.1.50",
            Port = 3306,
            UserID = "root",
            UserPwd = "Efc9679".EncryptDES(SecurityKey),
            RepositoryType = DataCategory.MySql,
            EnvCategory = EnvironCategory.Development
        };

        var DataETLDetails = new List<SysDbaseConfigDetail>()
        {
            new(){DataRepository = "DataETLDb".EncryptDES(SecurityKey), Desciption="*", SysDbaseConfigHead = DataETLHead}
        };
        DataETLHead.SysDbaseConfigDetail = DataETLDetails;
        _context.SysDbaseConfigHeads.Add(DataETLHead);
        
        _context.SaveChanges();

        //return Ok(new
        //{
        //    code = 200,
        //    msg = "OK"
        //});
        return StatusCode(200);
    }

    [HttpGet(Name = "GetDbaseConfig")]
    public string Get(string dataRepository)
    {
        //U9test0121
        var queryDBbaseConfig = (from a in _context.SysDbaseConfigHeads.ToList()
                                 join b in _context.SysDbaseConfigDetails.ToList() on a.Id equals b.HeadId
                                 where b.DataRepository == dataRepository.EncryptDES(SecurityKey)
                                 select new SqlConnectionStringBuilder
                                 {
                                     DataSource = a.RepositoryType == DataCategory.MsSql ? a.ServerAddr : string.Concat(a.ServerAddr, ":", a.Port),
                                     InitialCatalog = b.DataRepository.DecryptDES(SecurityKey),
                                     UserID = a.UserID,
                                     Password = a.UserPwd.DecryptDES(SecurityKey),
                                     MultipleActiveResultSets = true
                                 }).First();

        return queryDBbaseConfig.ConnectionString;
    }
}
