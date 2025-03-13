using Microsoft.EntityFrameworkCore;
using Quartz.Impl.Triggers;
using Quartz.Impl;
using Quartz;
using OH.ETL.Core.EFDbContext;
using OH.ETL.Entities.DomainModels;

namespace OH.ETL.Core.Quartz;

public class HttpResultfulJob : IJob
{
    readonly IHttpClientFactory _httpClientFactory;

    readonly IServiceProvider _serviceProvider;

    public HttpResultfulJob(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        DateTime dateTime = DateTime.Now;
        QuartzOption taskOptions = context.GetTaskOptions();
        string httpMessage = "";
        AbstractTrigger trigger = (context as JobExecutionContextImpl).Trigger as AbstractTrigger;
        if (taskOptions == null)
        {
            Console.WriteLine($"未获取到作业");
            return;
        }

        if (string.IsNullOrEmpty(taskOptions.ApiUrl) || taskOptions.ApiUrl == "/")
        {
            Console.WriteLine($"未配置作业:{taskOptions.TaskName}的url地址");
            QuartzFileHelper.Error($"未配置作业:{taskOptions.TaskName}的url地址");
            return;
        }

        string exceptionMsg = null;

        try
        {
            using var dbContext = new SysDbContext();
            var _taskOptions = dbContext.Set<QuartzOption>().AsTracking()
                  .Where(x => x.TaskId == taskOptions.TaskId).FirstOrDefault();

            if (_taskOptions != null)
            {
                dbContext.Update(_taskOptions);
                var entry = dbContext.Entry(_taskOptions);
                entry.State = EntityState.Unchanged;
                entry.Property("LastRunTime").IsModified = true;
                _taskOptions.LastRunTime = DateTime.Now;
                dbContext.SaveChanges();
            }

            Dictionary<string, string> header = new();
            if (!string.IsNullOrEmpty(taskOptions.AuthKey)
                && !string.IsNullOrEmpty(taskOptions.AuthValue))
            {
                header.Add(taskOptions.AuthKey.Trim(), taskOptions.AuthValue.Trim());
            }

            httpMessage = await _httpClientFactory.SendAsync(
                    taskOptions.RequestMode?.ToLower() == "get" ? HttpMethod.Get : HttpMethod.Post,
                    taskOptions.ApiUrl,
                    taskOptions.PostData,
                    taskOptions.TimeOut ?? 180,
                    header); ;
        }
        catch (Exception ex)
        {
            exceptionMsg = ex.Message + ex.StackTrace;
        }
        finally
        {
            try
            {
                var log = new QuartzLog
                {
                    LogId = Guid.NewGuid(),
                    TaskName = taskOptions.TaskName,
                    TaskId = taskOptions.TaskId,
                    CreateDate = dateTime,
                    ElapsedTime = Convert.ToInt32((DateTime.Now - dateTime).TotalSeconds),
                    ResponseContent = httpMessage,
                    ErrorMsg = exceptionMsg,
                    StratDate = dateTime,
                    Result = exceptionMsg == null ? 1 : 0,
                    EndDate = DateTime.Now
                };

                using var dbContext = new SysDbContext();
                dbContext.Set<QuartzLog>().Add(log);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"日志写入异常:{taskOptions.TaskName},{ex.Message}");
                QuartzFileHelper.Error($"日志写入异常:{typeof(HttpResultfulJob).Name},{taskOptions.TaskName},{ex.Message}");
            }
        }

        Console.WriteLine(trigger.FullName + " " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:sss") + " " + httpMessage);
        return;
    }


}
