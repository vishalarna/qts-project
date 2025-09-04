#nullable enable
using System.Text.Json.Serialization;

namespace QTD2.Infrastructure.Model
{
    public class Result
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Error { get; init; }
        public bool IsSuccess => string.IsNullOrEmpty(Error);

        public static Result CreateSuccess() => new();
        public static Result CreateError(string error) => new() { Error = error };
    }

    public class Result<TData> : Result
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TData? Data { get; init; }

        public static Result<TData> CreateSuccess(TData result) => new() { Data = result };
        public new static Result<TData> CreateError(string error) => new() { Error = error };
    }
}
