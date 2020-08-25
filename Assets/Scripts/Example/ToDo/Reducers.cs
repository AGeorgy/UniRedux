using System;

namespace Example.ToDo
{
    public static class Reducers
    {
        public static void CreateToDoItemReducer(ToDoState state, CreateTodoItemAction action)
        {
            var item = new TodoItem {Content = action.Content, Id = Guid.NewGuid()};
            state.Items.Add(item);
            state.ItemAdded = item;
        }

        public static void RemoveToDoItemReducer(ToDoState state, RemoveTodoItemAction action)
        {
            if (state.Items.Contains(action.Item))
            {
                state.Items.Remove(action.Item);
                state.ItemRemoved = action.Item;
            }
        }

        public static void CompleteToDoItemReducer(ToDoState state, CompleteTodoItemAction action)
        {
            if (state.Items.Contains(action.Item))
            {
                var index = state.Items.FindIndex(x => x.Id == action.Item.Id);
                var item = state.Items[index];
                item.Completed = !item.Completed;
                state.Items[index] = item;
                state.ItemCompleted = item;
            }
        }

        public static void ClearToDoItemsReducer(ToDoState state, ClearTodoItemsAction action)
        {
            state.Items.Clear();
        }
    }
}