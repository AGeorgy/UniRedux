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

        public SubReducer<TState, TFilteredState> AddReducer<TInputAction>(Action<TFilteredState, TInputAction> reducer)
        {
            _storeBuilder.AddReducer(_filter, reducer);
            return this;
        }

        public SubReducer<TState, TFilteredState> AddReducer<TInputAction, TOutputAction>(Func<TFilteredState, TInputAction, TOutputAction> reducer)
        {
            _storeBuilder.AddReducer(_filter, reducer);
            return this;
        }

        public SubReducer<TState, TFilteredState> AddReducer<TInputAction>(Func<TFilteredState, TInputAction, IObservable<TFilteredState>> reducer)
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