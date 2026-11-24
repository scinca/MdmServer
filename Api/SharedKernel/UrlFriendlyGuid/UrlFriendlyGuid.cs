// UrlFriendlyGuid.cs

using System.Buffers.Text;
using System.Diagnostics.CodeAnalysis;

namespace Api.SharedKernel.UrlFriendlyGuid;

public readonly record struct UrlFriendlyGuid : IParsable<UrlFriendlyGuid>
{
    private const int GuidLength = 16;
    private const int EncodedLength = 22; // no padding

    private UrlFriendlyGuid(ReadOnlySpan<byte> input)
    {
        Value = new Guid(input);
    }

    public UrlFriendlyGuid(Guid guid)
    {
        Value = guid;
    }

    public Guid Value { get; }

    public static UrlFriendlyGuid Parse(string s, IFormatProvider? provider)
        => TryParse(s, provider ,out var guid) ? guid
            : throw new FormatException("Malformed string");

    /// <summary>
    /// ToString() was overridden to return the <see cref="Base64Url"/> encoded string value.
    /// </summary>
    /// <returns>The <see cref="Base64Url"/> encoded string of the <see cref="Value"/></returns>
    public override string ToString()
    {
        Span<byte> bytes = stackalloc byte[GuidLength];
        Value.TryWriteBytes(bytes);
        return Base64Url.EncodeToString(bytes);
    }

    public static bool TryParse([NotNullWhen(true)] string? input,
        IFormatProvider? provider,
        out UrlFriendlyGuid result)
    {
        if (input is null || input.Length != EncodedLength)
        {
            result = default;
            return false;
        }

        Span<byte> bytes = stackalloc byte[GuidLength];

        if(Base64Url.TryDecodeFromChars(input, bytes, out int written)
           && written == GuidLength)
        {
            result = new UrlFriendlyGuid(bytes);
            return true;
        }
        result = default;
        return false;
    }
}
