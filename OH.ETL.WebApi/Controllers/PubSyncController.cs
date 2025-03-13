using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using OH.ETL.Core.EFDbContext;
using OH.ETL.Core.Extensions;
using OH.ETL.WebApi.DtoModels;

namespace OH.ETL.WebApi.Controllers;

/// <summary>
/// HeKeERP 数据采集Api
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class PubSyncController : ControllerBase
{
    private readonly ILogger<PubSyncController> _logger;
    private readonly OhErpContext _erpContext;
    private readonly OhErpU9cContext _erpu9cContext;
    private readonly U9ErpContext _u9ErpContext;

    public PubSyncController(ILogger<PubSyncController> logger,
        OhErpContext erpContext, OhErpU9cContext erpu9cContext, U9ErpContext u9ErpContext)
    {
        _logger = logger;
        _erpContext = erpContext;
        _erpu9cContext = erpu9cContext;
        _u9ErpContext = u9ErpContext;
    }

    /// <summary>
    /// 获取岗位所对应U9C的业务类型
    /// </summary>
    /// <param name="position">岗位名称</param>
    /// <returns></returns>
    private int GetOperatorType(string position)
    {
        string pattern = "(^[0-9]+$)|(^-[0-9]+$)|(^[0-9]+.[0-9]+$)|(^-[0-9]+.[0-9]+$)";
        var operatorType = _erpContext.HrJbzlInfos.AsEnumerable()
            .FirstOrDefault(x => x.Type == "岗位" && !string.IsNullOrEmpty(x.Cdefine2)
            && Regex.IsMatch(x.Cdefine2, pattern) && x.Memo.Contains(position))
            .Cdefine2;

        return !string.IsNullOrEmpty(operatorType) ? int.Parse(operatorType) : 7;
    }

    /// <summary>
    /// 获取人员同步资料
    /// </summary>
    /// <param name="userId">按人员工号</param>
    /// <returns></returns>
    [HttpGet]
    public List<DtoEmployee> GetOldErpEmployees(string userId)
    {
        /*
        var iResult = GetOperatorType(userId);
        var queryU9CDeptInfo = _erpu9cContext.BranchMappers.AsEnumerable().ToList();
        var queryPerson = (from a in _u9ErpContext.CboPersons.AsEnumerable()
                           join b in _u9ErpContext.CboPersonTrls.AsEnumerable() on a.Id equals b.Id
                           where a.PersonId == userId
                           select a).ToList();
        */


        int dtoId = 1;     //Id标识
        var querySrcPerson = (from a in _erpContext.Employees.AsEnumerable()
                              join b in _erpContext.Branches.AsEnumerable() on a.Branchid equals b.Id
                              join c in _erpu9cContext.BranchMappers.AsEnumerable() on a.Branchid equals c.Id
                              where !string.IsNullOrEmpty(a.Department) && a.UserId == userId
                              select new DtoEmployee
                              {
                                  Id = dtoId++,
                                  OrgCode = c.OrgCode,
                                  UserId = a.UserId,
                                  UserName = a.Name,
                                  BranchId = a.Branchid,
                                  BranchName = b.Name,
                                  Sex = a.Sex,
                                  Marry = a.Marry,
                                  Birthday = a.Birthday,
                                  Speciality = a.Speciality,
                                  Mz = a.Mz,

                                  Department = a.Department,
                                  Duty = a.Duty,
                                  Job = a.Job,
                                  Position = a.Position,

                                  Lztype = a.Lztype,
                                  LzDate = a.LzDate,
                                  Lzmemo = a.Lzmemo,

                                  CardId = a.Cardid,
                                  MovePhone = a.MovePhone,

                                  Ygtype = a.Ygtype,
                                  Fxtype = a.Fxtype,
                                  Lastupdate = a.Lastupdate,
                                  Lastuser = a.Lastuser
                              }).ToList();

        return querySrcPerson;
    }

    /// <summary>
    /// U9C人员档案信息同步
    /// </summary>
    /// <param name="srcEmployee">OldERP人员档案信息</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> BrwPersonDataAsync([FromBody] DtoEmployee srcEmployee)
    {

        if (srcEmployee == null)
        {
            _logger.LogError($"获取OldERP人员档案信息为空，同步失败！");
            return new NotFoundResult();
        }

        DateTime DataOn = DateTime.Now;
        string DataBy = "admin";

        var OrgID = _u9ErpContext.BaseOrganizations.First(x => x.Code == srcEmployee.OrgCode).Id;

        //当人员为离职时
        if (srcEmployee.Department == "离职人员")
        {
            //用户账号存在时
            if (_u9ErpContext.BaseUsers.AsEnumerable().Any(x => x.Code == srcEmployee.UserId))
            {
                _logger.LogInformation($"工号[{srcEmployee.UserId}] 账号存在则更新为停用");
                var queryUser = _u9ErpContext.BaseUsers.AsEnumerable().First(x => x.Code == srcEmployee.UserId);
                queryUser.ModifiedOn = DataOn;
                queryUser.ModifiedBy = DataBy;
                queryUser.IsAlive = false;
                queryUser.EffectiveIsEffective = false;
                queryUser.SysVersion++;

                await _u9ErpContext.SaveChangesAsync();
            }

            //人员基本信息不存在时
            if (!_u9ErpContext.CboPersons.AsEnumerable().Any(x => x.AttachOrg == OrgID && x.PersonId == srcEmployee.UserId))
            {
                _logger.LogInformation($"工号[{srcEmployee.UserId}] 跳过");

                return new NotFoundResult();
            }
        }



        return Ok(new { message = "操作成功" });
    }
    /*
    /// <summary>
    /// 新增供应商
    /// </summary>
    /// <param name="supplierDtos">供应商集合</param>
    /// <returns></returns>
    [HttpPost(Name = "AddPerson")]
    public ApiResult<List<ResultDTORData>> Add([FromBody] List<ResultDTORData> supplierDtos)
    {
        ApiResult<List<ResultDTORData>> apiResult = new();

        using (BPForEngine bp = new())
        {
            ContextDTO cDto = new()
            {
                UserClientID = "ohWebApi",      //应用Id
                //UserPwd = "",

                EntCode = "005",    //企业编码
                OrgCode = "1010",   //组织编码
                UserCode = "YY08",  //用户账号
            };
            //重写上下文
            cDto.WriteToContext();

            BatchQuerySupplierByDTOSRV proxy = new()
            {
                QuerySupplierDTOs = new List<QuerySupplierDTO>()
            };

            QuerySupplierDTO queryDto = new()
            {
                Supplier = new CommonArchiveDataDTO(),
                Org = new CommonArchiveDataDTO(),
            };
            //queryDto.Supplier.Code = head[0].DocNO;
            //queryDto.Org.Code = UFIDA.U9.Base.Context;

            proxy.QuerySupplierDTOs.Add(queryDto);
        }

        return apiResult;
    }


    [HttpGet]
    public ApiResult<List<ContactInfoDto>> GetContractInfo([FromBody] List<ContactInfoDto> contactInfos)
    {

        //using(UBFTransactionScope scope =new UBFTransactionScope())
        //{

        //}


        using (BPForEngine bp = new())
        {
            ContextDTO cDto = new()
            {
                UserClientID = "ohWebApi",      //应用Id
                //UserPwd = "",

                EntCode = "005",    //企业编码
                OrgCode = "1010",   //组织编码
                UserCode = "YY08",  //用户账号
            };
            //重写上下文
            cDto.WriteToContext();

        }

        try
        {
            ApiResult<List<ContactInfoDto>> apiResult = new();
            return apiResult;
        }
        catch (Exception ex)
        {

        }
    }
    */
}
