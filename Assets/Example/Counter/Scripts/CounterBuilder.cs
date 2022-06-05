using Example.Services;
using UniRedux.Redux;
using static Example.Counter.Scripts.Reducers;

namespace Example.Counter.Scripts
{
    public static class CounterBuilder
    {
        public static void Build(StoreBuilder builder)
        {
            builder
                .AddState(CounterState.InitialState)
                .AddReducer<IncrementAction, CounterSettings>(CounterIncrementReducer1)
                .AddReducer<IncrementAction>(CounterIncrementReducer2)
                .AddReducer<DecrementAction, CounterSettings>(CounterDecrementReducer)
                .Build();
        }
    }
}