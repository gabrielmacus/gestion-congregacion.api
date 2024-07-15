using gestion_congregacion.api.Features.Publishers;

namespace gestion_congregacion.api.Features.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Repositories
        public IPublisherRepository Publishers { get; }


        public UnitOfWork(
            AppDbContext context,
            IPublisherRepository publishers
         )
        {
            _context = context;

            Publishers = publishers;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    _context.Dispose();

                }
                // Dispose unmanaged resources.
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
