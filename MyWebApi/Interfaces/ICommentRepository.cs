using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos.Comment;
using MyWebApi.Dtos.Stock;
using MyWebApi.Models;

namespace MyWebApi.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreatedAsync(Comment commentModel);
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateDto);
        Task<Comment> DeleteAsync(int id);
    }
}