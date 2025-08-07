using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Data;
using MyWebApi.Dtos.Comment;
using MyWebApi.Extensions;
using MyWebApi.Interfaces;
using MyWebApi.Mappers;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/comment")]
    [ApiController]

    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentrepo;
        private readonly IStockRepository _stockrepo;
        private readonly UserManager<AppUser> _usermanager;
        public CommentController(ApplicationDBContext context, ICommentRepository commentrepo, IStockRepository stockrepo, UserManager<AppUser> usermanager)
        {
            _commentrepo = commentrepo;
            _stockrepo = stockrepo;
            _context = context;
            _usermanager = usermanager;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentrepo.GetAllAsync();
            var commentdata = comments.Select(s => s.ToCommentDto());
            return Ok(commentdata);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentrepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{Stockid:int}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int Stockid, CreateCommentRequestDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _stockrepo.StockExists(Stockid))
            {
                return BadRequest("Stock does not exist");
            }

            var username = User.GetUsername();
            var appUser = await _usermanager.FindByNameAsync(username);
            
            var commentModel = commentDto.ToCommentFromCreateDTO(Stockid);
            commentModel.AppUserId = appUser.Id;
            await _commentrepo.CreatedAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentrepo.UpdateAsync(id, updateDto);
            if (comment == null)
            {
                return NotFound("Comment not Found");
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel = await _commentrepo.DeleteAsync(id);
            if (commentModel == null)
            {
                return NotFound("Comment Deleted");
            }
            return Ok(commentModel);
        }
     }
}