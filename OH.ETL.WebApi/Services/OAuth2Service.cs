using OH.ETL.Core.Configuration;
using OH.ETL.Core.Const;
using OH.ETL.Core.Extensions;
using OH.ETL.Core.Utils;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OH.ETL.WebApi.Services;

/// <summary>
/// OAuth2授权服务
/// </summary>
public class OAuth2Service : IOAuth2Service
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OAuth2Service> _logger;

    public OAuth2Service(IHttpClientFactory httpClientFactory,
        ILogger<OAuth2Service> logger)
    {
        _httpClient = httpClientFactory.CreateClient();
        _logger = logger;
    }

    /// <summary>
    /// 检查字符串中是否有Html标签
    /// </summary>
    /// <param name="html">Html源码</param>
    /// <returns>存在为True</returns>
    public static bool CheckHtml(string html)
    {
        string filter = "<[\\s\\S]*?>";
        if (Regex.IsMatch(html, filter))
        {
            return true;
        }

        filter = "[<>][\\s\\S]*?";

        if (Regex.IsMatch(html, filter))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 返回过滤掉所有的Html标签后的字符串
    /// </summary>
    /// <param name="html">Html源码</param>
    /// <returns>过滤Html标签后的字符串</returns>
    public static string ClearAllHtml(string html)
    {
        string filter = "<[\\s\\S]*?>";
        if (Regex.IsMatch(html, filter))
        {
            html = Regex.Replace(html, filter, "");
        }

        filter = "[<>][\\s\\S]*?";
        if (Regex.IsMatch(html, filter))
        {
            html = Regex.Replace(html, filter, "");
        }

        return html;
    }

    /// <summary>
    /// 获取授权码
    /// </summary>
    /// <returns></returns>
    public async Task<object> GetAuthorizeCode()
    {
        var OAuth = AppSetting._authentication;
        var reqUrl = $"{AppSetting.ApiUrlPrefix}{U9CApi.GetAuthorizeCode}";

        var resDict = OAuth.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(s => s.Name.StartsWith("Client"))
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(OAuth, null)?.ToString());
        var reqParams = string.Join("&", resDict.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

        var response = await _httpClient.GetAsync($"{reqUrl}?{reqParams}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var ResContent = content.DeserializeObject<WebResponseContent>();
            return ResContent.Data;
        }

        return "";
    }

    /// <summary>
    /// 登录获取token
    /// </summary>
    /// <returns></returns>
    public async Task<object> GetLoginToken()
    {
        var OAuth = AppSetting._authentication;
        var reqUrl = $"{AppSetting.ApiUrlPrefix}{U9CApi.Login}";

        var resDict = OAuth.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(s => !s.Name.StartsWith("Client"))
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(OAuth, null)?.ToString());
        resDict.Add("Code", (await GetAuthorizeCode()).ToString());

        var reqParams = string.Join("&", resDict.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

        var response = await _httpClient.GetAsync($"{reqUrl}?{reqParams}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var ResContent = content.DeserializeObject<WebResponseContent>();
            return ResContent.Data;
        }

        return "";
    }

    /// <summary>
    /// 获取Token授权令牌
    /// </summary>
    /// <returns></returns>
    public async Task<object> GetAccessToken()
    {
        var OAuth = AppSetting._authentication;
        var reqUrl = $"{AppSetting.ApiUrlPrefix}{U9CApi.AuthLogin}";

        var resDict = OAuth.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToDictionary(prop => prop.Name, prop => prop.GetValue(OAuth, null)?.ToString());

        var reqParams = string.Join("&", resDict.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

        var response = await _httpClient.GetAsync($"{reqUrl}?{reqParams}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            //判断内容是否为XML格式
            if (CheckHtml(content))
                return ClearAllHtml(content);

            var ResContent = content.DeserializeObject<WebResponseContent>();
            return ResContent.Data;
        }

        return "";
    }

    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    public async Task<object> GetRefreshToken(object accessToken)
    {
        //var OAuth = AppSetting._authentication;
        var reqUrl = $"{AppSetting.ApiUrlPrefix}{U9CApi.RefreshToken}";
        var reqParams = $"token={accessToken}";

        var response = await _httpClient.GetAsync($"{reqUrl}?{reqParams}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var ResContent = content.DeserializeObject<WebResponseContent>();
            return ResContent.Data;
        }

        return "";
    }

    /// <summary>
    /// 销毁Token
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    public async Task<bool> GetDestroyToken(string accessToken)
    {
        //var OAuth = AppSetting._authentication;
        var reqUrl = $"{AppSetting.ApiUrlPrefix}{U9CApi.DestroyToken}";
        var reqParams = $"token={accessToken}";

        var response = await _httpClient.GetAsync($"{reqUrl}?{reqParams}");
        return response.IsSuccessStatusCode;
    }
}

/// <summary>
/// OAuth2授权服务接口
/// </summary>
public interface IOAuth2Service
{

    /// <summary>
    /// 获取授权码
    /// </summary>
    /// <returns></returns>
    Task<object> GetAuthorizeCode();

    /// <summary>
    /// 登录获取token
    /// </summary>
    /// <returns></returns>
    Task<object> GetLoginToken();

    /// <summary>
    /// 获取Token授权令牌
    /// </summary>
    /// <returns></returns>
    Task<object> GetAccessToken();

    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<object> GetRefreshToken(object accessToken);

    /// <summary>
    /// 销毁Token
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Task<bool> GetDestroyToken(string accessToken);
}
