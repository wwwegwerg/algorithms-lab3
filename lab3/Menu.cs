namespace lab3;

public class Menu {
    private string Title { get; }
    private Dictionary<int, (string label, Action action)> Items { get; } = new();

    public Menu(string title) => Title = title;

    public void Add(int key, string label, Action action) => Items[key] = (label, action);

    public void Run(bool showBack = false, Action? onBack = null) {
        while (true) {
            Console.Clear();
            Console.WriteLine("=== " + Title + " ===");
            foreach (var kv in Items) {
                Console.WriteLine($"{kv.Key}) {kv.Value.label}");
            }

            if (showBack) {
                Console.WriteLine("0) Назад");
            }

            Console.Write("Выберите пункт: ");

            var choice = Console.ReadLine();
            if (showBack && choice == "0") {
                onBack?.Invoke();
                return;
            }

            if (int.TryParse(choice, out var n) && Items.TryGetValue(n, out var item)) {
                item.action();
            } else {
                Console.WriteLine("Неверный ввод.");
                Pause();
            }
        }
    }

    public static void Pause() {
        Console.WriteLine();
        Console.Write("Нажмите Enter, чтобы продолжить...");
        Console.ReadLine();
    }
}