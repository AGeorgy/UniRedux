using System;

namespace Redux
{
    public interface IDispatcherSelector<out TState>
    {
        TState State { get; }
        
        void Dispatch<TAction>(TAction action);
        void Dispatch<TAction>();
        IObservable<TPartialState> Select<TPartialState>(Func<TState, TPartialState> selector);
    }
}