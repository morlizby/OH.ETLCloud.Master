using OH.ETL.Core.Enums;

namespace OH.ETL.Core.Extensions;

public static class ResponseMsg
{
    public static string GetMsg(this ResponseType responseType, string msg)
    {
        string Msg = "";
        switch (responseType)
        {
            case ResponseType.Unauthorized:
                Msg = "未授权"; break;
            case ResponseType.TokenExpiration:
                Msg = "Token已过期"; break;
            case ResponseType.RequestHasExpired:
                Msg = "请求已过期"; break;
            case ResponseType.RoutingError:
                Msg = "路由错误"; break;
            case ResponseType.AuthorizedCodeHasExpired:
                Msg = "授权码已过期"; break;
            case ResponseType.UserNameDoesNotExist:
                Msg = "用户名不存在"; break;
            case ResponseType.ClientIDNotEnabled:
                Msg = "客户端ID未启用"; break;
            case ResponseType.OrgCodeDoesNotExist:
                Msg = "组织编码不存在"; break;
            case ResponseType.ServerError:
                Msg = $"服务器内部问题,{msg}"; break;
            case ResponseType.SignatureVerificationFailure:
                Msg = "签名验证失败"; break;
            case ResponseType.ParametersLack:
                Msg = "参数错误或不完整"; break;
            case ResponseType.LoginFailure:
                Msg = "登录失败"; break;
            case ResponseType.TokenInvalidation:
                Msg = "Token已失效"; break;
            case ResponseType.ClientIPUnauthorized:
                Msg = "客户端IP未授权"; break;
            case ResponseType.Other:
                Msg = msg; break;
            default:
                Msg = responseType.ToString();
                break;
        }
        return Msg;
    }

}
