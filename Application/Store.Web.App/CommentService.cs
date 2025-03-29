using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Store.Data.EF.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace Store.Web.App
{
    
    public class CommentService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommentRepository _commentRepository;

        public CommentService(IBookRepository bookRepository, IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor, ICommentRepository commentRepository)
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
            _commentRepository = commentRepository;
        }

        public CommentModel Map(Comment comment, string UserName)
        {
            return new CommentModel
            {
                Id = comment.Id,
                UserName = UserName,
                Content = comment.Content,
                CreatedDate = comment.CreatedAt,
                LastModifiedDate = comment?.LastModifiedAt,
            };
        }

        public async Task<Comment> AddCommentAsync(int bookId, string content)
        {
            var userManager =  _httpContextAccessor.HttpContext.RequestServices.GetService<UserManager<User>>();
            User user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var userExists = user != null;
            if (!userExists)
                throw new Exception("Користувач не знайдений");

            var comment = new Comment
            {
                Content = content,
                CreatedAt = (DateTime.UtcNow).AddHours(2),
                UserId = user.Id,
                BookId = bookId,
                User = user
            };

            await _commentRepository.AddAsync(comment);
            return comment;
        }

        public async Task DeleteCommentAsync(int id)
        {
            var userManager = _httpContextAccessor.HttpContext.RequestServices.GetService<UserManager<User>>();
            User user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var userExists = user != null;
            if (!userExists)
                throw new Exception("Користувач не знайдений");

            var comment = await _commentRepository.GetAsync(id);
            if (comment.UserId != user.Id)
                throw new Exception("Коментар може видаляти тільки автор");

            await _commentRepository.DeleteAsync(id);
        }

        public async Task<CommentModel> ChangeCommentAsync(int id, string content)
        {
            var userManager = _httpContextAccessor.HttpContext.RequestServices.GetService<UserManager<User>>();
            User user = await userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var userExists = user != null;
            if (!userExists)
                throw new Exception("Користувач не знайдений");

            var comment = await _commentRepository.GetAsync(id);
            if (comment.UserId != user.Id)
                throw new Exception("Коментар може видаляти тільки автор");

            comment.Content = content;
            _commentRepository.UpdateAsync(comment);

            return Map(comment, user.UserName);
        }

        public async Task<List<CommentModel>> GetAllByBookAsync(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null)
            {
                return new List<CommentModel>(); 
            }

            var comments = await _commentRepository.GetAllAsync(bookId);

            comments.Sort((c1, c2) => c2.CreatedAt.CompareTo(c1.CreatedAt));

            var commentsFinal = comments.Select(c => Map(c, c.User.UserName)).ToList();

            return commentsFinal;
        }
    }
}
