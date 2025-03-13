using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OH.ETL.Entities.Enums;

namespace OH.ETL.Entities.DomainModels;

/// <summary>
/// 数据仓库配置主表
/// </summary>
public partial class SysDbConfigHead
{
    public SysDbConfigHead()
    {
        SysDbConfigDetail = new HashSet<SysDbConfigDetail>();
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    /// <summary>
    /// 服务器地址
    /// </summary>
    public string ServerAddr { get; set; }
    /// <summary>
    /// 端口号
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 登录账号
    /// </summary>
    public string UserID { get; set; }
    /// <summary>
    /// 密钥
    /// </summary>
    public string UserPwd { get; set; }

    /// <summary>
    /// 存储库类型
    /// </summary>
    public DataCategory RepositoryType { get; set; }

    /// <summary>
    /// 环境标识
    /// </summary>
    public EnvironCategory EnvCategory { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateDate { get; set; }

    public virtual ICollection<SysDbConfigDetail> SysDbConfigDetail { get; set; }
}
