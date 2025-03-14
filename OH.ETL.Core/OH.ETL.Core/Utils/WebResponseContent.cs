using OH.ETL.Core.Enums;
using OH.ETL.Core.Extensions;

namespace OH.ETL.Core.Utils;

public class WebResponseContent
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


    public WebResponseContent()
    {
    }
    public WebResponseContent(bool success)
    {
        this.Success = success;
    }

    public WebResponseContent OK()
    {
        this.Success = true;
        return this;
    }

    public static WebResponseContent Instance
    {
        get { return new WebResponseContent(); }
    }
    public WebResponseContent OK(string resMsg = null, object data = null)
    {
        this.Success = true;
        this.ResMsg = resMsg;
        Data = data;
        return this;
    }
    public WebResponseContent OK(ResponseType responseType)
    {
        return Set(responseType, true);
    }
    public WebResponseContent Error(string resMsg = null)
    {
        this.Success = false;
        this.ResMsg = resMsg;
        return this;
    }
    public WebResponseContent Error(ResponseType responseType)
    {
        return Set(responseType, false);
    }
    public WebResponseContent Set(ResponseType responseType)
    {
        bool? b = null;
        return Set(responseType, b);
    }
    public WebResponseContent Set(ResponseType responseType, bool? status)
    {
        return Set(responseType, null, status);
    }
    public WebResponseContent Set(ResponseType responseType, string msg)
    {
        bool? b = null;
        return Set(responseType, msg, b);
    }
    public WebResponseContent Set(ResponseType responseType, string resMsg, bool? success)
    {
        if (success != null)
        {
            this.Success = (bool)success;
        }
        ResCode = (int)responseType;
        if (!string.IsNullOrEmpty(resMsg))
        {
            this.ResMsg = resMsg;
            return this;
        }
        this.ResMsg = responseType.GetMsg(resMsg);
        return this;
    }
}
