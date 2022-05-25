using System;

namespace Example.ToDo.Scripts
{
    public static class Selectors
    {
        /*public static TodoItem FilterItemAdded(AppState state)
        {
            return state.ToDo.Items
                .Select(state => state.ItemAdded)
                .DistinctUntilChanged()
                .Where(item => item.Id != Guid.Empty);
        }
        
        public static IObservable<TodoItem> FilterItemCompleted(IObservable<AppState> input)
        {
            return FilterToDoState(input)
                .Select(state => state.ItemCompleted)
                .DistinctUntilChanged()
                .Where(item => item.Id != Guid.Empty);
        }
        
        public static IObservable<TodoItem> FilterItemRemoved(IObservable<AppState> input)
        {
            return FilterToDoState(input)
                .Select(state => state.ItemRemoved)
                .DistinctUntilChanged()
                .Where(item => item.Id != Guid.Empty);
        }
        
        public static IObservable<Unit> FilterItemsCleared(IObservable<AppState> input)
        {
            return FilterToDoState(input)
                .Select(state => state.Items.Count)
                .DistinctUntilChanged()
                .Where(items => items == 0)
                .AsUnitObservable();
        }*/
    }
}