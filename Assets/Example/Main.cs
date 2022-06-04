using Example.Counter.Scripts;
using Example.Services;
using Example.ToDo.Scripts;
using UniRedux.Redux;
using UnityEngine;

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

        private static void InitializeRedux()
        {
            var builder = new StoreBuilder();
            CounterBuilder.Build(builder);
            ToDoBuilder.Build(builder, _playerPrefsService);

            var storeProvider = builder.BuildStore();
            GlobalStore.SetStoreProvider(storeProvider);
        }
    }
}