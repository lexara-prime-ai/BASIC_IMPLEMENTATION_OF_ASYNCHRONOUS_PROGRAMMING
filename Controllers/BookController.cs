using BookShop.Helpers;
using BookShop.Models;
using BookShop.Services;

namespace BookShop.Controllers;

public class BookController
{
    BookService bookService = new BookService();
    // HANDLE COMMUNICATION WITH SERVICSE
    public async static Task rax_INIT()
    {
        Console.WriteLine($"{Environment.NewLine}Welcome to \"THE\" Book Shop...{Environment.NewLine}");
        Console.WriteLine("1. Add book");
        Console.WriteLine("2. View books");
        Console.WriteLine("3. View book");
        Console.WriteLine("4. Update book");
        Console.WriteLine("5. Delete book");

        var rdx_INPUT = Console.ReadLine();
        var validateResults = Validation.VALIDATE(new List<string> { rdx_INPUT });

        if (!validateResults)
        {
            await BookController.rax_INIT();
        }
        else
        {
            await new BookController().MenuRedirect(rdx_INPUT);
        }
    }

    public async Task MenuRedirect(string id)
    {
        Console.WriteLine($"Selected option: {id}");

        switch (id)
        {
            case "1":
                await rax_AddBook();
                break;
            case "2":
                await rax_ViewBooks();
                break;
            case "3":
                await rax_ViewBook();
                break;
            case "4":
                await rax_UpdateBook();
                break;
            case "5":
                await rax_DeleteBook();
                break;
            default:
                await BookController.rax_INIT();
                break;
        }
    }

    public async Task rax_AddBook()
    {
        // TITLE
        Console.WriteLine("Enter book title >");
        var bookTitle = Console.ReadLine();

        // DESCRIPTION
        Console.WriteLine("Enter book description >");
        var bookDescription = Console.ReadLine();

        // AUTHOR
        Console.WriteLine("Enter book author >");
        var bookAuthor = Console.ReadLine();

        // PRICE
        Console.WriteLine("Enter book price >");
        var bookPrice = Console.ReadLine();

        // CREATE NEW AddBook INSTANCE
        var newBook = new AddBook()
        {
            Title = bookTitle,
            Description = bookDescription,
            Author = bookAuthor,
            Price = bookPrice
        };

        // CALL SERVICS
        try
        {
            var response = await bookService.AddBookAsync(newBook);

            Console.WriteLine(response.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }

    // VIEW ALL BOOKS
    public async Task rax_ViewBooks()
    {
        try
        {
            // DISPLAY MESSAGE FOR USER
            Console.WriteLine($"{Environment.NewLine}**** Available Books ****");

            var books = await bookService.GetAllBooksAsync();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}. {book.Title}");
            }

            Console.WriteLine(Environment.NewLine);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }

    // VIEW SINGLE BOOK
    public async Task rax_ViewBook()
    {
        try
        {
            // DISPLAY AVAILABLE BOOKS
            await rax_ViewBooks();

            // DISPLAY MESSAGE FOR USER
            Console.WriteLine($"{Environment.NewLine}Enter book id to view additional information e.g 1, 2, 3 etc....");
            string id = Console.ReadLine();

            var book = await bookService.GetBookAsync(id);

            // LINE BREAK
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Description: {book.Description}");
            Console.WriteLine($"Price: KES {book.Price}/=");

            // LINE BREAK
            Console.WriteLine(Environment.NewLine);


        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }

    public async Task rax_UpdateBook()
    {
        // DISPLAY AVAILABLE BOOKS
        await rax_ViewBooks();

        Console.WriteLine("You're about to update a book");
        Console.WriteLine("Enter book id e.g 1, 2, 3 etc....");

        // RECEIVE USER INPUT
        string id = Console.ReadLine();

        // TITLE
        Console.WriteLine("Enter book title >");
        var bookTitle = Console.ReadLine();

        // DESCRIPTION
        Console.WriteLine("Enter book description >");
        var bookDescription = Console.ReadLine();

        // AUTHOR
        Console.WriteLine("Enter book author >");
        var bookAuthor = Console.ReadLine();

        // PRICE
        Console.WriteLine("Enter book price >");
        var bookPrice = Console.ReadLine();

        // CREATE NEW Book INSTANCE
        var updatedBook = new Book()
        {
            Id = id,
            Title = bookTitle,
            Description = bookDescription,
            Author = bookAuthor,
            Price = bookPrice
        };

        try
        {
            var response = await bookService.UpdateBookAsync(updatedBook);
            Console.WriteLine(response.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }

    public async Task rax_DeleteBook()
    {
        try
        {
            // DISPLAY AVAILABLE BOOKS
            await rax_ViewBooks();

            Console.WriteLine("You're about to delete a book!!!");
            Console.WriteLine("Enter book id e.g 1, 2, 3 etc....");

            // RECEIVE USER INPUT
            string id = Console.ReadLine();

            var response = await bookService.DeleteBookAsync(id);

            Console.WriteLine(response.Message);

        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occured: {e.Message}");
        }
    }
}
