namespace TodoListItems.Application
{
    public class Core
    {
        public static Dictionary<int, string> EnumDictionary<T>() where T : System.Enum
        {
            return System.Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToDictionary(t => Convert.ToInt32(t), t => t.ToString());
        }
    }
}
