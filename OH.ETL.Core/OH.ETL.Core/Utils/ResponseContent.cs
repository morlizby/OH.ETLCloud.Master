namespace OH.ETL.Core.Utils;

/// <summary>
/// Web请求返回体
/// </summary>
public class ResponseContent
{
    /// <summary>
    /// 返回代码
    /// </summary>
    public int ResCode { get; set; }

    /// <summary>
    /// 执行状态
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 返回消息
    /// </summary>
    public string ResMsg { get; set; }

    /// <summary>
    /// 返回内容(需解析)
    /// </summary>
    public object Data { get; set; }
}
