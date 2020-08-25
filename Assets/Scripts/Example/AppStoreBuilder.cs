using System;
using Example.Counter;
using Example.ToDo;
using Redux;
using static Example.Counter.Filters;
using static Example.Counter.Reducers;
using static Example.ToDo.Filters;
using static Example.ToDo.Reducers;

namespace Example
{
    public static class AppStoreBuilder
    {
        public static IFilterAndDispatcher<AppState> Build(out IDisposable disposable)
        {
            return StoreBuilder<AppState>.Create(AppState.InitialState)
                .StartSubReducer(FilterCounterState) // Counter
                    .AddReducer<IncrementAction>(CounterIncrementReducer)
                    .AddReducer<DecrementAction>(CounterDecrementReducer)
                .EndSubReducer()
            
                .StartSubReducer(FilterToDoState) // ToDo
                    .AddReducer<CreateTodoItemAction>(CreateToDoItemReducer)
                    .AddReducer<RemoveTodoItemAction>(RemoveToDoItemReducer)
                    .AddReducer<CompleteTodoItemAction>(CompleteToDoItemReducer)
                    .AddReducer<ClearTodoItemsAction>(ClearToDoItemsReducer)
                .EndSubReducer()
            
                //.AddEffect<ClearTodoItemsAction, CreateTodoItemAction>(VerifayTransactionEffect, _playerStore)
                .Build(out disposable);
        }
    }
}