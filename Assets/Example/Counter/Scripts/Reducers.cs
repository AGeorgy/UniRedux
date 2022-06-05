using Example.Services;

namespace Example.Counter.Scripts
{
    public static class Reducers
    {
        public static void CounterIncrementReducer1(CounterState state, IncrementAction action, CounterSettings counterSettings)
        {
            state.Counter.Count+= counterSettings.IncreaseRate;
        }
        
        public static void CounterIncrementReducer2(CounterState state, IncrementAction action)
        {
            state.Count++;
        }

        public static void CounterDecrementReducer(CounterState state, DecrementAction action, CounterSettings counterSettings)
        {
            state.Counter.Count -= action.Value + counterSettings.DecreaseRate;
        }
    }
}