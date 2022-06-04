using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace UniRedux.Redux
{
    public class Store<TState> : IDispatcherSelector<TState>
    {
        private readonly Dictionary<Type, List<object>> _reducers;
        private readonly StateProvider<TState> _stateProvider;
        private TState _state;

        public Store(TState initialState, Dictionary<Type, List<object>> reducers)
        {
            _state = initialState;
            _reducers = reducers;
            _stateProvider = new StateProvider<TState>(_state);
        }

        public void Dispatch<TAction>(TAction action)
        {
            if (_reducers.TryGetValue(typeof(TAction), out var reducerObjects))
            {
                var state = CreateDeepCopy(_state);
                foreach (var reducerObject in reducerObjects)
                {
                    var reducer = (IAnonymousHandler<TState, TAction>) reducerObject;
                    reducer.Handle(state, action);
                }
                _state = state;
                _stateProvider.Invoke(_state);
            }
        }

        public void Dispatch<TAction>()
        {
            Dispatch<TAction>(default);
        }

        public IReduxSelectObservable<TPartialState> Select<TPartialState>(Func<TState, TPartialState> selector)
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
