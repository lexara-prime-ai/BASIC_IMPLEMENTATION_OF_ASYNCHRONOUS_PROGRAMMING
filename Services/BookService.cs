using System.Text;
using BookShop.Models;
using Newtonsoft.Json;

namespace BookShop.Services;
public class BookService : IBookInterface
{
    private readonly HttpClient _httpClient;
    private readonly string URL = "http://localhost:3000/books";
    public BookService()
    {
        _httpClient = new HttpClient();

    }

    // HANDLE DATABASE COMMUNICATION
    // ADD BOOK
    public async Task<SuccessMessage> AddBookAsync(AddBook book)
    {
        try
        {
            var content = JsonConvert.SerializeObject(book);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(URL, bodyContent);

            if (response.IsSuccessStatusCode)
            {
                return new SuccessMessage { Message = "Book Added successfully" };
            }
            else
            {
                // Log the error details
                string errorMessage = $"HTTP request failed with status code: {response.StatusCode}";
                throw new HttpRequestException(errorMessage);
            }
        }
        catch (HttpRequestException ex)
        {
            // Log the exception and handle it as needed
            throw; // Re-throw the exception to maintain the exception stack trace
        }
    }

    // DELETE BOOK
    public async Task<SuccessMessage> DeleteBookAsync(string id)
    {
        var response = await _httpClient.DeleteAsync(URL + "/" + id);

        // SUCCESS AND ERROR MESSAGES
        if (response.IsSuccessStatusCode)
        {
            return new SuccessMessage { Message = "Book deleted successfully" };
        }

        throw new Exception("Failed to delete book...");
    }

    // GET ALL BOOKS
    public async Task<List<Book>> GetAllBooksAsync()
    {
        var response = await _httpClient.GetAsync(URL);

        var books = JsonConvert.DeserializeObject<List<Book>>(await response.Content.ReadAsStringAsync());

        // SUCCESS AND ERROR MESSAGES
        if (response.IsSuccessStatusCode)
        {
            return books;
        }

        throw new Exception("Failed to get books...");
    }

    // GET BOOK BY ID
    public async Task<Book> GetBookAsync(string id)
    {

        var response = await _httpClient.GetAsync(URL + "/" + id);

        var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());

        // SUCCESS AND ERROR MESSAGES
        if (response.IsSuccessStatusCode)
        {
            return book;
        }

        throw new Exception("Failed to get book...");
    }

    // UPDATE BOOK
    public async Task<SuccessMessage> UpdateBookAsync(Book book)
    {
        var content = JsonConvert.SerializeObject(book);

        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(URL + "/" + book.Id, bodyContent);

        // SUCCESS AND ERROR MESSAGES
        if (response.IsSuccessStatusCode)
        {
            return new SuccessMessage { Message = "Book updated successfully..." };
        }

        throw new Exception("Failed to update book...");
    }
}
