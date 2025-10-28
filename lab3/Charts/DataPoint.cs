namespace lab3.Charts;

public readonly struct DataPoint
{
    public DataPoint(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; }

    public double Y { get; }

    public override string ToString()
    {
        return X + " " + Y;
    }
}