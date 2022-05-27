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
            private readonly Dictionary<Type, List<object>> _reducers;
            private readonly StoreBuilder _storeBuilder;

            public StateBuilder(StoreBuilder storeBuilder, TState initialState)
            {
                _storeBuilder = storeBuilder;
                _state = initialState;
                _reducers = new Dictionary<Type, List<object>>();
            }

            public IStateBuilder<TState> AddReducer<TAction>(Action<TState, TAction> reducer)
            {
                var key = typeof(TAction);
                if (_reducers.ContainsKey(key))
                {
                    _reducers[key].Add(new AnonymousHandler<TState, TAction>(reducer));
                }
                else
                {
                    _reducers.Add(key, new List<object>{new AnonymousHandler<TState, TAction>(reducer)});
                }
                
                return this;
            }

            public IStateBuilder<TState> AddReducer<TAction, TService>(Action<TState, TAction, TService> reducer, TService service)
            {
                var key = typeof(TAction);
                if (_reducers.ContainsKey(key))
                {
                    _reducers[key].Add(new AnonymousHandler<TState, TAction, TService>(reducer, service));
                }
                else
                {
                    _reducers.Add(key, new List<object>{new AnonymousHandler<TState, TAction, TService>(reducer, service)});
                }
                return this;
            }

            public IStateBuilder<TState> AddReducer<TAction, TService1, TService2>(Action<TState, TAction, TService1, TService2> reducer, TService1 service1, TService2 service2)
            {
                var key = typeof(TAction);
                if (_reducers.ContainsKey(key))
                {
                    _reducers[key].Add(new AnonymousHandler<TState, TAction, TService1, TService2>(reducer, service1, service2));
                }
                else
                {
                    _reducers.Add(key, new List<object>{new AnonymousHandler<TState, TAction, TService1, TService2>(reducer, service1, service2)});
                }
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