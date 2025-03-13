using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataETLBuilder.Entity;

[Comment("数据仓库配置子表")]
public partial class SysDbaseConfigDetail
{
    [Key]
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

    public virtual SysDbaseConfigHead SysDbaseConfigHead { get; set; } = null!;
}
