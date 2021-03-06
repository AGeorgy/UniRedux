﻿using System;
using UniRx;

namespace Redux
{
    public sealed class Store<TState> : IReducerFilterDispatcherAndStore<TState>
        where TState : class
    {
        // State
        private readonly TState _state;
        private readonly BehaviorSubject<TState> _filterStateSubject;
        private readonly Subject<TState> _reduceStateSubject;
        private readonly Subject<TState> _resetSubject = new Subject<TState>();
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        
        // Dispatcher
        private readonly IMessageBroker _dispatcher = new MessageBroker();

        public Store(TState initialState)
        {
            _state = initialState;
            _filterStateSubject = new BehaviorSubject<TState>(_state);
            _reduceStateSubject = new Subject<TState>();
        }

        public void Dispatch<T>(T action)
        {
            if (action == null) return;
            _dispatcher.Publish(action);
        }

        public void AddReducer<TFilteredState, TAction>(Func<IObservable<TState>, IObservable<TFilteredState>> filter, Action<TFilteredState, TAction> reducer)
        {
            _dispatcher.Receive<TAction>()
                .Do(_ => _reduceStateSubject.OnNext(_state))
                .Zip(filter(_reduceStateSubject), (lhs, rhs) => new {Action = lhs, FilteredState = rhs})
                .Subscribe(filteredStateAndAction =>
                {
                    reducer(filteredStateAndAction.FilteredState, filteredStateAndAction.Action);
                    _filterStateSubject.OnNext(_state);
                })
                .AddTo(_disposables);
        }

        public void AddReducer<TFilteredState, TAction, TService>(Func<IObservable<TState>, IObservable<TFilteredState>> filter, Action<TFilteredState, TAction, TService> reducer, TService service)
        {
            _dispatcher.Receive<TAction>()
                .Do(_ => _reduceStateSubject.OnNext(_state))
                .Zip(filter(_reduceStateSubject), (lhs, rhs) => new {Action = lhs, FilteredState = rhs})
                .Subscribe(filteredStateAndAction =>
                {
                    reducer(filteredStateAndAction.FilteredState, filteredStateAndAction.Action, service);
                    _filterStateSubject.OnNext(_state);
                })
                .AddTo(_disposables);
        }

        public void AddReducer<TFilteredState, TAction>(Func<IObservable<TState>, IObservable<TFilteredState>> filter, Func<TFilteredState, TAction, IObservable<TFilteredState>> reducer)
        {
            _dispatcher.Receive<TAction>()
                .Do(_ => _reduceStateSubject.OnNext(_state))
                .Zip(filter(_reduceStateSubject), (lhs, rhs) => new {Action = lhs, FilteredState = rhs})
                .SelectMany(filteredStateAndAction => reducer(filteredStateAndAction.FilteredState, filteredStateAndAction.Action))
                .Subscribe(_ =>
                {
                    _filterStateSubject.OnNext(_state);
                })
                .AddTo(_disposables);
        }

        public void AddReducer<TFilteredState, TAction, TService>(Func<IObservable<TState>, IObservable<TFilteredState>> filter, Func<TFilteredState, TAction, TService, IObservable<TFilteredState>> reducer, TService service)
        {
            _dispatcher.Receive<TAction>()
                .Do(_ => _reduceStateSubject.OnNext(_state))
                .Zip(filter(_reduceStateSubject), (lhs, rhs) => new {Action = lhs, FilteredState = rhs})
                .SelectMany(filteredStateAndAction => reducer(filteredStateAndAction.FilteredState, filteredStateAndAction.Action, service))
                .Subscribe(_ =>
                {
                    _filterStateSubject.OnNext(_state);
                })
                .AddTo(_disposables);
        }

        public IObservable<TResult> Filter<TResult>(Func<IObservable<TState>, IObservable<TResult>> filter)
        {
            return filter(_filterStateSubject);
        }

        public IObservable<TState> ObserveReset()
        {
            return _resetSubject;
        }

        public void Dispose()
        {
            _filterStateSubject?.Dispose();
            _reduceStateSubject?.Dispose();
            _resetSubject?.Dispose();
            _disposables?.Dispose();
        }
    }
}