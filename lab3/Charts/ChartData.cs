namespace lab3.Charts;

public class ChartData
{
    public readonly string Title;
    public readonly IList<DataPoint> PushHeavyResults;
    public readonly IList<DataPoint> PopHeavyResults;
    public readonly IList<DataPoint> EquallyHeavyResults;
    public readonly string XAxisTitle;
    public readonly string YAxisTitle;
    public readonly double? TotalExecTimeSeconds;

    public ChartData(
        string title,
        IList<DataPoint> pushHeavyResults,
        IList<DataPoint> popHeavyResults,
        IList<DataPoint> equallyHeavyResults,
        string xAxisTitle,
        string yAxisTitle,
        double? totalExecTimeSeconds = null)
    {
        Title = title;
        PushHeavyResults = pushHeavyResults;
        PopHeavyResults = popHeavyResults;
        EquallyHeavyResults = equallyHeavyResults;
        XAxisTitle = xAxisTitle;
        YAxisTitle = yAxisTitle;
        TotalExecTimeSeconds = totalExecTimeSeconds;
    }
}