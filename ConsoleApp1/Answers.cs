namespace ConsoleApp1;

public static class Answers
{
    // Answer 11.1
    public static int[] SortIntArrayDescending(int[]? array)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));

        if (array.Length < 2)
            return array;

        // Собственная реализация - сортировка пузырьком - сложность в лучшем случае (массив уже отсортирован по убыванию) - O(n), в худшем (массив отсортирован по возрастанию) O(n^2)
        for (var i = 1; i < array.Length; i++)
        {
            for (var j = i; j > 0; j--)
            {
                if (array[j] > array[j - 1])
                    (array[j], array[j - 1]) = (array[j - 1], array[j]);
                else
                    break;
            }
        }

        // Использование стандартной реализации, использующей под капотом быструю сортировку, в лучшем случае O(n*log(n)), в худшем O(n^2)
        // Array.Sort(array, new Comparison<int>((i1, i2) => i2.CompareTo(i1)));

        return array;
    }

    // Answer 11.2
    public static void PingPong()
    {
        var mutex = new Mutex();
        var rnd = new Random();
        var ping = true;

        void DoIt(bool isPing)
        {
            var attemptsCount = 5;

            while (attemptsCount > 0)
            {
                Thread.Sleep(rnd.Next(100, 500));

                mutex.WaitOne();

                if (isPing == ping)
                {
                    attemptsCount--;
                    Console.WriteLine(isPing ? "ping" : "pong");
                    ping = !ping;
                }

                mutex.ReleaseMutex();
            }
        }

        var pingThread = new Thread(new ThreadStart(() => DoIt(true)));
        var pongThread = new Thread(new ThreadStart(() => DoIt(false)));

        pingThread.Start();
        pongThread.Start();

        pingThread.Join();
        pongThread.Join();
    }

    //Answer 11.3
    public static double GetStandardDeviation(ICollection<double>? values)
    {
        if (values?.Any() != true)
            throw new ArgumentException("Empty collection", nameof(values));

        var average = values.Sum() / values.Count;
        return Math.Sqrt(values.Sum(item => Math.Pow(item - average, 2)) / values.Count);
    }

    //Answer 11.4
    public static short GetSpecificSymbolsCount(string? str)
    {
        short result = 0;

        if (!(str?.Length >= 5))
            return result;

        for (short i = 3; i < str.Length - 1; i++)
        {
            if (str[i] == 'A' && str[i - 3] == 'B' && str[i + 1] == 'C')
            {
                result++;
                i++;
            }
        }

        return result;
    }

    //Answer 11.5
    /* Хотя LinkedList - это двусвязный список, я нигде не использовал свойство Previous его узлов, поэтому будем считать его односвязным -
     * стандартной реализации односвязного списка нет, а писать свою долго
     */
    public static bool IsClosedList<T>(LinkedList<T>? list)
    {
        if (!(list?.Count > 0))
            throw new ArgumentException("Empty list", nameof(list));

        var hashSet = new HashSet<LinkedListNode<T>>();
        var current = list.First;

        while (current!.Next != null)
        {
            if (hashSet.Contains(current.Next))
                return true;

            hashSet.Add(current.Next);

            current = current.Next;
        }

        return false;
    }
}