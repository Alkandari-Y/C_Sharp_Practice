using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllASync();
        Task<Comment> CreateAsync(Comment commentDto);
        Task<Comment?> GetByIdAsync(int id);
    }
}