﻿namespace Example.ToDo
{
    public struct CreateTodoItemAction
    {
        public string Content;
    }
    
    public struct ClearTodoItemsAction { }
    public struct ClearTodoItemsSucceedAction { }

    public struct RemoveTodoItemAction
    {
        public TodoItem Item { get; set; }
    }

    public struct CompleteTodoItemAction
    {
        public TodoItem Item { get; set; }
    }

    public struct StoreToDoItemsAction
    {
        public TodoItem[] Items { get; set; }
    }
    
    /*public struct SetFilterAction
    {
        public TodoFilter Filter { get; set; }
    }

    public struct RevertCompleteTodoItemAction
    {
        public int Id { get; set; }
    }

    public struct UpdateTodoItemAction
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }*/
}