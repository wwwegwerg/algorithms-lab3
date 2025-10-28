using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;

namespace lab3.Charts;

public static class ChartBuilder
{
    private static readonly DateTime StartTime = DateTime.Now;

    public static void Build2DLineChart(ChartData cd)
    {
        var outputDir = Path.Combine(AppContext.BaseDirectory, $"plots_{StartTime:s}");
        Directory.CreateDirectory(outputDir);

        var filePath = Path.Combine(outputDir, $"{cd.Title}.png");
        if (File.Exists(filePath))
        {
            Console.WriteLine($"Файл {filePath} уже существует");
            return;
        }

        var legend = new Legend
        {
            LegendTitle = "Легенда",
            LegendPosition = LegendPosition.RightTop,
            LegendPlacement = LegendPlacement.Outside,
            LegendOrientation = LegendOrientation.Vertical,
            LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
            LegendBorder = OxyColors.Black
        };

        var model = new PlotModel
        {
            Title = cd.Title,
            Background = OxyColors.White,
        };
        model.Legends.Add(legend);
        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Bottom,
            Title = cd.XAxisTitle,
            Minimum = cd.PushHeavyResults[0].X,
            Maximum = cd.PushHeavyResults[^1].X
        });
        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Title = cd.YAxisTitle,
        });

        var s1 = new LineSeries { Title = "PushHeavy", StrokeThickness = 4 };
        var s2 = new LineSeries { Title = "PopHeavy", StrokeThickness = 4 };
        var s3 = new LineSeries { Title = "EquallyHeavy", StrokeThickness = 4 };

        s1.Points.AddRange(cd.PushHeavyResults);
        s2.Points.AddRange(cd.PopHeavyResults);
        s3.Points.AddRange(cd.EquallyHeavyResults);

        model.Series.Add(s1);
        model.Series.Add(s2);
        model.Series.Add(s3);

        PngExporter.Export(model, filePath, 2000, 1400);

        Console.WriteLine("Готово!");
        Console.WriteLine($"Файл сохранён: {filePath}");
    }
}