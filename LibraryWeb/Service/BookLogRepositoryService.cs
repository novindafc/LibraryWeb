using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using AutoMapper;
using LibraryWeb.Data;
using LibraryWeb.DTOs;
using LibraryWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibraryWeb.Service
{
    public class BookLogRepositoryService:IBookLogRepositoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BookLogRepositoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

       
        public async Task<IEnumerable<BookLogDto>> GetBookLog()
        {
            var booklog = await _context.BookLog.ToListAsync();
            return _mapper.Map<IEnumerable<BookLogDto>>(booklog);
            
        }

        public IEnumerable<BookLogDto> GetDataBookLog()
        {
            var booklog =  _context.BookLog.ToListAsync();
            return _mapper.Map<IEnumerable<BookLogDto>>(booklog);
            
        }
        
        public async Task<BookLogDto> GetBookLogById(int id)
        {
            var booklog = await _context.BookLog.FindAsync(id);
            return _mapper.Map<BookLogDto>(booklog);

            
        }
        
        public async Task<bool> EditBookLog(BookLogDto booklogDto)
        {
            int id = booklogDto.Id;
            var booklog = _context.Entry(_mapper.Map<BookLog>(booklogDto)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            if (booklog == null)
            {
                return false;
            }
            return true;
        }
        
        
        
        public async Task<bool> AddBookLog(BookLogDto booklogDto)
        {
            bool stock = true;
            var book = await _context.Book.FindAsync(booklogDto.BookId);

            if (book != null || book.Remains != 0)
            {
                book.Remains = book.Remains - 1;
                _context.Entry(_mapper.Map<Book>(book)).State = EntityState.Modified;
                _context.BookLog.Add(_mapper.Map<BookLog>(booklogDto));
                await _context.SaveChangesAsync();
                // var result = CreatedAtAction("AddBookLog", new { id = booklog.Id }, booklog);

            }else if (book != null || book.Remains != 0)
            {
                stock = false;
            }

            return stock;

        }

        
        public async Task<bool> DeleteBookLog(int id)
        {
            var booklog = await _context.BookLog.FindAsync(id);
            _context.BookLog.Remove(_mapper.Map<BookLog>(booklog));
            await _context.SaveChangesAsync();
            if (booklog == null)
            {
                return false;
            }
            return true;

        }

        public bool BookLogExists(int id)
        {
            return _context.BookLog.Any(e => e.Id == id);
        }
        
       
        public async Task<BookLog> EmailBookLog(BookLog booklog)
        {
            var book = await _context.Book.FindAsync(booklog.BookId);
            var member = await _context.Member.FindAsync(booklog.MemberId);
            string format = "yyyy.MM.dd HH:mm:ss:ffff";
            string date = booklog.EndTime.ToString(format, DateTimeFormatInfo.InvariantInfo);
            string body = getHtml(member.Name, book.Title, date);
            Email(body, member.Email);

            _context.BookLog.Remove(booklog);
            await _context.SaveChangesAsync();

            return booklog;
        }

        
          public static string getHtml(string name, string book, string time)
        {
            try
            {
                string messageBody = "<font>Virtual Library Remainder </font><br><br>";
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart =
                    "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTrStart + "Dear, our Member " + name +
                               " ! This is Virtual Library, we would like to inform you that this is your last day for borrowing book. Please return book tomorrow!" +
                               htmlTdEnd;
                messageBody += htmlTrStart + "Time :" + time + ". " + htmlTdEnd;
                messageBody += htmlTrStart + "Member Name : " + name + htmlTdEnd;
                messageBody += htmlTrStart + "Book : " + book + htmlTdEnd;
                messageBody += htmlTrStart + "If you already return the book please ignore this message! " + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;

                messageBody = messageBody + htmlTableEnd;
                return messageBody; 
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static void Email(string htmlString, string toEmail) {  
            try {  
                MailMessage message = new MailMessage();  
                SmtpClient smtp = new SmtpClient();  
                message.From = new MailAddress("libraryvirtual77@gmail.com");  
                message.To.Add(new MailAddress(toEmail));  
                message.Subject = "Virtual Book Reminder";  
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;  
                smtp.Port = 587;  
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;  
                smtp.UseDefaultCredentials = false;  
                smtp.Credentials = new NetworkCredential("libraryvirtual77@gmail.com", "Mom190465");  
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;  
                smtp.Send(message);  
            } catch (Exception) {}  
        }
        
      
    }
}