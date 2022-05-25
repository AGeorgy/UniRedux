namespace Example.Counter.Scripts
{
    public static class Reducers
    {
        public static void CounterIncrementReducer(AppState state, IncrementAction action)
        {
            state.Counter.Count++;
        }

        public static void CounterDecrementReducer(AppState state, DecrementAction action)
        {
            state.Counter.Count -= action.Value;
        }
    }
}