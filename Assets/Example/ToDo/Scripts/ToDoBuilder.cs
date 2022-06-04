using UniRedux.Redux;
using Example.Services;
using static Example.ToDo.Scripts.Reducers;

namespace Example.ToDo.Scripts
{
    public static class ToDoBuilder
    {
        public static void Build(StoreBuilder builder, PlayerPrefsService playerPrefsService)
        {
            builder
                .AddState(ToDoState.InitialState)
                .AddReducer<CreateTodoItemAction, PlayerPrefsService>(CreateToDoItemReducer, playerPrefsService)
                .AddReducer<RemoveTodoItemAction>(RemoveToDoItemReducer)
                .AddReducer<CompleteTodoItemAction>(CompleteToDoItemReducer)
                .AddReducer<ClearTodoItemsAction, PlayerPrefsService>(ClearToDoItemsReducer, playerPrefsService)
                .Build();;
        }
    }
}