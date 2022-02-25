using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryWeb.DTOs;
using LibraryWeb.Models;

namespace LibraryWeb.Service
{
    public interface IBookLogRepositoryService
    {
        public IEnumerable<BookLogDto> GetDataBookLog();
        public Task<IEnumerable<BookLogDto>> GetBookLog();
        public Task<BookLogDto> GetBookLogById(int id);
        public Task<bool> EditBookLog(BookLogDto booklogDto);
        public Task<bool> AddBookLog(BookLogDto booklogDto);
        public Task<bool> DeleteBookLog(int id);
        public bool BookLogExists(int id);
        public Task<BookLog> EmailBookLog(BookLog bookLog);
    }
}