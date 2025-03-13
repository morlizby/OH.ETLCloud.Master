namespace OH.ETL.Core.Extensions;

public static class ExpressionExtension
{
    /// <summary>
    /// 扩展查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public static IQueryable<T> QueryCondition<T>(this IQueryable<T> query, IEnumerable<Conditions> conditions)
    {
        var parser = new ExpressionParser<T>();
        var filter = parser.ParserConditions(conditions);
        return query.Where(filter);
    }
}
