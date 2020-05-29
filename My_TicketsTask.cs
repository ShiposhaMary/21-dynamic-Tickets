using System.Numerics;

namespace Tickets
{
    public static class My_TicketsTask
    {
        public static BigInteger Solve(int totalLen, int totalSum)
        {
            // Если общая сумма нечётная, счастливых билетов не существует.
            if (totalSum % 2 == 1) return new BigInteger(0);
            var halfSum = totalSum / 2;
            var opt = new BigInteger[totalLen + 1, halfSum + 1];
            // Не существует билетов нулевой длины.
            for (int i = 0; i <= halfSum; i++)
                opt[0, i] = 0;
            // Для суммы равной 0 существует только один вариант (0...0).
            for (int i = 1; i <= totalLen; i++)
                opt[i, 0] = 1;
            for (int i = 1; i <= totalLen; i++)
                for (int j = 1; j <= halfSum; j++)
                    // Если сумма больше кол-ва знаков * 9, билетов не существует.
                    if (j > i * 9) opt[i, j] = 0;
                    else
                    {
                        // Берём варианты с той же суммой, но короче на знак, (как бы приписываем справа ноль)
                        // и с той же длиной, но суммой, меньше на 1 (увеличиваем последний знак на 1).
                        opt[i, j] = opt[i - 1, j] + opt[i, j - 1];
                        // Если сумма больше 9, то некоторые числа заканчиваются на 9, к ним
                        // нельзя прибавить 1, вычитаем их.
                        if (j > 9) opt[i, j] -= opt[i - 1, j - 10];
                    }
            return opt[totalLen, halfSum] * opt[totalLen, halfSum];
        }
    }
}
