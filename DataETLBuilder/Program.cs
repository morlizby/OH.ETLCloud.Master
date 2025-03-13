using DataETLBuilder.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SysDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DbConnectionString"));
});

builder.Services.AddControllers()
              .AddNewtonsoftJson(op =>
              {
                  op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                  op.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
              });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    //��Ϊ2�ݽӿ��ĵ�
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "걺��۾�DataETL Api",
        Version = "v1",
        Contact = new OpenApiContact()
        {
            Name = "morliz",
            Email = "morliz@live.cn",
            Url = null
        },
        Description = "걺��۾�ETL�������"
    });

    ////����ע���ĵ�
    //var basePath = AppContext.BaseDirectory;
    //var xmlPath = Path.Combine(basePath, "Mes.DataAcquire.WebApi.xml");
    ////�ӿ�ע����Ϣ
    //option.IncludeXmlComments(xmlPath, true);
    ////Modelע����Ϣ
    //option.IncludeXmlComments(Path.Combine(basePath, "Mes.DataAcquire.WebApi.xml"), true);
});

//builder.Services.AddSignalR();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    /*
    app.Use(async (context, next) =>
    {
        foreach (var header in context.Request.Headers)
        {
            Console.WriteLine($"Header: {header.Key} = {header.Value}");
        }
        Console.WriteLine("======================================================");
        await next();
    });
    */
}
else
{
    app.UseExceptionHandler("/Error");
    //Adds the Strict-Transport-Security header.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(option =>
{
    //ѡ���Ӧ���ĵ�
    option.SwaggerEndpoint("/swagger/v1/swagger.json", "걺��۾�DataETL Api");
    option.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
