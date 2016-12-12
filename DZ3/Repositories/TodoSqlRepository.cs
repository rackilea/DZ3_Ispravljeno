using DZ3.Classes;
using DZ3.Exceptions;
using DZ3.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ3.Repositories
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
                throw new ArgumentNullException();
            if (_context.TodoItems.Find(todoItem.Id) != null) throw new DuplicateTodoItemException("Duplicate id!");
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            if (todoId==null || userId==null || _context.TodoItems.Find(todoId)==null) throw new ArgumentNullException();
            if (_context.TodoItems.Find(todoId).UserId != userId) throw new TodoAccessDeniedException("Access denied!");
            return _context.TodoItems.Find(todoId);
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            if (userId == null) throw new ArgumentNullException();
            var list = new List<TodoItem>();
            foreach (TodoItem todoitem in _context.TodoItems.ToList())
            {
                if (todoitem.IsCompleted==false && todoitem.UserId.Equals(userId)) list.Add(todoitem);
            }
            return list;    
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            if (userId == null) throw new ArgumentNullException();
            return _context.TodoItems.OrderByDescending(item => item.DateCreated).Where(item => item.UserId.Equals(userId)).ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            if (userId == null) throw new ArgumentNullException();
            var list = new List<TodoItem>();
            foreach (TodoItem todoitem in _context.TodoItems.ToList())
            {
                if (todoitem.IsCompleted==true && todoitem.UserId.Equals(userId)) list.Add(todoitem);
            }
            return list;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            if (userId == null) throw new ArgumentNullException();
            return _context.TodoItems.Where(filterFunction).Where(item => item.UserId.Equals(userId)).ToList();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            if (todoId == null || userId == null) throw new ArgumentNullException();
            if (_context.TodoItems.Find(todoId).UserId != userId) throw new TodoAccessDeniedException("Access denied!");
            Get(todoId, userId).MarkCompleted();
            _context.SaveChanges();
            return true;
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem todoItem = Get(todoId, userId);
            if (todoId == null || userId == null || _context.TodoItems.Find(todoId) == null) throw new ArgumentNullException();
            if (_context.TodoItems.Find(todoId).UserId != userId) throw new TodoAccessDeniedException("Access denied!");
            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();
            return true;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (todoItem == null || userId == null || _context.TodoItems.Find(todoItem).Id == null) throw new ArgumentNullException();
            if (_context.TodoItems.Find(todoItem).UserId != userId) throw new TodoAccessDeniedException("Access denied!");
            _context.TodoItems.AddOrUpdate(todoItem);
            _context.SaveChanges();
        }
    }
}
