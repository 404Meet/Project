using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebApi.Dtos.Comment;
using MyWebApi.Models;

namespace MyWebApi.Mappers
{
    public static class CommentMappers
    {
        //In Get Mapper, we map Model with DTO's
        //parameter with return type
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                id = commentModel.id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                CreatedBy=commentModel.AppUser.UserName,
                Stockid = commentModel.Stockid
            };
        }

        //In Post Mapper, we map DTO's with Model
        //parameter with return type
        public static Comment ToCommentFromCreateDTO(this CreateCommentRequestDto commentDto, int stockid)
        {
            return new Comment
            {
                Stockid = stockid,
                Title = commentDto.Title,
                Content = commentDto.Content
            };
        }
        
        
    }
}