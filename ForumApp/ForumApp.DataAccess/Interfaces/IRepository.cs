using ForumApp.Domain.Models;

namespace ForumApp.DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        // Get all entities of type T
        List<T> GetAll();

        // Get an entity by its Id
        T GetById(int id);

        // Add a new entity of type T
        void Add(T entity);

        // Update an existing entity of type T
        void Update(T entity);

        // Delete an entity of type T
        void Delete(T entity);
    }
}
