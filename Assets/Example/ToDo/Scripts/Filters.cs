using System;
using UniRx;

namespace Example.ToDo.Scripts
{
    public static class Filters
    {
        public static IObservable<ToDoState> FilterToDoState(IObservable<AppState> input)
        {
            return input.Select(state => state.ToDo);
        }
        
        public static IObservable<TodoItem> FilterItemAdded(IObservable<AppState> input)
        {
            return FilterToDoState(input)
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
        }
    }
}