using System;
using System.Collections.Generic;

namespace Example.ToDo
{
    public class ToDoState
    {
        public TodoItem ItemAdded { get; set; }
        public TodoItem ItemRemoved { get; set; }
        public TodoItem ItemCompleted { get; set; }
        public List<TodoItem> Items { get; set; }
        public TodoFilter Filter { get; set; }

        public static ToDoState InitialState => 
            new ToDoState
            {
                Filter = TodoFilter.All,
                Items = new List<TodoItem>(),
                ItemAdded = TodoItem.Empty,
                ItemRemoved = TodoItem.Empty,
                ItemCompleted = TodoItem.Empty
            };
    }
    
    public struct TodoItem
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public bool Completed { get; set; }
        
        
        public static TodoItem Empty => new TodoItem
        {
            Id = Guid.Empty,
            Content = string.Empty
        };

        public override string ToString()
        {
            return Id + " ^ " + Content + " ^ " + Completed;
        }
    }

    public enum TodoFilter
    {
        All,
        Todo,
        Completed
    }
}