using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWeb.Data;
using LibraryWeb.DTOs;
using LibraryWeb.Models;
using LibraryWeb.Service;

namespace LibraryWeb.Controllers
{
    public class BookLogController : Controller
    {
         private readonly IBookLogRepositoryService _bookLogRepository;
        
        public BookLogController( IBookLogRepositoryService bookLogRepository)
        {
            _bookLogRepository = bookLogRepository ;
        }

        //GET: BookLog
        [HttpGet]
        [Route("[action]")]
        [Route("BookLog/Index")]
        public async Task<IActionResult> Index()
        {
            var booklog = await  _bookLogRepository.GetBookLog();
            return View(booklog);
        }

        // GET: BookLog/Details
        [HttpGet]
        [Route("[action]")]
        [Route("BookLog/Details")]
        public async Task<IActionResult> Details(int id)
        {
            var booklog = await  _bookLogRepository.GetBookLogById(id);
            if ( booklog == null)
            {
                return NotFound();
            }
            return View(booklog);
        }

        // GET: BookLog/Create
        [Route("BookLog/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            // ViewData["BookId"] = new SelectList(BookDto, "Id", "Id");
            // ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id");
            return View();
        }

        // POST: BookLog/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime,BookId,MemberId,Status")] BookLogDto bookLogDto)
        {
            if (ModelState.IsValid)
            {
                var booklog = await  _bookLogRepository.AddBookLog(bookLogDto);
                return RedirectToAction(nameof(Index));
            }
            // ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLog.BookId);
            // ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookLog.MemberId);
            return View(bookLogDto);
        }

        // GET: BookLog/Edit/5
        [Route("BookLog/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var booklog = await  _bookLogRepository.GetBookLogById(id);
            if (booklog  == null)
            {
                return NotFound();
            }
            // ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLog.BookId);
            // ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookLog.MemberId);
            return View(booklog);
        }

        // POST: BookLog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartTime,EndTime,BookId,MemberId,Status,Id")] BookLogDto bookLogDto)
        {
            var booklog = await  _bookLogRepository.EditBookLog(bookLogDto);
            if (ModelState.IsValid)
            {
                if (booklog == false)
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
            return View(bookLogDto);
        }

        
        // POST: BookLog/Delete/5
        [HttpGet]
        [Route("BookLog/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await  _bookLogRepository.GetBookLogById(id);
            if (result == null)
            {
                return NotFound();
            }
            bool booklog = await  _bookLogRepository.DeleteBookLog(id);
            if (booklog == false)
            {
                return NotFound();
            }
            
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("BookLog/Get_AllBookLog")]
        public IActionResult Get_AllBookLog() {  
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var booklog =  _bookLogRepository.GetDataBookLog();
          
            // if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            // {
            //     bookLog = booklog.OrderBy(sortColumn + " " + sortColumnDirection);
            // }
            // if (!string.IsNullOrEmpty(searchValue))
            // {
            //     customerData = customerData.Where(m => m.FirstName.Contains(searchValue) 
            //                                            || m.LastName.Contains(searchValue) 
            //                                            || m.Contact.Contains(searchValue) 
            //                                            || m.Email.Contains(searchValue) );
            // }

        
            recordsTotal = booklog.Count();
            var data = booklog.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(new JsonResult(new
            {
                draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data 
            }));


        }  
      
        public JsonResult GetBookLogById(int id) {  
            var booklog =   _bookLogRepository.GetBookLogById(id);  
            return new JsonResult(booklog);  
            //return Json(Obj.Employees.Find(EmpId), JsonRequestBehavior.AllowGet);  
            
        } 


    }
}
