using BookShop.Models;

namespace BookShop.Services
{
    public interface IBookInterface
    {
        // IMPLEMENT CRUD OPERATIONS
        Task<SuccessMessage> AddBookAsync(AddBook book);

        Task<SuccessMessage> UpdateBookAsync(Book book);

        Task<SuccessMessage> DeleteBookAsync(string id);

        Task<Book> GetBookAsync(string id);

        Task<List<Book>> GetAllBooksAsync();

    }
}