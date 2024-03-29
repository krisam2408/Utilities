﻿namespace Aide;

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
}
