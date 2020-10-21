using BKSHLF.Models;

namespace BKSHLF.Data
{
    public interface IEditionRepository
    {
        bool SaveChanges();
        void CreateEdition(Edition edition, Book book, Publisher publisher = null);
        Edition GetEdition(int id);
    }
}