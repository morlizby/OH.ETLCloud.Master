using DataETLBuilder.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataETLBuilder.Entity;

[Comment("数据仓库配置表")]
public partial class SysDbaseConfigHead
{
    public SysDbaseConfigHead()
    {
        SysDbaseConfigDetail = new HashSet<SysDbaseConfigDetail>();
    }

    [Key]
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
    /// 创建时间
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// 存储库类型
    /// </summary>
    public DataCategory RepositoryType { get; set; }

    /// <summary>
    /// 环境标识
    /// </summary>
    public EnvironCategory EnvCategory { get; set; }

    public virtual ICollection<SysDbaseConfigDetail> SysDbaseConfigDetail { get; set; }
}
