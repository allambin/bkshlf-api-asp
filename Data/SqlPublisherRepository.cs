using System;
using System.Linq;
using BKSHLF.Models;

namespace BKSHLF.Data
{
    public class SqlPublisherRepository : SqlBaseRepository, IPublisherRepository
    {
        public SqlPublisherRepository(BkshlfContext context) : base(context)
        {
        }

        public void CreatePublisher(Publisher publisher)
        {
            if (publisher == null)
            {
                throw new ArgumentNullException(nameof(publisher));
            }
            
            publisher.CreatedAt = DateTime.Now;
            publisher.UpdatedAt = DateTime.Now;

            _context.Publishers.Add(publisher);
        }

        public Publisher GetPublisher(int id)
        {
            return _context.Publishers.FirstOrDefault(p => p.Id == id);
        }

        public Publisher GetPublisher(string name)
        {
            return _context.Publishers.FirstOrDefault(p => p.Name == name);
        }
    }
}