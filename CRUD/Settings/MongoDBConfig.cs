namespace CRUD.Settings
{
    public class MongoDBConfig
    {
        public string Connection { get; set; } = null!;
        public string Dbname { get; set; } = null!;

        public string UsuariosCollection { get; set; } = null!;

        public string ItemsCollection { get; set; } = null!;
    }
}
