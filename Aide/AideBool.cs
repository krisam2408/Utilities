namespace Aide;

public static class AideBool
{
    public static bool AndCheck(params bool[] args)
    {
        foreach(bool b in args)
            if(!b) 
                return false;
        return true;
    }

    public static bool OneValueOf<T>(T refValue, params T[] compare)
    {
        if(compare.Contains(refValue))
            return true;
        return false;
    }

    public static bool OrCheck(params bool[] args)
    {
        foreach(bool b in args)
            if(b) 
                return true;
        return false;
    }
}
