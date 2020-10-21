using BKSHLF.Models;

namespace BKSHLF.Data
{
    public interface IPublisherRepository
    {
        bool SaveChanges();

        Publisher GetPublisher(int id);
        Publisher GetPublisher(string name);
        void CreatePublisher(Publisher publisher);
    }
}