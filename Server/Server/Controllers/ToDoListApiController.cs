using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Models.ViewModels;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListApiController : ControllerBase
    {
        private readonly MemorandumContext _dbContext;

        public ToDoListApiController(MemorandumContext dbContext)
        {
            _dbContext = dbContext;
        }

        // 讀取代辦事項
        [HttpGet]
        [Route("Todolist/rawdata")]
        public List<TodolistViewModel> GetAllTodolist()
        {
            var query = _dbContext.Todolists.Select(t => new TodolistViewModel()
            {
                Id = t.Id,
                Subjects = t.Subjects,
                ExpiryDate = t.ExpiryDate,
                Describe = t.Describe,
                TimeLeft = t.TimeLeft,
            }).ToList();
            return query;
        }

        // 新增代辦事項
        [HttpPost]
        [Route("createOne/rawdata")]
        public Message CreateTodoitem(TodolistViewModel todolistViewModel)
        {
            Todolist newTodolist = new Todolist();
            newTodolist.Subjects = todolistViewModel.Subjects;
            newTodolist.Describe = todolistViewModel.Describe;
            newTodolist.ExpiryDate = todolistViewModel.ExpiryDate;
            _dbContext.Add(newTodolist);

            Message message = new Message();
            try
            {
                int num = _dbContext.SaveChanges();
                if (num > 0)
                {
                    message.Code = 200;
                    string date = newTodolist.ExpiryDate?.ToShortDateString();
                    message.msg = $"{date}代辦事項 '{newTodolist.Subjects}' 新增成功!";
                }
            }
            catch (DbUpdateException ex)
            {
                HttpResponse response = this.Response;
                response.StatusCode = 400;
                message.Code = 400;
                message.msg = $"代辦事項 '{newTodolist.Subjects}' 新增失敗!";
            }
            return message;
        }

        // 修改代辦事項
        [HttpPut]
        [Route("updateItem/rawdata")]
        public Message UpdateTodoitem(TodolistViewModel todolistViewModel)
        {
            var query = _dbContext.Todolists.First(t => t.Id == todolistViewModel.Id);
            query.Subjects = todolistViewModel.Subjects;
            query.Describe = todolistViewModel.Describe;
            query.ExpiryDate = todolistViewModel.ExpiryDate;

            Message message = new Message();
            try
            {
                int num = _dbContext.SaveChanges();
                if (num > 0)
                {
                    message.Code = 200;
                    string date = query.ExpiryDate?.ToShortDateString();
                    message.msg = $"{date}代辦事項 '{query.Subjects}' 修改成功!";
                }
            }
            catch (DbUpdateException ex)
            {
                HttpResponse response = this.Response;
                response.StatusCode = 400;
                message.Code = 400;
                message.msg = $"代辦事項 '{query.Subjects}' 修改失敗!";
            }
            return message;
        }
        // 刪除代辦事項
        [HttpDelete]
        [Route("deleteItem/rawdata")]
        public Message DeleteTodoitem(int id)
        {
            var query = _dbContext.Todolists.First(t => t.Id == id);
            _dbContext.Remove(query);

            Message message = new Message();
            try
            {
                int num = _dbContext.SaveChanges();
                if (num > 0)
                {
                    message.Code = 200;
                    string date = query.ExpiryDate?.ToShortDateString();
                    message.msg = $"{date}代辦事項 '{query.Subjects}' 刪除成功!";
                }
            }
            catch (DbUpdateException ex)
            {
                HttpResponse response = this.Response;
                response.StatusCode = 400;
                message.Code = 400;
                message.msg = $"代辦事項 '{query.Subjects}' 刪除失敗!";
            }
            return message;
        }
    }
}
