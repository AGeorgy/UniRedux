namespace Example.ToDo.Scripts
{
    public static class Selectors
    {
        public static TodoItem FilterItemAdded(ToDoState state)
        {
            return state.ItemAdded;
        }
        
        public static TodoItem FilterItemCompleted(ToDoState state)
        {
            return state.ItemCompleted;
        }
        
        public static TodoItem FilterItemRemoved(ToDoState state)
        {
            return state.ItemRemoved;
        }
        
        /*public static IObservable<Unit> FilterItemsCleared(IObservable<AppState> input)
        {
            return FilterToDoState(input)
                .Select(state => state.Items.Count)
                .DistinctUntilChanged()
                .Where(items => items == 0)
                .AsUnitObservable();
        }*/
    }
}