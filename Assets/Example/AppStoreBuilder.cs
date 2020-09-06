using System;
using Example.Counter;
using Example.Counter.Scripts;
using Example.Services;
using Example.ToDo;
using Example.ToDo.Scripts;
using Redux;
using static Example.Counter.Scripts.Filters;
using static Example.Counter.Scripts.Reducers;
using static Example.ToDo.Scripts.Filters;
using static Example.ToDo.Scripts.Reducers;

namespace Example
{
    public static class AppStoreBuilder
    {
        public static IFilterAndDispatcher<AppState> Build(PlayerPrefsService playerPrefsService, out IDisposable disposable)
        {
            return StoreBuilder<AppState>.Create(AppState.InitialState)
                .StartSubReducer(FilterCounterState) // Counter
                    .AddReducer<IncrementAction>(CounterIncrementReducer)
                    .AddReducer<DecrementAction>(CounterDecrementReducer)
                .EndSubReducer()
            
                .StartSubReducer(FilterToDoState) // ToDo
                    .AddReducer<CreateTodoItemAction, PlayerPrefsService>(CreateToDoItemReducer, playerPrefsService)
                    .AddReducer<RemoveTodoItemAction>(RemoveToDoItemReducer)
                    .AddReducer<CompleteTodoItemAction>(CompleteToDoItemReducer)
                    .AddReducer<ClearTodoItemsAction, PlayerPrefsService>(ClearToDoItemsReducer, playerPrefsService)
                .EndSubReducer()
                
                .Build(out disposable);
        }
    }
}