using Microsoft.EntityFrameworkCore;
using Server.Interface;
using Server.Models;
using Server.Models.ViewModels;

namespace Server
{
    public class MemorandumServices : IMemorandum
    {
        private readonly MemorandumContext _dbContext;

        public MemorandumServices(MemorandumContext memorandumContext)
        {
            _dbContext = memorandumContext;
        }
        public List<TodolistViewModel> GetAllTodolist()
        {
            var query = _dbContext.Todolists.Select(t => new TodolistViewModel()
            {
                Id = t.Id,
                Subjects = t.Subjects,
                ExpiryDate = t.ExpiryDate.ToString(),
                Describe = t.Describe,
                TimeLeft = t.TimeLeft,
            }).OrderBy(t => t.ExpiryDate).ToList();
            return query;
        }
        public TodolistViewModel GetUpdataData(int id)
        {
            var query = _dbContext.Todolists.First(t => t.Id == id);
            TodolistViewModel todolistViewModel = new TodolistViewModel();
            todolistViewModel.Id = id;
            todolistViewModel.Subjects = query.Subjects;
            todolistViewModel.ExpiryDate = query.ExpiryDate.ToString();
            todolistViewModel.Describe = query.Describe;
            return todolistViewModel;
        }
        public Message CreateTodoitem(TodolistViewModel todolistViewModel)
        {
            Todolist newTodolist = new Todolist();
            newTodolist.Subjects = todolistViewModel.Subjects;
            newTodolist.Describe = todolistViewModel.Describe;
            newTodolist.ExpiryDate = Convert.ToDateTime(todolistViewModel.ExpiryDate);
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
                message.Code = 400;
                message.msg = $"代辦事項 '{newTodolist.Subjects}' 新增失敗!";
            }
            return message;
        }
        public Message UpdateTodoitem(TodolistViewModel todolistViewModel)
        {
            var query = _dbContext.Todolists.First(t => t.Id == todolistViewModel.Id);
            query.Subjects = todolistViewModel.Subjects;
            query.Describe = todolistViewModel.Describe;
            query.ExpiryDate = Convert.ToDateTime(todolistViewModel.ExpiryDate);

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
                message.Code = 400;
                message.msg = $"代辦事項 '{query.Subjects}' 修改失敗!";
            }
            return message;
        }
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
                message.Code = 400;
                message.msg = $"代辦事項 '{query.Subjects}' 刪除失敗!";
            }
            return message;
        }
    }
}
