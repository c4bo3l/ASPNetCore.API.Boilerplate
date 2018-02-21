using ASPNetCore.API.Boilerplate.Models;

namespace ASPNetCore.API.Boilerplate.Repositories
{
    public abstract class BaseRepository
    {
        protected MyDBContext Context { get; private set; }

        protected BaseRepository(MyDBContext context) {
            Context = context;
        }
    }
}
