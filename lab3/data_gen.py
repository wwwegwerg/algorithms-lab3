import argparse
import math
import random
import string
from typing import Dict, List

# ================== НАСТРОЙКИ ==================
DEFAULT_N = 15
OUTPUT_FILE_FILLER = "filler.txt"
OUTPUT_FILE_COMMANDS = "input.txt"
RNG_SEED = 42  # None для случайного; число — для воспроизводимости

# три набора долей для трёх строк (сумма не обязана быть 100 — нормализуем)
RATIO_SETS = [
    {"push": 75, "pop": 15, "top": 6, "isempty": 3, "print": 1},  # push-heavy
    {"push": 15, "pop": 75, "top": 6, "isempty": 3, "print": 1},  # pop-heavy
    {"push": 45, "pop": 45, "top": 6, "isempty": 3, "print": 1},  # 1:1 push/pop
]
# ===============================================

KEYS = ["push", "pop", "top", "isempty", "print"]


def rand_value(rng: random.Random) -> str:
    """Возвращает случайное значение для Push:
    60% — число, 40% — строка из маленьких латинских букв."""
    if rng.random() < 0.6:
        return str(rng.randint(-(2**31), 2**31 - 1))
    length = rng.randint(3, 8)
    return "".join(rng.choices(string.ascii_lowercase, k=length))


def to_counts(total: int, ratios: Dict[str, float]) -> Dict[str, int]:
    """Распределяет total по KEYS согласно ratios, аккуратно раздавая остатки."""
    weights = [max(0.0, ratios.get(k, 0.0)) for k in KEYS]
    s = sum(weights)
    if s <= 0:
        weights = [1.0] * len(KEYS)
        s = len(KEYS)
    targets = [w / s * total for w in weights]
    base = [int(t) for t in targets]
    rem = total - sum(base)
    order = sorted(range(len(KEYS)), key=lambda i: (targets[i] - base[i]), reverse=True)
    for i in range(rem):
        base[order[i % len(base)]] += 1
    return dict(zip(KEYS, base))


def make_tokens(counts: Dict[str, int], rng: random.Random) -> List[str]:
    tokens: List[str] = []
    tokens += [f"1,{rand_value(rng)}" for _ in range(counts["push"])]
    tokens += ["2"] * counts["pop"]
    tokens += ["3"] * counts["top"]
    tokens += ["4"] * counts["isempty"]
    tokens += ["5"] * counts["print"]
    rng.shuffle(tokens)
    return tokens


def main():
    parser = argparse.ArgumentParser(
        description="Генератор тестовых файлов для команд стека."
    )
    parser.add_argument(
        "total_operations",
        type=int,
        nargs="?",
        default=DEFAULT_N,
        help=f"общее количество операций (по умолчанию {DEFAULT_N})",
    )
    args = parser.parse_args()
    total_operations = 2**args.total_operations

    if total_operations < 2**7:
        print("Внимание: TOTAL_OPERATIONS меньше 2**7; процесс завершен досрочно.")
        return

    rng = random.Random(RNG_SEED) if RNG_SEED is not None else random.Random()

    filler_tokens = [rand_value(rng) for _ in range(total_operations)]
    with open(OUTPUT_FILE_FILLER, "w", encoding="utf-8") as f:
        f.write(" ".join(filler_tokens))
    print(f"OK: создан файл {OUTPUT_FILE_FILLER} с {total_operations} токенами.")

    max_i = int(math.floor(math.log2(total_operations)))
    lines: List[str] = []
    for i in range(7, max_i + 1):
        total_per_line = 2**i
        for ratios in RATIO_SETS:
            counts = to_counts(total_per_line, ratios)  # type: ignore
            tokens = make_tokens(counts, rng)
            lines.append(" ".join(tokens))

    with open(OUTPUT_FILE_COMMANDS, "w", encoding="utf-8") as f:
        f.write("\n".join(lines))
    print(f"OK: создан файл {OUTPUT_FILE_COMMANDS} с {len(lines)} строками.")


if __name__ == "__main__":
    main()
