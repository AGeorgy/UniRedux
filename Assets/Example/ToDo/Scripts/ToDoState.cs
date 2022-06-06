using System;
using System.Collections.Generic;

namespace Example.ToDo.Scripts
{
    [Serializable]
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
                ItemCompleted = null,
                ItemRemoved = null,
                ItemAdded = null,
            };

        private ToDoState() { }
    }
    
    [Serializable]
    public class TodoItem
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

        public static explicit operator TodoItem(string s)
        {
            if (string.IsNullOrEmpty(s)) return default;
            
            var props = s.Split(" ^ ");
            return new TodoItem {Id = Guid.Parse(props[0]), Content = props[1], Completed = Convert.ToBoolean(props[2])};
        }
    }

    public enum TodoFilter
    {
        All,
        Todo,
        Completed
    }
}