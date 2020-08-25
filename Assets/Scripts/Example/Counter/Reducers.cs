using System;
using UniRx;

namespace Example.Counter
{
    public static class Reducers
    {
        public static void CounterIncrementReducer(CounterState state, IncrementAction action)
        {
            state.Count++;
        }

        public static IObservable<CounterState> CounterDecrementReducer(CounterState input, DecrementAction action)
        {
            return Observable.Return(input)
                .Do(state => { state.Count -= action.Value; });
        }
    }
}