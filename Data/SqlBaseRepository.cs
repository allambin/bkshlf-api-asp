namespace BKSHLF.Data
{
    abstract public class SqlBaseRepository
    {
        protected readonly BkshlfContext _context;

        public SqlBaseRepository(BkshlfContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}