using System;
using UniRx;

namespace Example.Counter
{
    public static class Filters
    {
        public static IObservable<CounterState> FilterCounterState(IObservable<AppState> input)
        {
            return input.Select(state => state.Counter);
        }
        
        public static IObservable<int> FilterCount(IObservable<AppState> input)
        {
            return FilterCounterState(input)
                .Select(state => state.Count)
                .DistinctUntilChanged();
        }
    }
}