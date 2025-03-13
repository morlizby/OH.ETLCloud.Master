using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OH.ETL.Entities.DomainModels;

/// <summary>
/// 数据仓库配置子表
/// </summary>
public partial class SysDbConfigDetail
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DetailId { get; set; }

    public int HeadId { get; set; }

    /// <summary>
    /// 数据仓库
    /// </summary>
    public string DataRepository { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Desciption { get; set; }

    public virtual SysDbConfigHead SysDbConfigHead { get; set; } = null!;
}
