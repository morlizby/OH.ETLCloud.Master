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
    //分为2份接口文档
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "瓯海眼镜DataETL Api",
        Version = "v1",
        Contact = new OpenApiContact()
        {
            Name = "morliz",
            Email = "morliz@live.cn",
            Url = null
        },
        Description = "瓯海眼镜ETL解决方案"
    });

    ////配置注释文档
    //var basePath = AppContext.BaseDirectory;
    //var xmlPath = Path.Combine(basePath, "Mes.DataAcquire.WebApi.xml");
    ////接口注释信息
    //option.IncludeXmlComments(xmlPath, true);
    ////Model注释信息
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
    //选择对应的文档
    option.SwaggerEndpoint("/swagger/v1/swagger.json", "瓯海眼镜DataETL Api");
    option.RoutePrefix = "";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
