using AutoMapper;
using LibraryWeb.DTOs;
using LibraryWeb.Models;

namespace LibraryWeb.Profiles
{
    public class AutoMappingProfile:Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<BookLogDto, BookLog>().ReverseMap();
            CreateMap<MemberDto, Member>().ReverseMap();
        }
    }

   
}