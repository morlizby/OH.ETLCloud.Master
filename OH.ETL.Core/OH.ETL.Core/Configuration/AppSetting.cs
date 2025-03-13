using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OH.ETL.Core.Const;

namespace OH.ETL.Core.Configuration;

public static class AppSetting
{
    private static Connection _connection = null!;

    public static Authentication _authentication = null!;

    public static string DbConnectionString
    {
        get { return _connection.DbConnectionString; }
    }

    public static string U9ErpConnectionString
    {
        get { return _connection.U9ErpConnectionString; }
    }

    public static string OhErpDbConnectionString
    {
        get { return _connection.OhErpDbConnectionString; }
    }

    public static string OhErpU9cDbConnectionString
    {
        get { return _connection.OhErpU9cDbConnectionString; }
    }

    public static IConfiguration Configuration { get; private set; } = null!;

    public static Secret Secret { get; private set; } = null!;
    public static string ApiUrlPrefix { get; private set; } = null!;
    public static string TokenHeaderName = "Authorization";
    //public static string CurrentPath { get; private set; } = null!;

    public static void Init(IServiceCollection services, IConfiguration configuration)
    {
        Configuration = configuration;

        services.Configure<Connection>(configuration.GetSection("Connection"));
        services.Configure<Authentication>(configuration.GetSection("Authentication"));
        services.Configure<Secret>(configuration.GetSection("Secret"));
        ApiUrlPrefix = configuration["ApiUrlPrefix"];

        var provider = services.BuildServiceProvider();
        _connection = provider.GetRequiredService<IOptions<Connection>>().Value;
        _authentication = provider.GetRequiredService<IOptions<Authentication>>().Value;
        Secret = provider.GetRequiredService<IOptions<Secret>>().Value;

        DBType.Name = _connection.DBType;
        if (string.IsNullOrEmpty(_connection.DbConnectionString))
            throw new Exception("未配置好内置数据库默认连接");

        if (string.IsNullOrEmpty(_connection.U9ErpConnectionString))
            throw new Exception("未配置好U9C数据库默认连接");

        if (string.IsNullOrEmpty(_connection.OhErpDbConnectionString))
            throw new Exception("未配置好HeKeERP数据库默认连接");

        if (string.IsNullOrEmpty(_connection.OhErpU9cDbConnectionString))
            throw new Exception("未配置好HekeERPU9C数据库默认连接");

        /*
        try
        {
            _connection.DbConnectionString = _connection.DbConnectionString.DecryptDES(Secret.DB);
            _connection.MesImsDbConnectionString = _connection.MesImsDbConnectionString.DecryptDES(Secret.DB);
        }
        catch { }
        */
    }

    public static string GetSettingString(string key)
    {
        return Configuration[key];
    }

    public static IConfigurationSection GetSection(string key)
    {
        return Configuration.GetSection(key);
    }
}


public class Connection
{
    public string DBType { get; set; } = null!;
    public string DbConnectionString { get; set; } = null!;
    public string U9ErpConnectionString { get; set; } = null!;
    public string OhErpDbConnectionString { get; set; } = null!;
    public string OhErpU9cDbConnectionString { get; set; } = null!;
}

/// <summary>
/// U9C鉴权
/// </summary>
public class Authentication
{
    #region OAuth2鉴权
    /// <summary>
    /// 应用Id
    /// </summary>
    public string ClientId { get; set; } = null!;

    /// <summary>
    /// 应用秘钥
    /// </summary>
    public string ClientSecret { get; set; } = null!;

    /// <summary>
    /// 企业编码
    /// </summary>
    public string? EntCode { get; set; } = null!;

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserCode { get; set; } = null!;

    /// <summary>
    /// 组织编码
    /// </summary>
    public string OrgCode { get; set; } = null!;

    #endregion
}