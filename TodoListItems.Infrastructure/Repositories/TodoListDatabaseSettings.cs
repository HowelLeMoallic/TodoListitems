namespace TodoListItems.Infrastructure.Repositories
{
    public class TodoListDatabaseSettings
    {
        public static string ConnectionString { get; set; } = string.Empty;
        public static string DatabaseName { get; set; } = string.Empty;
        public static Boolean IsSSL { get; set; }
    }
}
