namespace Example.Counter.Scripts
{
    public static class Reducers
    {
        public static void CounterIncrementReducer1(CounterState state, IncrementAction action)
        {
            state.Counter.Count++;
        }
        
        public static void CounterIncrementReducer2(CounterState state, IncrementAction action)
        {
            state.Count++;
        }

        public static void CounterDecrementReducer(CounterState state, DecrementAction action)
        {
            state.Counter.Count -= action.Value;
        }
    }
}