using System.Collections.Immutable;
using System.Data.Common;
using FradminDomain.Exceptions;
using FradminDomain.SGBDs.Interfaces;
using Npgsql;

namespace FradminDomain.SGBDs.Handlers;

public sealed class PostgresExceptionHandler : ISgbdExceptionHandler
{
    public BaseException Handler(DbException dbException)
    {
        var psqlExcept = (PostgresException)dbException;
        
        var except = psqlExcept.SqlState switch
        {
            "23505" => new EntityConflictException(CreateDic(new[]
            {
                new KeyValuePair<string, object>("entity", NormalizeTableName(psqlExcept.TableName)),
                new KeyValuePair<string, object>("attr", NormalizeConstraintName(psqlExcept.ConstraintName)),
            })),
            _ => new EntityConflictException(CreateDic(ArraySegment<KeyValuePair<string, object>>.Empty)),
        };

        return except;
    }

    private static string NormalizeTableName(string? tableName)
    {
        if (String.IsNullOrEmpty(tableName)) return "";

        var tokens = tableName.Split("_");
        var tokensNormalized = tokens[1..].Select(s => char.ToUpper(s[0]) + s[1..]);

        return string.Join("", tokensNormalized);
    }

    private static string NormalizeConstraintName(string? constraint)
    {
        if (String.IsNullOrEmpty(constraint)) return "";

        var tokens = constraint.Split("_");
        var tokensNormalized = tokens[1..^1].Select(s => char.ToUpper(s[0]) + s[1..]);

        return string.Join("", tokensNormalized);
    }

    private static ImmutableDictionary<string, object> CreateDic(IEnumerable<KeyValuePair<string, object>> pairs)
    {
        return new Dictionary<string, object>(pairs).ToImmutableDictionary();
    }
}