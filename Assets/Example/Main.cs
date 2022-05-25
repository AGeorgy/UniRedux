using Example.Counter.Scripts;
using Example.ToDo.Scripts;
using Redux;
using UnityEngine;
using static Example.Counter.Scripts.Reducers;
using static Example.ToDo.Scripts.Reducers;

namespace Example
{
    public static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            var builder = new StoreBuilder();
            builder
                .AddState(AppState.InitialState)
                .AddReducer<IncrementAction>(CounterIncrementReducer)
                .AddReducer<DecrementAction>(CounterDecrementReducer)

                //.AddReducer<CreateTodoItemAction, PlayerPrefsService>(CreateToDoItemReducer, playerPrefsService)
                .AddReducer<RemoveTodoItemAction>(RemoveToDoItemReducer)
                .AddReducer<CompleteTodoItemAction>(CompleteToDoItemReducer)
                //.AddReducer<ClearTodoItemsAction, PlayerPrefsService>(ClearToDoItemsReducer, playerPrefsService)
                .Build();

            var storeProvider = builder.BuildStore();
            GlobalStore.SetStoreProvider(storeProvider);
            
            
            /*return StoreBuilder<AppState>.Create(AppState.InitialState)
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
                
                .Build(out disposable);*/
        }
    }
}