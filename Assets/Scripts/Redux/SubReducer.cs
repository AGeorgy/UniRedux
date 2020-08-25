using System;

namespace Redux
{
    public class SubReducer<TState, TFilteredState> where TState : class
    {
        private readonly Func<IObservable<TState>, IObservable<TFilteredState>> _filter;
        private readonly StoreBuilder<TState> _storeBuilder;

        public SubReducer(StoreBuilder<TState> storeBuilder, Func<IObservable<TState>, IObservable<TFilteredState>> filter)
        {
            _storeBuilder = storeBuilder;
            _filter = filter;
        }

        public SubReducer<TState, TFilteredState> AddReducer<TAction>(Action<TFilteredState, TAction> reducer)
        {
            _storeBuilder.AddReducer(_filter, reducer);
            return this;
        }

        public SubReducer<TState, TFilteredState> AddReducer<TAction>(Func<TFilteredState, TAction, IObservable<TFilteredState>> reducer)
        {
            _storeBuilder.AddReducer(_filter, reducer);
            return this;
        }

        public StoreBuilder<TState> EndSubReducer()
        {
            return _storeBuilder;
        }
    }
}