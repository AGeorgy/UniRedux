using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Redux
{
    public class Store<TState> : IDispatcherSelector<TState>
    {
        public TState State => _state;

        private readonly Dictionary<Type,object> _reducers;
        private readonly StateProvider<TState> _stateProvider;
        private TState _state;

        public Store(TState initialState, Dictionary<Type, object> reducers)
        {
            _stateProvider = new StateProvider<TState>();
            _state = initialState;
            _reducers = reducers;
        }

        public void Dispatch<TAction>(TAction action)
        {
            if (_reducers.TryGetValue(typeof(TAction), out var reducerObject))
            {
                var reducer = (Action<TState, TAction>) reducerObject;
                var state = CreateDeepCopy(_state);
                reducer.Invoke(state, action);
                _state = state;
                _stateProvider.OnNext(_state);
            }
        }

        public void Dispatch<TAction>()
        {
            Dispatch<TAction>(default);
        }

        public IObservable<TPartialState> Select<TPartialState>(Func<TState, TPartialState> selector)
        {
            return new SelectObservable<TState, TPartialState>(_stateProvider, selector);
        }

        private static T CreateDeepCopy<T>(T obj)
        {
            using var ms = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(ms);
        }
    }
}