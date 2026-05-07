namespace TtPlan.WebApi.Configuration.Options;

public record PostgresOptions
{
    public const string Position = "Postgres";

    public required string User { get; init; }
    public required string Password { get; init; }
    public required string Host { get; init; }
    public required string Port { get; init; }
    public required string Db { get; init; }
    
    public string ConnectionString =>
        @$"User ID = {User};
        Password = {Password};
        Host = {Host};
        Port = {Port};
        Database = {Db};";
}