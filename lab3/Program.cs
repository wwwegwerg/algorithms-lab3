using DataStructures;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;

namespace lab3;

public class Program
{
    private static readonly List<string[]> InputSame = Parser.ReadData("input_same.txt");
    private static readonly List<string[]> InputVar = Parser.ReadData("input_var.txt");

    public static void Main()
    {
        var main = new Menu("Главное меню");
        main.Add(1, "Часть 1. Стек", () => GetStackMenu().Run(showBack: true));
        main.Add(2, "Часть 2. Очередь", () => GetQueueMenu().Run(showBack: true));
        main.Add(3, "Часть 3. Динамические структуры", () => GetDynamicStructsMenu().Run(showBack: true));
        main.Add(4, "Часть 4. Связный список", () => GetLinkedListMenu().Run(showBack: true));
        main.Add(0, "Выход", () => Environment.Exit(0));

        main.Run();
    }

    private static Menu GetStackMenu()
    {
        var stackMenu = new Menu("Часть 1. Стек");
        stackMenu.Add(1, "input_same.txt", () =>
        {
            for (var i = 0; i < 10; i++)
            {
                var line = InputSame[i];
                Console.WriteLine($"case: {string.Join(' ', line)}");
                Parser.ParseData(line, new CustomStack<string>());
                Console.WriteLine("--------------------");
            }

            Menu.Pause();
        });
        stackMenu.Add(2, "Вычисление постфиксного выражения", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });
        stackMenu.Add(3, "Перевод инфиксного выражения в постфиксное", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });

        return stackMenu;
    }

    private static Menu GetQueueMenu()
    {
        var queueMenu = new Menu("Часть 2. Очередь");
        queueMenu.Add(1, "input_var.txt", () =>
        {
            for (var i = 0; i < 10; i++)
            {
                var line = InputVar[i];
                Console.WriteLine($"case: {string.Join(' ', line)}");
                Parser.ParseData(line, new CustomStack<string>());
                Console.WriteLine("--------------------");
            }

            Menu.Pause();
        });
        queueMenu.Add(2, "input_same.txt", () =>
        {
            for (var i = 0; i < 10; i++)
            {
                var line = InputSame[i];
                Console.WriteLine($"case: {string.Join(' ', line)}");
                Parser.ParseData(line, new CustomStack<string>());
                Console.WriteLine("--------------------");
            }

            Menu.Pause();
        });

        return queueMenu;
    }

    private static Menu GetDynamicStructsMenu()
    {
        var dynamicStructsMenu = new Menu("Часть 3. Динамические структуры");
        dynamicStructsMenu.Add(1, "Список: алгоритм Кадане", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(2, "Стек: валидация скобочных выражений", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(3, "Очередь: скользящее среднее", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });
        dynamicStructsMenu.Add(4, "Деревья: алгоритм Хаффмана", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });

        return dynamicStructsMenu;
    }

    private static Menu GetLinkedListMenu()
    {
        var linkedListMenu = new Menu("Часть 4. Связный список");
        linkedListMenu.Add(1, "Список", () =>
        {
            Console.WriteLine();
            Menu.Pause();
        });

        return linkedListMenu;
    }

    private static void GenerateTestPlot(string title)
    {
        // === 1. Папка и имена файлов ===
        var outputDir = Path.Combine(AppContext.BaseDirectory, "plots");
        Directory.CreateDirectory(outputDir);

        var pngPath = Path.Combine(outputDir, $"{title}.png");

        // === 2. Создаём модель графика ===
        var model = new PlotModel { Title = "y = sin(x)", Background = OxyColors.White };

        // Ось X в радианах (0..2π)
        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Bottom,
            Title = "x (rad)",
            Minimum = 0,
            Maximum = 2 * Math.PI
        });

        // Ось Y (-1..1)
        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Title = "y",
            Minimum = -1.1,
            Maximum = 1.1
        });

        // === 3. Серия с синусом ===
        var series = new LineSeries
        {
            Title = "sin(x)",
            StrokeThickness = 2
        };

        // дискретизируем 0..2π
        int points = 1000;
        double xMin = 0.0;
        double xMax = 2 * Math.PI;
        double step = (xMax - xMin) / (points - 1);

        for (int i = 0; i < points; i++)
        {
            double x = xMin + i * step;
            double y = Math.Sin(x);
            series.Points.Add(new DataPoint(x, y));
        }

        model.Series.Add(series);

        // === 4. Экспорт в файлы ===
        PngExporter.Export(model, pngPath, 1000, 700);

        Console.WriteLine("Готово!");
        Console.WriteLine($"Файл сохранён: {pngPath}");
    }
}