namespace ServerApp.Services
{
    public abstract class Service
    {
        protected readonly AppDbContext _context;
        protected Service(AppDbContext context)
        {
            _context = context;
        }
    }
}
