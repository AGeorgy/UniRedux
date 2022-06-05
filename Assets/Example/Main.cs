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

        public void Initialize()
        {
            InitializeRedux();
        }

        private void InitializeRedux()
        {
            var builder = new StoreBuilder();
            builder
                .AddService(() => new PlayerPrefsService())
                .AddService(() => (CounterSettings) Resources.Load("CounterSettings"));
            
            CounterBuilder.Build(builder);
            ToDoBuilder.Build(builder);

            var storeProvider = builder.BuildStore();
            GlobalStore.SetStoreProvider(storeProvider);
        }
    }
}