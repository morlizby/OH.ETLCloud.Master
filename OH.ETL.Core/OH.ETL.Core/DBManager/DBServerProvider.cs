using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using OH.ETL.Core.Utils;
using OH.ETL.Core.Configuration;
using OH.ETL.Core.EFDbContext;
using OH.ETL.Core.Enums;
using OH.ETL.Core.Extensions;

namespace OH.ETL.Core.DBManager;

public class DBServerProvider
{

    private static readonly string DefaultConnName = "default";
    private static readonly string _u9ErpConnString = "U9CDb";
    private static readonly string _ohErpConnString = "HekeERPDb";
    private static readonly string _ohErpU9cConnString = "DB_HekeERPU9C";

    private static Dictionary<string, string> ConnectionPool = new(StringComparer.OrdinalIgnoreCase);

    static DBServerProvider()
    {
        SetConnection(DefaultConnName, AppSetting.DbConnectionString);
        SetConnection(_u9ErpConnString, AppSetting.U9ErpConnectionString);
        SetConnection(_ohErpConnString, AppSetting.OhErpDbConnectionString);
        SetConnection(_ohErpU9cConnString, AppSetting.OhErpU9cDbConnectionString);
    }

    public static void SetConnection(string key, string val)
    {
        ConnectionPool[key] = val;
    }

    public static string GetConnectionString(string key)
    {
        key ??= DefaultConnName;
        if (ConnectionPool.ContainsKey(key))
        {
            return ConnectionPool[key];
        }
        return key;
    }

    /// <summary>
    /// 获取默认数据库连接
    /// </summary>
    /// <returns></returns>
    public static string GetConnectionString()
    {
        return GetConnectionString(DefaultConnName);
    }

    public static IDbConnection GetDbConnection(string connString)
    {
        connString ??= ConnectionPool[DefaultConnName];
        //if (DBType.Name == DbCurrentType.MySql.ToString())
        //{
        //    return new MySqlConnection(connString);
        //}
        //if (DBType.Name == DbCurrentType.PgSql.ToString())
        //{
        //    return new NpgsqlConnection(connString);
        //}
        return new SqlConnection(connString);
    }

    /// <summary>
    /// 扩展dapper 获取MSSQL数据库DbConnection，默认系统获取配置文件的DBType数据库类型，
    /// </summary>
    /// <param name="connString">如果connString为null 执行重载GetDbConnection(string connString = null)</param>
    /// <param name="dbCurrentType">指定连接数据库的类型：MySql/MsSql/PgSql</param>
    /// <returns></returns>
    public static IDbConnection GetDbConnection(string connString, DbCurrentType dbCurrentType = DbCurrentType.Default)
    {
        //默认获取DbConnection
        if (connString.IsNullOrEmpty() || DbCurrentType.Default == dbCurrentType)
        {
            return GetDbConnection(connString);
        }

        if (dbCurrentType == DbCurrentType.MySql)
        {
            return new MySqlConnection(connString);
        }
        //if (dbCurrentType == DbCurrentType.PgSql)
        //{
        //    return new NpgsqlConnection(connString);
        //}
        return new SqlConnection(connString);
    }

    /// <summary>
    /// 获取系统库
    /// </summary>
    public static SysDbContext SysDbContext
    {
        get { return HttpContext.Current.GetService<SysDbContext>(); ; }
    }

    /// <summary>
    /// 获取系统库
    /// </summary>
    public static SysDbContext DbContext
    {
        get { return GetEFDbContext(); }
    }

    /// <summary>
    /// 获取系统库
    /// </summary>
    public static SysDbContext GetEFDbContext()
    {
        return SysDbContext;
    }


    /// <summary>
    /// 获取OldErp业务库
    /// </summary>
    public static OhErpContext OldErpContext
    {
        get { return HttpContext.Current.GetService<OhErpContext>(); ; }
    }

    /// <summary>
    /// 获取U9C业务库
    /// </summary>
    public static U9ErpContext U9ErpDbContext
    {
        get { return HttpContext.Current.GetService<U9ErpContext>(); ; }
    }


    public static string OhErpDbConnectionString
    {
        //netcoredevserver为ConnectionPool字典中的key，如果字典中的key改变了，这里也要改变
        get { return GetDbConnectionString(_ohErpConnString); }
    }

    public static string OhErpU9cDbConnectionString
    {
        get { return GetDbConnectionString(_ohErpU9cConnString); }
    }

    public static string U9CErpDbConnectionString
    {
        get { return GetDbConnectionString(_u9ErpConnString); }
    }

    public static string SysConnectingString
    {
        get { return GetDbConnectionString(DefaultConnName); }
    }

    /// <summary>
    /// key为ConnectionPool初始化的所有数据库连接
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetDbConnectionString(string key)
    {
        if (ConnectionPool.TryGetValue(key, out string connString))
        {
            return connString;
        }
        throw new Exception($"未配置[{key}]的数据库连接");
    }


}
