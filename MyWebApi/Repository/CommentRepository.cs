using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Data;
using MyWebApi.Dtos.Comment;
using MyWebApi.Interfaces;
using MyWebApi.Models;

namespace MyWebApi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreatedAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
            //throw new NotImplementedException();
        }

        public async Task<Comment> DeleteAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.id == id);
            if (commentModel == null)
            {
                return null;
            }
            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
            //throw new NotImplementedException();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.Include(a=>a.AppUser).ToListAsync();
            //throw new NotImplementedException();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.Include(a=>a.AppUser).FirstOrDefaultAsync(c=>c.id==id);
            //throw new NotImplementedException();
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateDto)
        {
            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }
            existingComment.Title = updateDto.Title;
            existingComment.Content = updateDto.Content;
            await _context.SaveChangesAsync();
            return existingComment;
            //throw new NotImplementedException();
        }
    }
}