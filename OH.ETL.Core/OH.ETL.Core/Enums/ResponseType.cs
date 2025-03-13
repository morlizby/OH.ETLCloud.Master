﻿namespace OH.ETL.Core.Enums;

public enum ResponseType
{
    ServerError = 1,
    LoginExpiration = 302,
    ParametersLack = 303,
    TokenExpiration,
    PINError,
    NoPermissions,
    NoRolePermissions,
    LoginError,
    AccountLocked,
    LoginSuccess,
    SaveSuccess,
    AuditSuccess,
    OperSuccess,
    RegisterSuccess,
    ModifyPwdSuccess,
    EidtSuccess,
    DelSuccess,
    NoKey,
    NoKeyDel,
    KeyError,
    Other
}
