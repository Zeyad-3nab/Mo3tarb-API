﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mo3tarb.APIs.Controllers;
using Mo3tarb.APIs.Errors;
using Mo3tarb.APIs.PL.DTOs;
using Mo3tarb.APIs.PL.Errors;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Repositries;
using System.Security.Claims;

namespace Mo3tarb.APIs.PL.Controllers
{
    [Authorize]
    public class CommentController : APIBaseController
    {
        private readonly UserManager<AppUser> _UserManager;
        private readonly IMapper _Mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CommentController(UserManager<AppUser> userManager , IMapper mapper , IUnitOfWork unitOfWork)
        {
            _UserManager = userManager;
            _Mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet("GetAllCommentsOfApartment")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllCommentsOfApartment(int ApartmentId) 
        {
            var comments =  await _unitOfWork.commentRepository.GetAllCommentsForApartmentAsync(ApartmentId);
            return Ok(comments);
        }


        [Authorize]
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Comment>> GetCommentById(int Id)
        {
            var comment = await _unitOfWork.commentRepository.GetByIdAsync(Id);
            if(comment is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Comment ith this Id is not found"));

            return Ok(comment);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddComment(CommentDTO commentDTO) 
        {
            if(ModelState.IsValid) 
            {
                var map = _Mapper.Map<Comment>(commentDTO);
                map.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var count =  await _unitOfWork.commentRepository.AddCommentAsync(map);
                if (count > 0) 
                {
                    return Ok(commentDTO);
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Save Comment"));
            }
            return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                     , "a bad Request , You have made"
                     , ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        }


        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateComment(int CommentId , string text) 
        {
            if (ModelState.IsValid) 
            {
                var comment = await _unitOfWork.commentRepository.GetByIdAsync(CommentId);
                if(comment is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Comment with this Id is not found"));

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (comment.UserId == userId) 
                {
                    comment.Text = text;
                    comment.CreatedAt= DateTime.Now;
                    var count = await _unitOfWork.commentRepository.UpdateCommentAsync(comment);
                    if (count > 0)
                    {
                        return Ok(text);
                    }
                    return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Update Comment"));
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Don't have access to Update this comment"));
            }
            return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                     , "a bad Request , You have made"
                     , ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        }


        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteComment(int CommentId)
        {
            if (ModelState.IsValid)
            {
                var comment = await _unitOfWork.commentRepository.GetByIdAsync(CommentId);
                if (comment is null)
                    return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Comment with this Id is not found"));

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (comment.UserId == userId)
                {
                    var count = await _unitOfWork.commentRepository.DeleteCommentAsync(comment);
                    if (count > 0)
                    {
                        return Ok();
                    }
                    return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in Delete Comment"));
                }
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Don't have access to remove this comment"));
            }
            return BadRequest(new ApiValidationResponse(StatusCodes.Status400BadRequest
                     , "a bad Request , You have made"
                     , ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        }
    }
}
