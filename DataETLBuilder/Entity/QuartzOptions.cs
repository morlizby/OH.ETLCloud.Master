using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataETLBuilder.Entity;

[Comment("定时任务")]
public partial class QuartzOption
{
    public QuartzOption()
    {
        QuartzLog = new HashSet<QuartzLog>();
    }

    [Key]
    public Guid TaskId { get; set; }

    /// <summary>
    /// 任务名称
    /// </summary>
    public string TaskName { get; set; } = null!;

    /// <summary>
    /// 组名
    /// </summary>
    public string GroupName { get; set; } = null!;

    /// <summary>
    /// 任务描述
    /// </summary>
    public string? Describe { get; set; }

    /// <summary>
    /// 任务类型 0：Simple,1: Cron
    /// </summary>
    public string TaskType { get; set; } = null!;

    /// <summary>
    ///  请求方式:POST/GET/PUT/DELETE
    /// </summary>
    public string RequestMode { get; set; } = null!;

    /// <summary>
    /// post参数(可选)
    /// </summary>
    public string? PostData { get; set; }

    /// <summary>
    /// Api接口地址
    /// </summary>
    public string ApiUrl { get; set; } = null!;

    /// <summary>
    /// 身份验证秘钥名
    /// </summary>
    public string? AuthKey { get; set; }

    /// <summary>
    /// 身份验证秘钥值
    /// </summary>
    public string? AuthValue { get; set; }

    /// <summary>
    /// 间隔时间
    /// </summary>
    public TimeSpan RepeatInterval { get; set; }

    /// <summary>
    /// 重复次数:-1无限
    /// </summary>
    public int RepeatCount { get; set; }

    /// <summary>
    /// 触发次数
    /// </summary>
    public int FiredCount { get; set; }

    /// <summary>
    /// Corn表达式(可选)
    /// </summary>
    public string? CronExpression { get; set; }

    /// <summary>
    /// 下次触发时间
    /// </summary>
    public DateTimeOffset? NextFireTime { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTimeOffset BeginTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTimeOffset? EndTime { get; set; }

    /// <summary>
    /// 最后执行时间
    /// </summary>
    public DateTime? LastRunTime { get; set; }

    /// <summary>
    /// 超时时间(秒)
    /// </summary>
    public int? TimeOut { get; set; }

    /// <summary>
    /// 运行状态:0正常, 1暂停, 2停止
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    public double ExcuteTime { get; set; }

    ///// <summary>
    ///// 创建人Id
    ///// </summary>
    //public int? CreateID { get; set; }

    ///// <summary>
    ///// 创建人
    ///// </summary>
    //[StringLength(30)]
    //public string? Creator { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateDate { get; set; }

    public virtual ICollection<QuartzLog> QuartzLog { get; set; }
}
