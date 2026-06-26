namespace PC212206.U202318609.Capbase.Platform.Paperwork.Domain.Model.ValueObjects;

/// <summary>
///     Represents the client identifier coming from another bounded context.
/// </summary>
public class PartyId
{
    private PartyId()
    {
        Value = Guid.Empty;
    }

    /// <summary>
    ///     Initializes a new party identifier.
    /// </summary>
    /// <param name="value">The identifier value.</param>
    /// <exception cref="ArgumentException">Thrown when the identifier is empty.</exception>
    public PartyId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("The client identifier must be a non-empty GUID.", nameof(value));

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
