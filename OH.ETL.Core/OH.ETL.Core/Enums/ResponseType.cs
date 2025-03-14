namespace OH.ETL.Core.Enums;

public enum ResponseType
{
    //ActionSuccess = 0,
    Unauthorized = 401,
    TokenExpiration = 402,
    RequestHasExpired = 403,
    RoutingError = 404,
    AuthorizedCodeHasExpired = 405,
    UserNameDoesNotExist = 406,
    ClientIDNotEnabled = 407,
    OrgCodeDoesNotExist = 408,
    ServerError = 500,
    SignatureVerificationFailure = 501,
    ParametersLack = 502,
    LoginFailure = 503,
    TokenInvalidation = 504,
    ClientIPUnauthorized = 601,

    Other = 999
}
