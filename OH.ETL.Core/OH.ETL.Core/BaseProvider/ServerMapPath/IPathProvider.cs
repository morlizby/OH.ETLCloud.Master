using Microsoft.AspNetCore.Hosting;
using OH.ETL.Core.Extensions.AutofacManager;

namespace OH.ETL.Core.BaseProvider.ServerMapPath;

interface IPathProvider : IDependency
{
    string MapPath(string path);
    string MapPath(string path, bool rootPath);
    IWebHostEnvironment GetHostingEnvironment();
}
