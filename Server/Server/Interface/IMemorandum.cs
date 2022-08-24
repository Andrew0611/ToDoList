using Server.Models;
using Server.Models.ViewModels;

namespace Server.Interface
{
    public interface IMemorandum
    {
        public List<TodolistViewModel> GetAllTodolist();
        public TodolistViewModel GetUpdataData(int id);
        public Message CreateTodoitem(TodolistViewModel todolistViewModel);
        public Message UpdateTodoitem(TodolistViewModel todolistViewModel);
        public Message DeleteTodoitem(int id);
    }
}
