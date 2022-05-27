using Example.Counter.Scripts;
using Example.Services;
using Example.ToDo.Scripts;
using UniRedux.Redux;
using UnityEngine;
using static Example.Counter.Scripts.Reducers;
using static Example.ToDo.Scripts.Reducers;

namespace Example
{
    public static class Initializer
    {
        private static PlayerPrefsService _playerPrefsService;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            _playerPrefsService = new PlayerPrefsService();
            InitializeRedux();
        }
        public static void InitializeRedux()
        {
            var builder = new StoreBuilder();
            builder
                .AddState(CounterState.InitialState)
                .AddReducer<IncrementAction>(CounterIncrementReducer1)
                .AddReducer<IncrementAction>(CounterIncrementReducer2)
                .AddReducer<DecrementAction>(CounterDecrementReducer)
                .Build()

                .AddState(ToDoState.InitialState)
                .AddReducer<CreateTodoItemAction, PlayerPrefsService>(CreateToDoItemReducer, _playerPrefsService)
                .AddReducer<RemoveTodoItemAction>(RemoveToDoItemReducer)
                .AddReducer<CompleteTodoItemAction>(CompleteToDoItemReducer)
                .AddReducer<ClearTodoItemsAction, PlayerPrefsService>(ClearToDoItemsReducer, _playerPrefsService)
                .Build();

            var storeProvider = builder.BuildStore();
            GlobalStore.SetStoreProvider(storeProvider);
        }
    }
}