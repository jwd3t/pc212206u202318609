namespace PC212206.U202318609.Capbase.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Represents a legal validity period with a start and end date.
/// </summary>
public class LegalityPeriod
{
    private LegalityPeriod()
    {
        StartDate = default;
        EndDate = default;
    }

    /// <summary>
    ///     Initializes a new legality period.
    /// </summary>
    /// <param name="startDate">The period start date.</param>
    /// <param name="endDate">The period end date.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown when the end date is less than or equal to the start date.
    /// </exception>
    public LegalityPeriod(DateOnly startDate, DateOnly endDate)
    {
        if (endDate <= startDate)
            throw new ArgumentException("End date must be greater than the start date.", nameof(endDate));

        StartDate = startDate;
        EndDate = endDate;
    }

    /// <summary>
    ///     Gets the period start date.
    /// </summary>
    public DateOnly StartDate { get; private set; }

    /// <summary>
    ///     Gets the period end date.
    /// </summary>
    public DateOnly EndDate { get; private set; }

    /// <summary>
    ///     Returns whether the period is expired on the provided date.
    /// </summary>
    /// <param name="checkDate">The date to compare against the period end date.</param>
    /// <returns><c>true</c> when the provided date is after the end date; otherwise <c>false</c>.</returns>
    public bool IsExpired(DateOnly checkDate)
    {
        return checkDate > EndDate;
    }
}
