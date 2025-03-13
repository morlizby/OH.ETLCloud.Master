namespace OH.ETL.Core.Const;

public static class U9CApi
{
    #region OAuth2授权
    //根据userCode获取盐值
    public const string GetSalt = "/OAuth2/GetSalt";

    //获取授权码
    public const string GetAuthorizeCode = "/OAuth2/GetAuthorizeCode";

    //登录获取token
    public const string Login = "/OAuth2/Login";

    //登录获取token
    public const string AuthLogin = "/OAuth2/AuthLogin";

    //刷新token
    public const string RefreshToken = "/OAuth2/RefreshToken";

    //销毁token
    public const string DestroyToken = "/OAuth2/DestroyToken";

    //单点登录获取授权码
    public const string SSOLogin = "/OAuth2/SSOLogin";

    #endregion


    #region PersonInfoDoc人员信息
    //获取人员信息
    public const string GetPersonInfoDoc = $"/PersonInfoDoc/GetPersonInfoDoc";


    //创建人员信息
    public const string CreatePersonInfoDoc = "/PersonInfoDoc/CreatePersonInfoDoc";

    //创建员工银行账号
    public const string CreateEmployeeBankAccount = "/PersonInfoDoc/CreateEmployeeBankAccount";


    #endregion
}
