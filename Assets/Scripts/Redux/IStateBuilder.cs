using System;

namespace UniRedux.Redux
{
    public interface IStateBuilder<TState>
    {
        IStateBuilder<TState> AddReducer<TAction>(Action<TState, TAction> reducer);
        IStateBuilder<TState> AddReducer<TAction, TService>(Action<TState, TAction, TService> reducer);
        IStateBuilder<TState> AddReducer<TAction, TService1, TService2>(Action<TState, TAction, TService1, TService2> reducer);
        
        StoreBuilder Build();
    }
}