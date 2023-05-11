using System.Collections.Generic;

public static class ListExtention
{
    public static void Add<T>(this List<T> origin, List<T> objects)
    {
        foreach (T obj in objects)
        {
            origin.Add(obj);
        }
    }
}
