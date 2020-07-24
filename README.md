# abp vnext的相关模块

- [Ldap 模块](doc/Ldap_module.md)

# Ldap 模块

## 安装Nuget包

```shell
dotnet add package iBestRead.Abp.Ldap
```

## 配置

- 修改`appsettings.json`文件:

```json
"LDAP": {
    "ServerHost": "127.0.0.1",}
    "ServerPort": 636,```
    "UseSSL": true,
    "Credentials": {
        "DomainUserName": "yourUser@yourdomain.com.cn",
        "Password": "yourPassword"
    },
    "SearchBase": "CN=Users,DC=yourDomain,DC=com,DC=cn",
    "DomainName": "yourDomain.com.cn",
    "DomainDistinguishedName": "DC=yourDomain,DC=com,DC=cn"
}
```

- 引用模块

```csharp
[DependsOn(typeof(AbpLdapModule))]
public class LdapTestModule : AbpModule
{
  
}
```

- 注入服务