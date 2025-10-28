using System.Text;
using DataStructures;

namespace lab3.Tasks;

public static class Huffman
{
    public static void Run()
    {Console.WriteLine("Введите текст");
        var input = Console.ReadLine()?.Trim().ToLower();
        while (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Введите текст");
            input = Console.ReadLine()?.Trim().ToLower();
        }

        var tree = HuffmanCodec.BuildTree(input);
        var codes = HuffmanCodec.BuildCodes(tree);
        var encoded = HuffmanCodec.Encode(input, codes);
        var decoded = HuffmanCodec.Decode(encoded, tree);

        Console.WriteLine("Исходный текст: " + input);
        Console.WriteLine("Коды:");
        foreach (var kv in codes)
            Console.WriteLine($"  '{kv.Key}' -> {kv.Value}");

        Console.WriteLine("Закодировано: " + encoded);
        Console.WriteLine("Декодировано: " + decoded);
    }

    // ------------------------------
    // Данные узла для Хаффмана
    // ------------------------------
    public class HuffmanValue
    {
        public char? Symbol { get; }
        public int Frequency { get; }
        public bool IsLeaf => Symbol.HasValue;

        public HuffmanValue(char? symbol, int frequency)
        {
            Symbol = symbol;
            Frequency = frequency;
        }

        public override string ToString()
            => IsLeaf ? $"{Symbol}:{Frequency}" : $"•:{Frequency}";
    }

    // ------------------------------
    // Кодек Хаффмана на базе универсального бинарного дерева
    // ------------------------------
    private static class HuffmanCodec
    {
        /// <summary>
        /// Построение дерева Хаффмана для заданного текста.
        /// </summary>
        public static BinaryTree<HuffmanValue> BuildTree(string text)
        {
            if (string.IsNullOrEmpty(text))
                return new BinaryTree<HuffmanValue>(null);

            // Подсчёт частот
            var freq = new Dictionary<char, int>();
            foreach (var c in text)
            {
                if (freq.ContainsKey(c)) freq[c]++;
                else freq[c] = 1;
            }

            // Очередь с приоритетом по частоте (меньшая частота — больший приоритет)
            var pq = new PriorityQueue<TreeNode<HuffmanValue>, int>();
            foreach (var kv in freq)
            {
                var leaf = new TreeNode<HuffmanValue>(new HuffmanValue(kv.Key, kv.Value));
                pq.Enqueue(leaf, kv.Value);
            }

            // Спец-случай: единственный символ
            if (pq.Count == 1)
            {
                var only = pq.Dequeue();
                var root = new TreeNode<HuffmanValue>(new HuffmanValue(null, only.Value.Frequency));
                root.SetLeft(only); // код для единственного символа станет "0"
                return new BinaryTree<HuffmanValue>(root);
            }

            // Построение
            while (pq.Count > 1)
            {
                var a = pq.Dequeue(); // мин1
                var b = pq.Dequeue(); // мин2
                var parent = new TreeNode<HuffmanValue>(
                    new HuffmanValue(null, a.Value.Frequency + b.Value.Frequency),
                    a, b
                );
                pq.Enqueue(parent, parent.Value.Frequency);
            }

            var rootNode = pq.Dequeue();
            return new BinaryTree<HuffmanValue>(rootNode);
        }

        /// <summary>
        /// Генерация кодов символов (символ -> битовая строка).
        /// </summary>
        public static Dictionary<char, string> BuildCodes(BinaryTree<HuffmanValue> tree)
        {
            var map = new Dictionary<char, string>();
            if (tree.Root is null) return map;

            foreach (var (node, path) in tree.LeavesWithPaths(n => n.Value.IsLeaf))
            {
                var ch = node.Value.Symbol!.Value;
                map[ch] = path.Length == 0 ? "0" : path; // на случай односимвольного алфавита
            }

            return map;
        }

        /// <summary>
        /// Кодирование строки в битовую строку ('0'/'1').
        /// </summary>
        public static string Encode(string text, Dictionary<char, string> codeMap)
        {
            var sb = new StringBuilder(text.Length * 2);
            foreach (var c in text)
            {
                if (!codeMap.TryGetValue(c, out var code))
                    throw new KeyNotFoundException($"Для символа '{c}' нет кода Хаффмана.");
                sb.Append(code);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Декодирование битовой строки по дереву.
        /// </summary>
        public static string Decode(string bits, BinaryTree<HuffmanValue> tree)
        {
            if (tree.Root is null) return string.Empty;

            var result = new StringBuilder();
            var node = tree.Root;

            foreach (var bit in bits)
            {
                node = bit == '0' ? node!.Left : node!.Right;
                if (node is null) throw new InvalidOperationException("Некорректная битовая последовательность.");

                if (node.Value.IsLeaf)
                {
                    result.Append(node.Value.Symbol);
                    node = tree.Root;
                }
            }

            return result.ToString();
        }
    }
}