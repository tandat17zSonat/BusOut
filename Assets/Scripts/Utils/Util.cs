using System.Collections.Generic;
using Unity.VisualScripting;

public class Util
{
    public static T GetRandomEnumValue<T>(T[] ignores = null)
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T)); // Lấy tất cả các giá trị của enum
        Dictionary<T, bool> mapIgnore = new Dictionary<T, bool>();
        int ignoreLength = 0;
        if( ignores != null)
        {
            foreach (T t in ignores)
            {
                mapIgnore.Add(t, true);
            }
            ignoreLength = ignores.Length;
        }
        
        
        int random = UnityEngine.Random.Range(0, values.Length - ignoreLength);

        int i = 0;
        foreach (T value in values)
        {
            if(mapIgnore.ContainsKey(value) == false)
            {
                if (i == random) return value;
                i += 1;
            }
        }
        return values[0];
    }

    public static int GetEnumLength<T>()
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T));
        return values.Length;
    }
}
