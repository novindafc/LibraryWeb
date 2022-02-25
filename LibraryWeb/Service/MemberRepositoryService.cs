using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryWeb.Data;
using LibraryWeb.DTOs;
using LibraryWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ubiety.Dns.Core;

namespace LibraryWeb.Service
{
    public class MemberRepositoryService:IMemberRepositoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MemberRepositoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        
        public async Task<IEnumerable<MemberDto>> GetMember()
        {
            var member = await _context.Member.ToListAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(member);
            
        }
        
        public async Task<MemberDto> GetMemberById(int id)
        {
            var member = await _context.Member.FindAsync(id);
            return _mapper.Map<MemberDto>(member);
            
        }
        
        
        public async Task<bool> EditMember(MemberDto memberDto)
        {
            int id = memberDto.Id;

            var result = _context.Entry(_mapper.Map<Member>(memberDto)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            if (!MemberExists(memberDto.Id))
            {
                return false;
            }
            return true;
        }

        
       
        public async Task<MemberDto> AddMember(MemberDto memberDto)
        {
            _context.Member.Add(_mapper.Map<Member>(memberDto));
            await _context.SaveChangesAsync();
            // var result = CreatedAtAction("AddMember", new { id = member.Id }, member);

            return memberDto;
        }

        
        public async Task<bool> DeleteMember(int id)
        {
            var member = await _context.Member.FindAsync(id);
            _context.Member.Remove(_mapper.Map<Member>(member));
            await _context.SaveChangesAsync();
            if (member == null)
            {
                return false;
            }

            return true;
        }

        public bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.Id == id);
        }
    }
}