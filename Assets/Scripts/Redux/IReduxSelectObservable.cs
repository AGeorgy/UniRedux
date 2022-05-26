using System;

namespace UniRedux.Redux
{
    public interface IReduxSelectObservable<out TState> : IReduxObservable<TState>
    {
        IReduxSelectObservable<TPartialState> Select<TPartialState>(Func<TState, TPartialState> selector);
    }
}