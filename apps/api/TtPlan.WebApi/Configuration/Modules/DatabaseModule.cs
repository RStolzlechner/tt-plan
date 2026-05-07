using System.Data;
using Dapper;
using Npgsql;
using TtPlan.WebApi.Models.Enums;

namespace TtPlan.WebApi.Configuration.Modules;

public class StatusTypeHandler : SqlMapper.TypeHandler<Status>
{
    public override void SetValue(IDbDataParameter parameter, Status value)
        => parameter.Value = value.ToDbString();

    public override Status Parse(object value)
        => StatusExtensions.FromDbString((string)value);
}

public static class DatabaseModule
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        SqlMapper.AddTypeHandler(new StatusTypeHandler());
        DefaultTypeMap.MatchNamesWithUnderscores = true;

        var dataSource = new NpgsqlDataSourceBuilder(connectionString).Build();
        services.AddSingleton(dataSource);

        return services;
    }
}