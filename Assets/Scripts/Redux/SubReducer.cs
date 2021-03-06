﻿using System;

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

        public SubReducer<TState, TFilteredState> AddReducer<TInputAction, TService>(Action<TFilteredState, TInputAction, TService> reducer, TService service)
        {
            _storeBuilder.AddReducer(_filter, reducer, service);
            return this;
        }

        public SubReducer<TState, TFilteredState> AddReducer<TInputAction>(Func<TFilteredState, TInputAction, IObservable<TFilteredState>> reducer)
        {
            _storeBuilder.AddReducer(_filter, reducer);
            return this;
        }

        public SubReducer<TState, TFilteredState> AddReducer<TInputAction, TService>(Func<TFilteredState, TInputAction, TService, IObservable<TFilteredState>> reducer, TService service)
        {
            _storeBuilder.AddReducer(_filter, reducer, service);
            return this;
        }

        public StoreBuilder<TState> EndSubReducer()
        {
            return _storeBuilder;
        }
    }
}