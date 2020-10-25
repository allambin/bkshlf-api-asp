using BKSHLF.Models;

namespace BKSHLF.Data
{
    public interface ISerieRepository
    {
        bool SaveChanges();

        Serie GetSerie(int id);
        void CreateSerie(Serie serie);
        void UpdateSerie(Serie serie);
        void AddBookToSerie(Serie serie, Book book, int order, int type = (int)BookSerie.BookType.Primary);
        void RemoveBookFromSerie(Serie serie, Book book);
        void DeleteSerie(Serie serie);
    }
}