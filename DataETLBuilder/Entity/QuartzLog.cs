using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataETLBuilder.Entity;

[Comment("任务日志")]
public partial class QuartzLog
{
    [Key]
    public Guid LogId { get; set; }

    /// <summary>
    ///任务Id
    /// </summary>
    public Guid TaskId { get; set; }

    /// <summary>
    ///任务名称
    /// </summary>
    public string TaskName { get; set; } = null!;

    /// <summary>
    /// 耗时(秒)
    /// </summary>
    public int? ElapsedTime { get; set; }

    /// <summary>
    ///开始时间
    /// </summary>
    public DateTime? StratDate { get; set; }

    /// <summary>
    ///结束时间
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    ///是否成功
    /// </summary>
    public int? Result { get; set; }

    /// <summary>
    ///返回内容
    /// </summary>
    public string? ResponseContent { get; set; }

    /// <summary>
    ///异常信息
    /// </summary>
    public string? ErrorMsg { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateDate { get; set; }

    public virtual QuartzOption QuartzOption { get; set; } = null!;
}
