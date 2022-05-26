namespace Example.Counter.Scripts
{
    public static class Selectors
    {
        public static int FullCountSelector(CounterState state)
        {
            return state.Counter.Count;
        }
        
        public static Counter CounterSelector(CounterState state)
        {
            return state.Counter;
        }
        
        public static int CountSelector(Counter state)
        {
            return state.Count;
        }
    }
}