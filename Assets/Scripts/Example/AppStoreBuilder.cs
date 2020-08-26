using System;
using Example.Counter;
using Example.ToDo;
using Redux;
using static Example.Counter.Filters;
using static Example.Counter.Reducers;
using static Example.ToDo.Filters;
using static Example.ToDo.Reducers;
using static Example.ToDo.SideEffects;

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
                    .AddReducer<CreateTodoItemAction, StoreToDoItemsAction>(CreateToDoItemReducer)
                    .AddReducer<RemoveTodoItemAction>(RemoveToDoItemReducer)
                    .AddReducer<CompleteTodoItemAction>(CompleteToDoItemReducer)
                    .AddReducer<ClearTodoItemsSucceedAction>(ClearToDoItemsReducer)
                .EndSubReducer()
                .AddSideEffect<ClearTodoItemsAction, PlayerPrefsService, ClearTodoItemsSucceedAction>(ClearToDoItemsEffect, playerPrefsService)
                .AddSideEffect<StoreToDoItemsAction, PlayerPrefsService>(StoreToDoItemsEffect, playerPrefsService)
                
                .Build(out disposable);
        }
    }
}