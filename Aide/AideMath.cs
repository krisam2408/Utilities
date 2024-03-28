namespace Aide;

public static class AideMath
{
    public static int Min(this int value, int min)
    {
        if(value < min)
            return min;
        return value;
    }

    public static int Max(this int value, int max)
    {
        if(value > max)
            return max;
        return value;
    }

    public static int Clamp(this int value, int min, int max)
    {
        if(value < min)
            return min;
        if(value > max)
            return max;
        return value;
    }

    public static float Clamp(this float value, float min, float max)
    {
        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
    }

    public static bool BetweenInc(this int value, int min, int max)
    {
        if(value >= min && value <= max)
            return true;
        return false;
    }

    public static bool BetweenExc(this int value, int min, int max)
    {
        if(value > min && value < max)
            return true;
        return false;
    }

    public static float Percentage(this IEnumerable<bool> values)
    {
        float len = values.Count();
        int sum = values.Sum(v =>
        {
            if (v)
                return 1;
            return 0;
        });

        return sum / len;
    }

    public static TimeSpan Average(this IEnumerable<TimeSpan> values)
    {
        IEnumerable<int> secondsArray = values
            .Select(x => (int)x.TotalSeconds);

        int average = (int)secondsArray.Average();
        TimeSpan result = new(0, 0, average);
        return result;
    }

    public static T[] Mode<T>(this IEnumerable<T> values) where T : class
    {
        if(!values.Any())
            return Array.Empty<T>();

        Dictionary<T, int> dictionary = new();

        foreach(T value in values)
        {
            if(dictionary.TryAdd(value, 1))
                continue;

            dictionary[value]++;
        }

        int maxValue = dictionary.Max(kv => kv.Value);

        T[] result = dictionary
            .Where(d => d.Value == maxValue)
            .Select(d => d.Key)
            .ToArray();

        return result;
    }

    public static T[] Mode<T>(this IEnumerable<T> values, out int count) where T : class
    {
        if (!values.Any())
        {
            count = 0;
            return Array.Empty<T>();
        }

        Dictionary<T, int> dictionary = new();

        foreach (T value in values)
        {
            if (dictionary.TryAdd(value, 1))
                continue;

            dictionary[value]++;
        }

        int maxValue = dictionary.Max(kv => kv.Value);
        count = maxValue;

        T[] result = dictionary
            .Where(d => d.Value == maxValue)
            .Select(d => d.Key)
            .ToArray();

        return result;
    }

    public static float[] Split(this float value, params float[] weights)
    {
        List<float> result = new();

        float weightSum = weights.Sum();

        foreach(float weight in weights)
            result.Add(value * weight / weightSum);

        return result.ToArray();
    }
}
