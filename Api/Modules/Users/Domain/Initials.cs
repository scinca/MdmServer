// Initials.cs

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Modules.Users.Domain;

[JsonConverter(typeof(InitialsJsonConverter))]
public readonly record struct Initials
{
    public char First { get; } = 'A';
    public char Last { get; } = 'B';

  public override string ToString() => $"{First}{Last}";

  public Initials(ReadOnlySpan<char> name)
  {
      if (name.IsWhiteSpace() || name.Length != 2)
      {
          throw new ArgumentException($"{nameof(name)} should contain 2 characters");
      }

      First = name[0];
      Last = name[1];
  }

  public Initials(char first, char last)
  {
      First = first;
      Last = last;
  }
}


public sealed class InitialsJsonConverter : JsonConverter<Initials>
{
    public override Initials Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is not JsonTokenType.String)
        {
            throw new JsonException("Expected a string");
        }

        Span<char> buffer = stackalloc char[2];

        return reader.CopyString(buffer) != 2
            ? throw new JsonException("String must be 2 characters long")
            : new Initials(buffer);
    }

    public override void Write(Utf8JsonWriter writer, Initials value, JsonSerializerOptions options)
    {
        Span<char> buffer = [value.First, value.Last];
        writer.WriteStringValue(buffer);
    }
}

public sealed class InitialsEfConverter()
    : ValueConverter<Initials, string>
    (initials => initials.ToString(),
    value => new Initials(value)
    );


