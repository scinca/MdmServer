// Default.cs

namespace Api.SharedKernel.Defaults;

public static class Default
{
    public static TimeSpan Timeout { get; } = TimeSpan.FromSeconds(10);
}
