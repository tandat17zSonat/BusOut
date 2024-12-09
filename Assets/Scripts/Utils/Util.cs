public class Util
{
    public static T GetRandomEnumValue<T>()
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T)); // Lấy tất cả các giá trị của enum
        return values[UnityEngine.Random.Range(0, values.Length)];      // Chọn một giá trị ngẫu nhiên
    }
}
