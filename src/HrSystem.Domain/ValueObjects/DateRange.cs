namespace HrSystem.Domain.ValueObjects;

public sealed record DateRange
{
    public DateOnly Start { get; }
    public DateOnly End { get; }

    public int Days => End.DayNumber - Start.DayNumber + 1;

    public DateRange(DateOnly start, DateOnly end)
    {
        if (end < start)
            throw new ArgumentException("End date must be greater than or equal to start date.");

        Start = start;
        End = end;
    }

    public bool Overlaps(DateRange other) => Start <= other.End && other.Start <= End;
}
