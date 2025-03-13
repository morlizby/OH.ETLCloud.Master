using System.Linq.Expressions;

namespace OH.ETL.Core.Extensions;

public class ExpressionParser<T>
{
    private ParameterExpression parameter = Expression.Parameter(typeof(T));

    public Expression<Func<T, bool>> ParserConditions(IEnumerable<Conditions> conditions)
    {
        // 将条件转化成表达是的Body
        var query = ParseExpressionBody(conditions);
        return Expression.Lambda<Func<T, bool>>(query, parameter);
    }

    private Expression ParseExpressionBody(IEnumerable<Conditions> conditions)
    {
        if (conditions == null || conditions.Count() == 0)
        {
            return Expression.Constant(true, typeof(bool));
        }
        else if (conditions.Count() == 1)
        {
            return ParseCondition(conditions.First());
        }
        else
        {
            Expression left = ParseCondition(conditions.First());
            Expression right = ParseExpressionBody(conditions.Skip(1));
            return Expression.AndAlso(left, right);
        }
    }

    private Expression ParseCondition(Conditions condition)
    {
        var key = Expression.Property(parameter, condition.Key);
        var value = Expression.Constant(condition.Value);

        switch (condition.Operator)
        {
            case OperatorEnum.Contains:
                return Expression.Call(key,
                    typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), value);
            case OperatorEnum.Equal:
                return Expression.Equal(key, Expression.Convert(value, key.Type));
            case OperatorEnum.Greater:
                return Expression.GreaterThan(key, Expression.Convert(value, key.Type));
            case OperatorEnum.GreaterEqual:
                return Expression.GreaterThanOrEqual(key, Expression.Convert(value, key.Type));
            case OperatorEnum.Less:
                return Expression.LessThan(key, Expression.Convert(value, key.Type));
            case OperatorEnum.LessEqual:
                return Expression.LessThanOrEqual(key, Expression.Convert(value, key.Type));
            case OperatorEnum.NotEqual:
                return Expression.NotEqual(key, Expression.Convert(value, key.Type));
            case OperatorEnum.In:
                return ParaseIn(parameter, condition);
            case OperatorEnum.Between:
                return ParaseBetween(parameter, condition);
            default:
                throw new NotImplementedException("不支持此操作");
        }
    }

    private Expression ParaseIn(ParameterExpression parameter, Conditions conditions)
    {
        ParameterExpression p = parameter;
        Expression key = Expression.Property(p, conditions.Key);
        var valueArr = conditions.Value.ToString().Split(',');
        if (valueArr.Length != 2)
        {
            throw new NotImplementedException("ParaseBetween参数错误");
        }
        try
        {
            int.Parse(valueArr[0]);
            int.Parse(valueArr[1]);
        }
        catch
        {
            throw new NotImplementedException("ParaseBetween参数只能为数字");
        }
        Expression expression = Expression.Constant(true, typeof(bool));
        //开始位置
        Expression startvalue = Expression.Constant(int.Parse(valueArr[0]));
        Expression start = Expression.GreaterThanOrEqual(key, Expression.Convert(startvalue, key.Type));

        Expression endvalue = Expression.Constant(int.Parse(valueArr[1]));
        Expression end = Expression.GreaterThanOrEqual(key, Expression.Convert(endvalue, key.Type));
        return Expression.AndAlso(start, end);
    }

    private Expression ParaseBetween(ParameterExpression parameter, Conditions conditions)
    {
        var p = parameter;
        var key = Expression.Property(p, conditions.Key);
        var valueArr = conditions.Value.ToString().Split(',');
        if (valueArr.Length != 2)
        {
            throw new NotImplementedException("ParaseBetween参数错误");
        }

        //var exp =Expression.Constant(true,typeof(bool));
        ConstantExpression startvalue = null;
        BinaryExpression start = null;
        ConstantExpression endvalue = null;
        BinaryExpression end = null;
        try
        {
            switch (conditions.ValueType)
            {
                case "int":
                    //开始位置
                    startvalue = Expression.Constant(int.Parse(valueArr[0]));
                    start = Expression.GreaterThanOrEqual(key, Expression.Convert(startvalue, key.Type));
                    endvalue = Expression.Constant(int.Parse(valueArr[1]));
                    end = Expression.LessThan(key, Expression.Convert(endvalue, key.Type));
                    break;
                case "double":
                    //开始位置
                    startvalue = Expression.Constant(double.Parse(valueArr[0]));
                    start = Expression.GreaterThanOrEqual(key, Expression.Convert(startvalue, key.Type));
                    endvalue = Expression.Constant(double.Parse(valueArr[1]));
                    end = Expression.LessThan(key, Expression.Convert(endvalue, key.Type));
                    break;
                case "datetime":
                    startvalue = Expression.Constant(DateTime.Parse(valueArr[0]));
                    start = Expression.GreaterThanOrEqual(key, Expression.Convert(startvalue, key.Type));
                    endvalue = Expression.Constant(DateTime.Parse(valueArr[1]));
                    end = Expression.LessThan(key, Expression.Convert(endvalue, key.Type));
                    break;
            }

        }
        catch (Exception ex)
        {
            throw new NotImplementedException($"ParaseBetween参数类型转换失败，{ex.Message}");
        }

        return Expression.AndAlso(start, end);
    }
}

public class Conditions
{
    /// <summary>
    /// 字段名称
    /// </summary>
    public string Key { get; set; }
    /// <summary>
    /// 值
    /// </summary>
    public object Value { get; set; }
    /// <summary>
    /// 值类型
    /// </summary>
    public string ValueType { get; set; }
    /// <summary>
    /// 逻辑运算符
    /// </summary>
    public OperatorEnum Operator { get; set; }
}

public enum OperatorEnum
{
    //包含
    Contains,
    //MethodCallExpression带方法
    Call,
    //等于
    Equal,
    //大于
    Greater,
    //大于等于
    GreaterEqual,
    //小于
    Less,
    //小于等于
    LessEqual,
    //不等于
    NotEqual,
    //等于多个in (a,b,c)
    In,
    //范围内 >= a and < b
    Between
}
