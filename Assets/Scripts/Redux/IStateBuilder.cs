using System;

namespace Redux
{
    public interface IStateBuilder<TState>
    {
        IStateBuilder<TState> AddReducer<TAction>(Action<TState, TAction> reducer);
        
        StoreBuilder Build();
    }
}