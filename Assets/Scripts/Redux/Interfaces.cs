using System;

namespace Redux
{
    public interface IStore<out TState> : IDisposable
    {
        IObservable<TState> ObserveReset();
    }
    
    public interface IReducer<out TState>
    {
        void AddReducer<TFilteredState, TAction>(Func<IObservable<TState>, IObservable<TFilteredState>> filter,
            Action<TFilteredState, TAction> reducer);
        
        void AddReducer<TFilteredState, TInputAction, TOutputAction>(Func<IObservable<TState>, IObservable<TFilteredState>> filter,
            Func<TFilteredState, TInputAction, TOutputAction> reducer);
        
        void AddReducer<TFilteredState, TAction>(Func<IObservable<TState>, IObservable<TFilteredState>> filter,
            Func<TFilteredState, TAction, IObservable<TFilteredState>> reducer);
        
        void AddSideEffect<TService, TInputAction>(Action<TService, TInputAction> sideEffect, TService service);
        
        void AddSideEffect<TService, TInputAction, TOutputAction>(Func<TService, TInputAction, TOutputAction> sideEffect, TService service);
    }
    
    public interface IFilter<out TState>
    {
        IObservable<TResult> Filter<TResult>(Func<IObservable<TState>, IObservable<TResult>> filter);
    }
    
    public interface IDispatcher
    {
        void Dispatch<T>(T action);
    }

    public interface IFilterAndDispatcher<out TState> : IFilter<TState>, IDispatcher
    {
    }

    public interface IReducerFilterAndDispatcher<out TState> : IReducer<TState>, IFilterAndDispatcher<TState>
    {
    }

    public interface IReducerFilterDispatcherAndStore<out TState> : IReducerFilterAndDispatcher<TState>, IStore<TState>
    {
    }
}