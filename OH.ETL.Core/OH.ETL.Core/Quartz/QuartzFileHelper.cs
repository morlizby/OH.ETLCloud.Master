using System.Reflection;
using OH.ETL.Core.Extensions;
using OH.ETL.Core.Utils;

namespace OH.ETL.Core.Quartz;

public static class QuartzFileHelper
{
    public static void OK(string message)
    {
        Write(message, "log");
    }

    public static void Error(string message)
    {
        Write(message, "error");
    }

    private static void Write(string message, string folder)
    {
        try
        {
            string location = Assembly.GetExecutingAssembly().Location;

            string fileName = DateTime.Now.ToString("yyyy-MM-dd");
            string path = $"{Path.GetDirectoryName(location)}\\quartz\\{folder}\\".ReplacePath();
            FileHelper.WriteFile(path, $"{fileName}.txt", message, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"文件写入异常{message},{ex.Message + ex.StackTrace}");
        }
    }
}
