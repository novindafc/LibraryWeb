using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryWeb.Data;
using LibraryWeb.DTOs;
using LibraryWeb.Models;
using LibraryWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace LibraryWeb.Controllers
{
    public class BookController : Controller
    {
         private readonly IBookRepositoryService _bookRepository;
        
        public BookController( IBookRepositoryService bookRepository)
        {
            _bookRepository = bookRepository ;
        }

        //GET: Book
        [HttpGet]
        [Route("[action]")]
        [Route("Book/Index")]
        public async Task<IActionResult> Index()
        {
            var book = await  _bookRepository.GetBook();
            return View(book);
        }

       

        // GET: Book/Details
        [HttpGet]
        [Route("[action]")]
        [Route("Book/Details")]
        public async Task<IActionResult> Details(int id)
        {
            var book = await  _bookRepository.GetBookById(id);
            if ( book== null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        [Route("Book/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            // ViewData["BookId"] = new SelectList(BookDto, "Id", "Id");
            // ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Author,Position,Qty,Remains")] BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                var book = await  _bookRepository.AddBook(bookDto);
                return RedirectToAction(nameof(Index));
            }
            // ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLog.BookId);
            // ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookLog.MemberId);
            return View(bookDto);
        }

        // GET: Book/Edit/5
        [Route("Book/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await  _bookRepository.GetBookById(id);
            if (book  == null)
            {
                return NotFound();
            }
            // ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLog.BookId);
            // ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookLog.MemberId);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Author,Position,Qty,Remains")] BookDto bookDto)
        {
            var book = await  _bookRepository.EditBook(bookDto);
            if (ModelState.IsValid)
            {
                if (book == false)
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            //ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLog.BookId);
            //ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookLog.MemberId);
            return View(bookDto);
        }

        
        // POST: Book/Delete/5
        [HttpGet]
        [Route("Book/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await  _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            bool result = await  _bookRepository.DeleteBook(id);
            if (result == false)
            {
                return NotFound();
            }
            
            return RedirectToAction("Index");;
        }
        
        public JsonResult Get_AllBook() {  
            var book =   _bookRepository.GetBook();
                
                return new JsonResult(book);  
           
        }  
      
        public JsonResult GetBookById(int id) {  
            var book =   _bookRepository.GetBookById(id);  
            return new JsonResult(book);  
                //return Json(Obj.Employees.Find(EmpId), JsonRequestBehavior.AllowGet);  
            
        } 

    }
}