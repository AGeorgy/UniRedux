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
                // Counter
                .AddReducer<CounterState, IncrementAction>(FilterCounterState, CounterIncrementReducer)
                .AddReducer<CounterState, DecrementAction>(FilterCounterState, CounterDecrementReducer)
            
                // ToDo
                .AddReducer<ToDoState, CreateTodoItemAction>(FilterToDoState, CreateToDoItemReducer)
                .AddReducer<ToDoState, RemoveTodoItemAction>(FilterToDoState, RemoveToDoItemReducer)
                .AddReducer<ToDoState, CompleteTodoItemAction>(FilterToDoState, CompleteToDoItemReducer)
                .AddReducer<ToDoState, ClearTodoItemsAction>(FilterToDoState, ClearToDoItemsReducer)
            
                //.AddEffect<ClearTodoItemsAction, CreateTodoItemAction>(VerifayTransactionEffect, _playerStore)
                .Build(out disposable);
        }
    }
}