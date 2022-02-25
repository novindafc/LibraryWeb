using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.Data;
using LibraryWeb.DTOs;
using LibraryWeb.Models;
using LibraryWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Controllers
{
    public class MemberController : Controller
    {
        // GET
        private readonly IMemberRepositoryService _memberRepository;
        
        public MemberController(IMemberRepositoryService memberRepository)
        {
            _memberRepository = memberRepository ;
        }

        //GET: Member
        [HttpGet]
        [Route("[action]")]
        [Route("Member/Index")]
        public async Task<IActionResult> Index()
        {
            var member = await _memberRepository.GetMember();
            return View(member);
        }

        // GET: Member/Details
        [HttpGet]
        [Route("[action]")]
        [Route("Member/Details")]
        public async Task<IActionResult> Details(int id)
        {
            var member = await _memberRepository.GetMemberById(id);
            if ( member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // GET: Member/Create
        [Route("Member/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            // ViewData["BookId"] = new SelectList(BookDto, "Id", "Id");
            // ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id");
            return View();
        }

        // POST: Member/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Gender, Phone, Occupation, Email")] MemberDto memberDto)
        {
            if (ModelState.IsValid)
            {
                var member = await _memberRepository.AddMember(memberDto);
                return RedirectToAction(nameof(Index));
            }
            // ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLog.BookId);
            // ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookLog.MemberId);
            return View(memberDto);
        }

        // GET: Member/Edit/5
        [Route("Member/Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var member = await _memberRepository.GetMemberById(id);
            if (member == null)
            {
                return NotFound();
            }
            // ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", bookLog.BookId);
            // ViewData["MemberId"] = new SelectList(_context.Member, "Id", "Id", bookLog.MemberId);
            return View(member);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name, Gender, Phone, Occupation, Email, Id")] MemberDto memberDto)
        {
            var member = await _memberRepository.EditMember(memberDto);
            if (ModelState.IsValid)
            {
                if (member == false)
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
            return View(memberDto);
        }

        
        // POST: Member/Delete/5
        [HttpGet]
        [Route("Member/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await  _memberRepository.GetMemberById(id);
            if (book == null)
            {
                return NotFound();
            }
            bool member = await _memberRepository.DeleteMember(id);
            if (member == false)
            {
                return NotFound();
            }
            
            return RedirectToAction("Index");
        }
        public JsonResult Get_AllMember() {  
            var member =   _memberRepository.GetMember();
                
            return new JsonResult(member);  
           
        }  
      
        public JsonResult GetMemberById(int id) {  
            var member =   _memberRepository.GetMemberById(id);  
            return new JsonResult(member);  
            //return Json(Obj.Employees.Find(EmpId), JsonRequestBehavior.AllowGet);  
            
        } 


        
    }
}