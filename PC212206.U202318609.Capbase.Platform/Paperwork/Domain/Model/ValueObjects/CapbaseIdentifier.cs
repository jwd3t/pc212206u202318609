namespace PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.ValueObjects;

/// <summary>
///     Represents the external identifier assigned to a covenant document.
/// </summary>
public class CapbaseIdentifier
{
    private CapbaseIdentifier()
    {
        Value = Guid.Empty;
    }

    /// <summary>
    ///     Initializes a new identifier.
    /// </summary>
    /// <param name="value">The identifier value.</param>
    /// <exception cref="ArgumentException">Thrown when the identifier is empty.</exception>
    public CapbaseIdentifier(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("The document identifier must be a non-empty GUID.", nameof(value));

        Value = value;
    }

    /// <summary>
    ///     Gets the underlying identifier value.
    /// </summary>
    public Guid Value { get; private set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Value.ToString();
    }
}
