using CRUD.Models;
using CRUD.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CRUD.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioService(IOptions<MongoDBConfig> settings)
        {
            var client = new MongoClient(settings.Value.Connection);
            var db = client.GetDatabase(settings.Value.Dbname);
            _usuarios = db.GetCollection<Usuario>(settings.Value.UsuariosCollection);
        }

        public async Task<List<Usuario>> GetAllAsync() =>
            await _usuarios.Find(_ => true).ToListAsync();

        public async Task<Usuario?> GetByIdAsync(string id) =>
            await _usuarios.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Usuario?> GetByEmailAsync(string email) =>
            await _usuarios.Find(x => x.Email == email).FirstOrDefaultAsync();

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
            return usuario;
        }

        public async Task UpdateAsync(string id, Usuario usuario) =>
            await _usuarios.ReplaceOneAsync(x => x.Id == id, usuario);

        public async Task DeleteAsync(string id) =>
            await _usuarios.DeleteOneAsync(x => x.Id == id);

    }
}
