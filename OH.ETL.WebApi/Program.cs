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
        Description = "걺�ERP���ݽ������"
    });

    //����ע���ĵ�
    var basePath = AppContext.BaseDirectory;
    var currentProjectName = string.Concat(Assembly.GetEntryAssembly().GetName().Name, ".xml");
    var xmlPath = Path.Combine(basePath, currentProjectName);
    //�ӿ�ע����Ϣ
    option.IncludeXmlComments(xmlPath, true);
    //Modelע����Ϣ
    option.IncludeXmlComments(Path.Combine(basePath, currentProjectName), true);
});

/*
 * AddSingleton����ע��
 * ʹ�õ�һʵ��������ע��ķ������Ӧ������ʱ����һ�Σ�����Ӧ�õ����������ظ�ʹ�á����������ڶ��ڴ����ɱ��߰��򲻻ᾭ�����ĵķ���ǳ����á�
 *  ���磬���һ��������Դ��ļ��ж�ȡ�������ã������ע��Ϊ��һʵ����
 * 
 * AddScoped������ע��
 * ʹ�÷�Χ��������ע��ķ���������õ�ÿ����Χ�ڴ���һ�Σ�ASP.NET Core ��Ϊÿ���������ø÷�Χ�� ASP.NET Core �еķ�Χ�Է���ͨ�����յ�
 * ����ʱ�����������������ʱ������� ���������Ҫ�����ض�����������ݣ����������ڻ�ǳ����á� 
 * ���磬�����ݿ�����ȡ�ͻ����ݵķ������ע��Ϊ��Χ�Է���
 *
 * AddTransient��ʱ��ע��
 * ʹ����ʱ��������ע��ķ������ÿ������ʱ������ ����������������������״̬���� ���磬ִ��ר�ü���ķ������ע��Ϊ��ʱ�Է���
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