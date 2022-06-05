using System;
using System.Collections.Generic;

namespace UniRedux.Redux
{
    public class StoreBuilder
    {
        private readonly Dictionary<Type, object> _stores;
        private readonly ServiceProvider _serviceProvider;

        public StoreBuilder()
        {
            _stores = new Dictionary<Type, object>();
            _serviceProvider = new ServiceProvider();
        }

        public StoreBuilder AddService<TService>(Func<TService> serviceConstructor)
        {
            _serviceProvider.Add(serviceConstructor);
            return this;
        }

        public IStateBuilder<TState> AddState<TState>(TState initialState)
        {
            return new StateBuilder<TState>(this, initialState, _serviceProvider);
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
            private readonly ServiceProvider _serviceProvider;

            public StateBuilder(StoreBuilder storeBuilder, TState initialState, ServiceProvider  serviceProvider)
            {
                _storeBuilder = storeBuilder;
                _state = initialState;
                _reducers = new Dictionary<Type, List<object>>();
                _serviceProvider = serviceProvider;
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

            public IStateBuilder<TState> AddReducer<TAction, TService>(Action<TState, TAction, TService> reducer)
            {
                var key = typeof(TAction);
                if (_reducers.ContainsKey(key))
                {
                    _reducers[key].Add(new AnonymousHandler<TState, TAction, TService>(reducer, _serviceProvider));
                }
                else
                {
                    _reducers.Add(key, new List<object>{new AnonymousHandler<TState, TAction, TService>(reducer, _serviceProvider)});
                }
                return this;
            }

            public IStateBuilder<TState> AddReducer<TAction, TService1, TService2>(Action<TState, TAction, TService1, TService2> reducer)
            {
                var key = typeof(TAction);
                if (_reducers.ContainsKey(key))
                {
                    _reducers[key].Add(new AnonymousHandler<TState, TAction, TService1, TService2>(reducer, _serviceProvider));
                }
                else
                {
                    _reducers.Add(key, new List<object>{new AnonymousHandler<TState, TAction, TService1, TService2>(reducer, _serviceProvider)});
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