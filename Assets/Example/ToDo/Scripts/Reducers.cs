using System;
using System.Linq;
using Example.Services;

namespace Example.ToDo.Scripts
{
    public static class Reducers
    {
        private const string TODO_ITEMS = "ToDO#Items";
        public static void CreateToDoItemReducer(ToDoState state, CreateTodoItemAction action, PlayerPrefsService playerPrefsService)
        {
            var item = new TodoItem {Content = action.Content, Id = Guid.NewGuid()};
            state.Items.Add(item);
            state.ItemAdded = item;
            
            var storedString = state.Items.Aggregate(string.Empty, (current, todoItem) => current + (todoItem + " ~ "));
            playerPrefsService.Store(TODO_ITEMS, storedString);
        }

        public static void RemoveToDoItemReducer(AppState state, RemoveTodoItemAction action)
        {
            if (state.ToDo.Items.Contains(action.Item))
            {
                state.ToDo.Items.Remove(action.Item);
                state.ToDo.ItemRemoved = action.Item;
            }
        }

        public static void CompleteToDoItemReducer(AppState state, CompleteTodoItemAction action)
        {
            if (state.ToDo.Items.Contains(action.Item))
            {
                var index = state.ToDo.Items.FindIndex(x => x.Id == action.Item.Id);
                var item = state.ToDo.Items[index];
                item.Completed = !item.Completed;
                state.ToDo.Items[index] = item;
                state.ToDo.ItemCompleted = item;
            }
        }

        public static void ClearToDoItemsReducer(ToDoState state, ClearTodoItemsAction action, PlayerPrefsService playerPrefsService)
        {
            state.Items.Clear();
            playerPrefsService.Clear(TODO_ITEMS);
        }
    }
}