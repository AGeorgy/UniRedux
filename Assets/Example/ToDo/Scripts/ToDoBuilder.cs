using UniRedux.Redux;
using Example.Services;
using static Example.ToDo.Scripts.Reducers;

namespace Example.ToDo.Scripts
{
    public static class ToDoBuilder
    {
        public static void Build(StoreBuilder builder)
        {
            builder
                .AddState(ToDoState.InitialState)
                .AddReducer<CreateTodoItemAction, PlayerPrefsService>(CreateToDoItemReducer)
                .AddReducer<RemoveTodoItemAction>(RemoveToDoItemReducer)
                .AddReducer<CompleteTodoItemAction>(CompleteToDoItemReducer)
                .AddReducer<ClearTodoItemsAction, PlayerPrefsService>(ClearToDoItemsReducer)
                .Build();;
        }
    }
}