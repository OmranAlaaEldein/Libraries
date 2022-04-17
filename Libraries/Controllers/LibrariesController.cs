using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Libraries.Models;
using AutoMapper;
using Libraries.Models.ViewModels;

namespace Libraries.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrariesController : ControllerBase
    {
        private readonly LibrariesDBContext _context;
        private readonly IMapper _mapper;

        public LibrariesController(LibrariesDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Libraries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibraryDto>>> GetLibraries(string filter = "",int skip=0,int take=10)
        {
            //filter
            if (!string.IsNullOrEmpty(filter))
            {
                var librariesFilter = await _context.Libraries.
                    Where(x => x.Name.Equals(filter)).Include(x=>x.Books)
                    .Skip(skip).Take(take).ToListAsync(); 
                var resultFilter = _mapper.Map<List<LibraryDto>>(librariesFilter);
                return resultFilter;
            }

            //without filter
            var libraries = await _context.Libraries.Include(x => x.Books)
                                          .Skip(skip).Take(take).ToListAsync(); 
            var result = _mapper.Map<List<LibraryDto>>(libraries);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibraryDto>> GetLibrary(int id)
        {
            var library = await _context.Libraries.FindAsync(id);

            if (library == null)
            {
                return NotFound();
            }

            //map
            var result=_mapper.Map<LibraryDto>(library);

            return result;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibrary(int id, LibraryCreateUpdateDto libraryCreateUpdateDto)
        {
            //valid
            if(!ValidLibrary(libraryCreateUpdateDto, id))
            {
                return BadRequest();
            }
            var Library = _context.Libraries.FindAsync(id).Result;

            //list book to remove
            List<int> removeBook=new List<int>();
            if (Library.Books!=null)
            {
                var oldBook = Library.Books.Select(x => x.Id);
                var newBook = libraryCreateUpdateDto.Books.Select(x => x.Id);
                removeBook = newBook.Except(oldBook).ToList();
            }
            

            //mapper+edit
            Library = _mapper.Map<LibraryCreateUpdateDto,Library>(libraryCreateUpdateDto, Library);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {   
                    throw;  
            }

            //remove deleted book
            var books = _context.Books.Where(x => removeBook.Contains(x.Id));
            if (books != null)
            {
                _context.Books.RemoveRange(books);
            }

            return Ok("success Update");
        }

        
        [HttpPost]
        public async Task<ActionResult<Library>> PostLibrary(LibraryCreateUpdateDto libraryCreateUpdateDto)
        {
            //valid
            ValidLibrary(libraryCreateUpdateDto);
            if (!ValidLibrary(libraryCreateUpdateDto))
            {
                return BadRequest();
            }

            //mapper
            var library = _mapper.Map<Library>(libraryCreateUpdateDto);
            library.StartDay = DateTime.Now;

            //add
            _context.Libraries.Add(library);
            await _context.SaveChangesAsync();

            return Ok("success Added");
        }

        // DELETE: api/Libraries/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Library>> DeleteLibrary(int id)
        {
            var library = await _context.Libraries.FindAsync(id);
            if (library == null)
            {
                return NotFound();
            }

            _context.Libraries.Remove(library);
            await _context.SaveChangesAsync();

            return Ok("success deleted");
        }

        private bool LibraryExists(int id)
        {
            return _context.Libraries.Any(e => e.Id == id);
        }

        private bool ValidLibrary(LibraryCreateUpdateDto library, int id = -1)
        {
            //valid Library
            if (id!=-1 && id != library.Id)
            {
                return false;
            }

            if (id != -1 && !LibraryExists(id))
            {
                return false;
            }

           
            if (string.IsNullOrEmpty(library.Name))
            {
                return false;
            }

            //valid Books
            foreach (var item in library.Books)
            {
                if (string.IsNullOrEmpty(item.Tittle))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
