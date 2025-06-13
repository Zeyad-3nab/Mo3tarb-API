using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Mo3tarb.APIs.Controllers;
using Mo3tarb.APIs.Errors;
using Mo3tarb.APIs.PL.DTOs.AccountDTO;
using Mo3tarb.APIs.PL.DTOs.ChatMessagesDTO;
using Mo3tarb.APIs.PL.Helper;
using Mo3tarb.Core.Entites.Identity;
using Mo3tarb.Core.Entities;
using Mo3tarb.Core.Repositries;
using Mo3tarb.Repository.Identity;
using Mo3tarb.Repository.RealTime;
using Mo3tarb.Repository.Repositories;
using System.Security.Claims;

namespace Mo3tarb.APIs.PL.Controllers
{
    public class ChatController:APIBaseController
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ChatController(IHubContext<ChatHub> hubContext , IMapper mapper , IUnitOfWork unitOfWork)
        {
            _hubContext = hubContext;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // Send a message via HTTP API
        [Authorize]
        [HttpPost("send")]
        public async Task<ActionResult> SendMessage([FromBody] SendMessageDTO request)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            if (senderId is null)
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid to get sender Id please sure a sign in "));

            var chatMessage = new ChatMessage   //Mapping
            {
                SenderId = senderId,
                ReceiverId = request.ReceiverId,
                Message = request.Message,
                Timestamp = DateTime.UtcNow,
                IsRead = false,
                MessageType = MessageType.Text
            };
            
            var count = await _unitOfWork.chatRepository.SendMessageAsync(chatMessage);

            if (count > 0) 
            {
                // Send message via SignalR if receiver is online
                await _hubContext.Clients.User(request.ReceiverId)
                    .SendAsync("ReceiveMessage", senderId,request.ReceiverId, request.Message);
                return Ok();
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save message please try again"));

        }

        [Authorize]
        [HttpPost("sendImage")]
        public async Task<ActionResult> SendImage(SendImageDTO request)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            if (senderId is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid to get sender Id please sure a sign in "));

            var chatMessage = new ChatMessage   //Mapping
            {
                SenderId = senderId,
                ReceiverId = request.ReceiverId,
                Timestamp = DateTime.UtcNow,
                IsRead = false,
                MessageType = MessageType.Image
            };
            if (request.Image is not null)
                chatMessage.Message = DocumentSettings.UploadChatImage(request.Image, "Images");

            var count = await _unitOfWork.chatRepository.SendMessageAsync(chatMessage);

            if (count > 0)
            {
                // Send message via SignalR if receiver is online
                await _hubContext.Clients.User(request.ReceiverId)
                    .SendAsync("ReceiveMessage", senderId, chatMessage.Message);
                return Ok();
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save message please try again"));

        }

        [Authorize]
        [HttpPost("sendAudio")]
        public async Task<ActionResult> SendAudio(SendAudioDTO request)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            if (senderId is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid to get sender Id please sure a sign in "));

            var chatMessage = new ChatMessage   //Mapping
            {
                SenderId = senderId,
                ReceiverId = request.ReceiverId,
                Timestamp = DateTime.UtcNow,
                IsRead = false,
                MessageType = MessageType.Audio
            };
            if (request.Image is not null)
                chatMessage.Message = DocumentSettings.Upload(request.Image, "Audios");

            var count = await _unitOfWork.chatRepository.SendMessageAsync(chatMessage);

            if (count > 0)
            {
                // Send message via SignalR if receiver is online
                await _hubContext.Clients.User(request.ReceiverId)
                    .SendAsync("ReceiveMessage", senderId, chatMessage.Message);
                return Ok();
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in save message please try again"));

        }

        [Authorize]
        [HttpGet("GetContactUsers")]
        public async Task<ActionResult<IEnumerable<GetContactUser>>> GetContactUsers()
        {

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UserId is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid to get Users please sure a sign in "));

            var users = await _unitOfWork.chatRepository.GetContactedUsersWithUnreadCountAsync(UserId);
            var result = users.Select(tuple => new GetContactUser
            {
                Id = tuple.User.Id,
                UserName = tuple.User.UserName,
                FirstName = tuple.User.FirstName,
                LastName = tuple.User.LastName,
                Email = tuple.User.Email,
                WhatsappNumber = tuple.User.WhatsappNumber,
                PhoneNumber = tuple.User.PhoneNumber,
                NumOfUnReadMessages = tuple.UnreadCount
            });
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetChat")]
        public async Task<ActionResult<IEnumerable<ReturnChat>>> GetChatHistory(string receiverId)
        {
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(senderId is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid to get sender Id please sure a sign in "));

            await _unitOfWork.chatRepository.MarkMessagesAsReadAsync(senderId, receiverId);

            var messages = await _unitOfWork.chatRepository.GetChatHistoryAsync(receiverId, senderId);
            var map = _mapper.Map<IEnumerable<ReturnChat>>(messages);

            return Ok(map);

        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteMessage(int MessageId) 
        {
            var message = await _unitOfWork.chatRepository.GetMessageAsync(MessageId);
            if (message is null)
                return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Message with this Id is not found"));



            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (senderId != message.SenderId)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Don't have an access to remove this message"));

            var count = await _unitOfWork.chatRepository.DeleteAsync(message);
            if (count > 0) 
            {
                // Send message via SignalR if receiver is online
                await _hubContext.Clients.User(message.ReceiverId)
                    .SendAsync("MessageDeleted", MessageId , message.ReceiverId);
                if (message.MessageType == MessageType.Image)
                {
                    DocumentSettings.Delete(message.Message, "Images");
                    return Ok();
                }
                else if (message.MessageType == MessageType.Audio)
                {
                    DocumentSettings.Delete(message.Message, "Audios");
                    return Ok();
                }
                else 
                {
                    return Ok();
                   
                }
            }
            return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Error in delete please try again"));
        }
    }
}