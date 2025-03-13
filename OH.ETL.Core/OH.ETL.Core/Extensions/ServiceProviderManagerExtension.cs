using OH.ETL.Core.Utils;

namespace OH.ETL.Core.Extensions;

public static class ServiceProviderManagerExtension
{
    public static object GetService(this Type serviceType)
    {
        // HttpContext.Current.RequestServices.GetRequiredService<T>(serviceType);
        return HttpContext.Current.RequestServices.GetService(serviceType);
    }

}
