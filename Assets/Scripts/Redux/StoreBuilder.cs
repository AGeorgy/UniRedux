using System;
using System.Collections.Generic;

namespace UniRedux.Redux
{
    public class StoreBuilder
    {
        private readonly Dictionary<Type, object> _stores;

        public StoreBuilder()
        {
            _stores = new Dictionary<Type, object>();
        }

        public IStateBuilder<TState> AddState<TState>(TState initialState)
        {
            return new StateBuilder<TState>(this, initialState);
        }

        public IStoreProvider BuildStore()
        {
            return new StoreProvider(_stores);
        }

        private void BuildState(Type type, object store)
        {
            _stores.Add(type, store);
        }

        private class StateBuilder<TState> : IStateBuilder<TState>
        {
            private readonly TState _state;
            private readonly Dictionary<Type, object> _reducers;
            private readonly StoreBuilder _storeBuilder;

            public StateBuilder(StoreBuilder storeBuilder, TState initialState)
            {
                _storeBuilder = storeBuilder;
                _state = initialState;
                _reducers = new Dictionary<Type, object>();
            }

            public IStateBuilder<TState> AddReducer<TAction>(Action<TState, TAction> reducer)
            {
                _reducers.Add(typeof(TAction), reducer);
                return this;
            }

            public StoreBuilder Build()
            {
                if (_reducers.Count > 0)
                {
                    _storeBuilder.BuildState(typeof(TState), new Store<TState>(_state, _reducers));
                }

                return _storeBuilder;
            }
        }
        
        private class StoreProvider : IStoreProvider
        {
            private Dictionary<Type, object> _stores;

            public StoreProvider(Dictionary<Type, object> stores)
            {
                _stores = stores;
            }

            public IDispatcherSelector<TStoreState> GetStore<TStoreState>()
            {
                if (_stores.TryGetValue(typeof(TStoreState), out var store))
                {
                    return (Store<TStoreState>) store;
                }

                return null;
            }
        }
    }
}