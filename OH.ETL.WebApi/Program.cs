using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OH.ETL.Core.EFDbContext;
using OH.ETL.Core.Extensions;
using OH.ETL.Core.Extensions.AutofacManager;
using OH.ETL.Core.Quartz;
using OH.ETL.WebApi.Services;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        builder.Services.AddModule(containerBuilder, builder.Configuration);
    });
/*
builder.Services.AddDbContext<SysDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DbConnectionString"));
});
*/
builder.Services.AddControllers()
              .AddNewtonsoftJson(op =>
              {
                  op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                  op.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
              });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//AddQuartz and AddQuartzHostedService IOC.
builder.Services.AddTransient<HttpResultfulJob>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddSingleton<IJobFactory, IOCJobFactory>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{

    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Ouhai ERP Api",
        Version = "v1",
        Contact = new OpenApiContact()
        {
            Name = "morliz",
            Email = "morliz@live.cn",
            Url = null
        },
        Description = "瓯海ERP数据解决方案"
    });

    //配置注释文档
    var basePath = AppContext.BaseDirectory;
    var currentProjectName = string.Concat(Assembly.GetEntryAssembly().GetName().Name, ".xml");
    var xmlPath = Path.Combine(basePath, currentProjectName);
    //接口注释信息
    option.IncludeXmlComments(xmlPath, true);
    //Model注释信息
    option.IncludeXmlComments(Path.Combine(basePath, currentProjectName), true);
});

/*
 * AddSingleton单例注册
 * 使用单一实例生存期注册的服务会在应用启动时创建一次，并在应用的生存期内重复使用。这种生存期对于创建成本高昂或不会经常更改的服务非常有用。
 *  例如，如果一个服务可以从文件中读取配置设置，则可以注册为单一实例。
 * 
 * AddScoped作用域注册
 * 使用范围性生存期注册的服务会在配置的每个范围内创建一次，ASP.NET Core 会为每个请求设置该范围。 ASP.NET Core 中的范围性服务通常在收到
 * 请求时创建，并在请求完成时处理掉。 如果服务需要访问特定于请求的数据，这种生存期会非常有用。 
 * 例如，从数据库中提取客户数据的服务可以注册为范围性服务。
 *
 * AddTransient暂时性注册
 * 使用暂时性生存期注册的服务会在每次请求时创建。 这种生存期适用于轻型无状态服务。 例如，执行专用计算的服务可以注册为暂时性服务。
 *
*/
//builder.Services.AddSingleton<IWelcomeService, WelcomeService>();
builder.Services.AddScoped<IOAuth2Service, OAuth2Service>();

//builder.Services.AddSignalR();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {

        option.SwaggerEndpoint("/swagger/v1/swagger.json", "Ouhai ERP Api");
        option.RoutePrefix = "";
    });
}
else
{
    app.UseExceptionHandler("/Error");
    //Adds the Strict-Transport-Security header.
    app.UseHsts();
}
/*
app.Use(async (context, next) =>
{
    var _context = context.GetService<SysDbContext>();
    _context.Database.EnsureDeleted();
    _context.Database.EnsureCreated();

    foreach (var header in context.Request.Headers)
    {
        Console.WriteLine($"Header: {header.Key} = {header.Value}");
    }
    Console.WriteLine("======================================================");
    await next();
});
*/

//app.UseRouting();
//app.UseCors();
app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();