using Example.Counter.Scripts;
using Example.Services;
using Example.ToDo.Scripts;
using UniRedux.Redux;
using UnityEngine;

namespace Example
{
    [CreateAssetMenu(fileName = "Initializer", menuName = "_PROJECT/Initializer")]
    public class Main : ScriptableObject
    {
        [SerializeField] private int _tempInt = 9;
        private PlayerPrefsService _playerPrefsService;

        public void Initialize()
        {
            _playerPrefsService = new PlayerPrefsService();
            InitializeRedux();
        }

        private void InitializeRedux()
        {
            var builder = new StoreBuilder();
            CounterBuilder.Build(builder);
            ToDoBuilder.Build(builder, _playerPrefsService);

            var storeProvider = builder.BuildStore();
            GlobalStore.SetStoreProvider(storeProvider);
        }
    }
}