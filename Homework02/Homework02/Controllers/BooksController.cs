using Homework02.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //Add GET method that returns all books
        [HttpGet]
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred, contact the admin");
            }
        }


        //Add GET method that returns one book by sending index in the query string
        [HttpGet("getOneBook")]
        public ActionResult<List<Book>> GetOneBook(int? index)
        {
            try
            {
                if(index == null)
                {
                    return BadRequest("Your must specify index");
                }
                if(index < 0)
                {
                    return BadRequest("Id can not be negative");
                }

                Book bookDb = StaticDb.Books.FirstOrDefault(x => x.Id == index);
                return Ok(bookDb);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred, contact the admin");
            }
        }

        //Add GET method that returns one book by filtering by author and title (use query string parameters)
        [HttpGet("getByAuthorOrTitle")]
        public ActionResult<List<Book>> FilterBookQuery(string? author, string? title)
        {
            try
            {
                if(string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return Ok(StaticDb.Books);
                }

                if (string.IsNullOrEmpty(title))
                {
                    List<Book> bookDb = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())).ToList();
                    return Ok(bookDb);
                }

                if (string.IsNullOrEmpty(author))
                {
                    List<Book> bookDbAuthor = StaticDb.Books.Where(x => x.Title.ToLower().Contains(title.ToLower())).ToList();
                    return Ok(bookDbAuthor);
                }

                List<Book> filteredBooks = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower())
                                                         && x.Title.ToLower().Contains(title.ToLower())).ToList();
                return Ok(filteredBooks);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred, contact the admin");
            }
        }

        //Add POST method that adds new book to the list of books (use the FromBody attribute)
        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            try
            {
                if (string.IsNullOrEmpty(book.Author))
                {
                    return BadRequest("Your must specify author");
                }

                if (string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("Your must specify title");
                }

                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occurred, contact the admin");
            }
        }

        //Add POST method that accepts list of books from the body of the request and returns their titles as a list of strings.
        [HttpPost("listOfBooks")]
        public IActionResult ListOfBooksTitle([FromBody] List<Book> books)
        {
            if(books == null || books.Count == 0)
            {
                return BadRequest("No books in the body.");
            }

            List<string> bookTitles = books.Select(x => x.Title).ToList();
            return Ok(bookTitles);
        }
    }
}
