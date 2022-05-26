using System;

namespace UniRedux.Redux
{
    public interface IStateBuilder<TState>
    {
        IStateBuilder<TState> AddReducer<TAction>(Action<TState, TAction> reducer);
        
        StoreBuilder Build();
    }
}