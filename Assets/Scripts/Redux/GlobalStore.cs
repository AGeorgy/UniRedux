using System;

namespace UniRedux.Redux
{
    public static class GlobalStore
    {
        private static IStoreProvider _storeProvider;
        
        public static void SetStoreProvider(IStoreProvider storeProvider)
        {
            _storeProvider = storeProvider;
        }

        public static IDispatcherSelector<TStore> GetStore<TStore>()
        {
            return _storeProvider.GetStore<TStore>();
        }

        public static IReduxSelectObservable<TPartialState> Select<TState, TPartialState>(Func<TState, TPartialState> selector)
        {
            return GetStore<TState>()?.Select(selector);
        }

        public static void Dispatch<TState, TAction>()
        {
            GetStore<TState>()?.Dispatch<TAction>();
        }

        public static void Dispatch<TState, TAction>(TAction action)
        {
            GetStore<TState>()?.Dispatch(action);
        }
    }
}