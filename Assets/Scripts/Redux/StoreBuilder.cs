using System;

namespace Redux
{
    public class StoreBuilder<TState> where TState : class
    {
        private readonly IReducerFilterDispatcherAndStore<TState> _store;

        public static StoreBuilder<TState> Create(TState initialState)
        {
            return new StoreBuilder<TState>(new Store<TState>(initialState));
        }

        private StoreBuilder(IReducerFilterDispatcherAndStore<TState> initialState)
        {
            _store = initialState;
        }

        public SubReducer<TState, TFilteredState> StartSubReducer<TFilteredState>(Func<IObservable<TState>, IObservable<TFilteredState>> filter)
        {
            return new SubReducer<TState, TFilteredState>(this, filter);
        }

        public StoreBuilder<TState> AddReducer<TFilteredState, TAction>(
            Func<IObservable<TState>, IObservable<TFilteredState>> filter,
            Action<TFilteredState, TAction> reducer)
        {
            _store.AddReducer(filter, reducer);
            return this;
        }

        public StoreBuilder<TState> AddReducer<TFilteredState, TAction>(
            Func<IObservable<TState>, IObservable<TFilteredState>> filter,
            Func<TFilteredState, TAction, IObservable<TFilteredState>> reducer)
        {
            _store.AddReducer(filter, reducer);
            return this;
        }

        public IFilterAndDispatcher<TState> Build(out IDisposable disposable)
        {
            _store.Dispatch(new InitializeStoreAction());
            disposable = _store;
            return _store;
        }
    }
}