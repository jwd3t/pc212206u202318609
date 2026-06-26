namespace PC212206.U202318609.Capbase.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Represents a monetary amount composed of a value and a currency.
/// </summary>
public class MonetaryAmount
{
    private MonetaryAmount()
    {
        Value = decimal.Zero;
        Currency = string.Empty;
    }

    /// <summary>
    ///     Initializes a new monetary amount.
    /// </summary>
    /// <param name="value">The amount value.</param>
    /// <param name="currency">The currency code or label.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the amount is negative.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the currency is null, empty, or whitespace.</exception>
    public MonetaryAmount(decimal value, string currency)
    {
        if (value < decimal.Zero)
            throw new ArgumentOutOfRangeException(nameof(value), "The monetary amount value must be greater than or equal to zero.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentNullException(nameof(currency), "The monetary amount currency is required.");

        Value = value;
        Currency = currency.Trim();
    }

    /// <summary>
    ///     Gets the numeric amount.
    /// </summary>
    public decimal Value { get; private set; }

    /// <summary>
    ///     Gets the currency.
    /// </summary>
    public string Currency { get; private set; }

    /// <summary>
    ///     Multiplies the amount by the provided factor while keeping the same currency.
    /// </summary>
    /// <param name="factor">The multiplication factor.</param>
    /// <returns>A new <see cref="MonetaryAmount" /> with the calculated value.</returns>
    public MonetaryAmount Multiply(decimal factor)
    {
        return new MonetaryAmount(Value * factor, Currency);
    }
}
