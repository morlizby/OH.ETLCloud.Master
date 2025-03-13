using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Reflection;
using OH.ETL.Core.Configuration;
using OH.ETL.Core.Const;
using OH.ETL.Core.Extensions;
using OH.ETL.Core.Utils;
using OH.ETL.WebApi.DtoModels;
using OH.ETL.WebApi.Services;

namespace OH.ETL.WebApi.Controllers;

/// <summary>
/// UFIDIA U9C WebApi
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class PersonInfoDocController(IHttpClientFactory httpClientFactory,
    ILogger<PersonInfoDocController> logger, IOAuth2Service oAuth2Service) : ControllerBase
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly ILogger<PersonInfoDocController> _logger = logger;

    private readonly IOAuth2Service _oAuth2Service = oAuth2Service;

    /// <summary>
    /// 获取人员信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ResponseContent>> GetPersonInfoDoc([FromQuery] ReqQueryPerson req)
    {
        var accessToken = await _oAuth2Service.GetAccessToken();
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("Token", $"{accessToken}");

        var reqParams = string.Join("&",
            req.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => prop.GetValue(req) != null)
            .Select(prop => $"req.{prop.Name}={prop.GetValue(req)}"));

        var reqUrl = $"{AppSetting.ApiUrlPrefix}{U9CApi.GetPersonInfoDoc}";

        var response = await _httpClient.GetAsync($"{reqUrl}?{reqParams}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            //return List<ResPersonInfoDoc>
            //var jObject = Newtonsoft.Json.Linq.JObject.Parse(content)["Data"][0];
            //var ResData = jObject.ToObject<ResPersonInfoDoc>();
            //return ResData;
            return JsonConvert.DeserializeObject<ResponseContent>(content);

        }

        return NotFound();
    }


}
