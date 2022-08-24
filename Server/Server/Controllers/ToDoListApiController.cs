using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Interface;
using Server.Models;
using Server.Models.ViewModels;
using System.Globalization;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListApiController : ControllerBase
    {
        private readonly IMemorandum _memorandum;

        public ToDoListApiController(IMemorandum memorandum)
        {
            _memorandum = memorandum;
        }

        // 讀取代辦事項
        [HttpGet]
        [Route("Todolist/rawdata")]
        public List<TodolistViewModel> GetAllTodolist()
        {
            return _memorandum.GetAllTodolist();
        }

        // 取得欲修改代辦事項資料
        [HttpGet]
        [Route("updateItemData/rawdata/{id}")]
        public TodolistViewModel GetUpdataData(int id)
        {
            return _memorandum.GetUpdataData(id);
        }

        // 新增代辦事項
        [HttpPost]
        [Route("createOne/rawdata")]
        public Message CreateTodoitem(TodolistViewModel todolistViewModel)
        {
            return _memorandum.CreateTodoitem(todolistViewModel);
        }

        // 修改代辦事項
        [HttpPut]
        [Route("updateItem/rawdata")]
        public Message UpdateTodoitem(TodolistViewModel todolistViewModel)
        {
            return _memorandum.UpdateTodoitem(todolistViewModel);
        }
        // 刪除代辦事項
        [HttpDelete]
        [Route("deleteItem/rawdata/{id}")]
        public Message DeleteTodoitem(int id)
        {
            return _memorandum.DeleteTodoitem(id);
        }
    }
}
