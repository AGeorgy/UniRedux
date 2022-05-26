using System;

namespace UniRedux.Redux
{
    public interface IDispatcherSelector<out TState>
    {
        void Dispatch<TAction>(TAction action);
        void Dispatch<TAction>();
        IReduxSelectObservable<TPartialState> Select<TPartialState>(Func<TState, TPartialState> selector);
    }
}