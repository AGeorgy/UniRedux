using System.Linq;

namespace Example.ToDo
{
    public static class SideEffects
    {
        private const string TODO_ITEMS = "ToDO#Items";
        public static ClearTodoItemsSucceedAction ClearToDoItemsEffect(PlayerPrefsService playerPrefsService, ClearTodoItemsAction action)
        {
            playerPrefsService.Clear(TODO_ITEMS);
            return new ClearTodoItemsSucceedAction();
        }
        
        public static void StoreToDoItemsEffect(PlayerPrefsService playerPrefsService, StoreToDoItemsAction action)
        {
            var storedString = action.Items.Aggregate(string.Empty, (current, todoItem) => current + (todoItem + " ~ "));
            playerPrefsService.Store(TODO_ITEMS, storedString);
        }
    }
}