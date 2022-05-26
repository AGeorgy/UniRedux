namespace Example.Counter.Scripts
{
    public static class Reducers
    {
        public static void CounterIncrementReducer(CounterState state, IncrementAction action)
        {
            state.Counter.Count++;
        }

        public static void CounterDecrementReducer(CounterState state, DecrementAction action)
        {
            state.Counter.Count -= action.Value;
        }
    }
}