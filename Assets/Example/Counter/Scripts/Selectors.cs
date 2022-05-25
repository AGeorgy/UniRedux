namespace Example.Counter.Scripts
{
    public static class Selectors
    {
        public static int CountSelector(AppState state)
        {
            return state.Counter.Count;
        }
    }
}