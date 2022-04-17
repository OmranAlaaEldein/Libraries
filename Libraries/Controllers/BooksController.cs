using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libraries.Models;
using AutoMapper;
using Libraries.Models.ViewModels;

namespace Libraries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibrariesDBContext _context;
        private readonly IMapper _mapper;

        public BooksController(LibrariesDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(string filter = "", int skip = 0, int take = 10)
        {
            //filter
            if (!string.IsNullOrEmpty(filter))
            {
                var BooksFilter = await _context.Books.
                                        Where(x => x.Tittle.Equals(filter)).Include(x=>x.Author).Include(x => x.Library).
                                        Skip(skip).Take(take).ToListAsync();
                var resultFilter = _mapper.Map<List<BookDto>>(BooksFilter);
                return resultFilter;
            }

            //without filter
            var Books = await _context.Books.Include(x => x.Author).Include(x => x.Library).
                              Skip(skip).Take(take).ToListAsync();
            var result = _mapper.Map<List<BookDto>>(Books);
            return result;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public ActionResult<BookDto> GetBook(int id)
        {
            var book = _context.Books.Include(x => x.Author).Include(x => x.Library).Where(x=>x.Id==id).FirstOrDefault();

            if (book == null)
            {
                return NotFound();
            }

            //map
            var result = _mapper.Map<BookDto>(book);

            return result;
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookCreateUpdateDto bookCreateUpdateDto)
        {
            //valid
            if (!ValidBook(bookCreateUpdateDto, id))
            {
                return BadRequest();
            }

            //old Author
            var Book = _context.Books.Find(id);
            var oldAuthor = Book.Author;

            //mapper+edit
            Book = _mapper.Map<BookCreateUpdateDto, Book>(bookCreateUpdateDto, Book);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            //remove old Author
            if(oldAuthor!=null && oldAuthor.Id!= bookCreateUpdateDto.Author.Id)
                _context.Authors.Remove(oldAuthor);

            return Ok("success update");
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookCreateUpdateDto bookCreateUpdateDto)
        {
            if (!ValidBook(bookCreateUpdateDto))
            {
                return BadRequest();
            }

            //mapper+add
            var book = _mapper.Map<Book>(bookCreateUpdateDto);
            book.AddedTime = DateTime.Now;
            
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return Ok("success add");
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookDto>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok("success remove");
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        private bool ValidBook(BookCreateUpdateDto book, int id = -1)
        {
            //valid book
            if (id != -1 && id != book.Id)
            {
                return false;
            }

            if (id != -1 && !BookExists(id))
            {
                return false;
            }

            if (string.IsNullOrEmpty(book.Tittle))
            {
                return false;
            }

            //valid Author 
            if (string.IsNullOrEmpty(book.Author.Name))
            {
                return false;
            }

            if (string.IsNullOrEmpty(book.Author.LastName))
            {
                return false;
            }

            return true;
        }
    }
}
