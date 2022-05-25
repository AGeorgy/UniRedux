using System;
using System.Collections.Generic;

namespace Redux
{
    public class StateProvider<TState> : IObserver<TState>, IObservable<TState>
    {
        private readonly List<IObserver<TState>> _observers;
        private readonly object _lockObject = new();

        public StateProvider()
        {
            _observers = new List<IObserver<TState>>();
        }

        public void OnCompleted()
        {
            foreach (var observer in _observers)
            {
                observer.OnCompleted();
            }
        }

        public void OnError(Exception error)
        {
            foreach (var observer in _observers)
            {
                observer.OnError(error);
            }
        }

        public void OnNext(TState value)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
        }

        public IDisposable Subscribe(IObserver<TState> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Subscription(this, observer);
        }

        private sealed class Subscription : IDisposable
        {
            private bool _isDisposed;
            private StateProvider<TState> _stateProvider;
            private IObserver<TState> _observer;

            public Subscription(StateProvider<TState> stateProvider, IObserver<TState> observer)
            {
                _stateProvider = stateProvider;
                _observer = observer;
            }

            public void Dispose()
            {
                if (!_isDisposed)
                {
                    _isDisposed = true;
                    lock (_stateProvider._lockObject)
                    {
                        if (_observer != null && _stateProvider._observers.Contains(_observer))
                            _stateProvider._observers.Remove(_observer);
                    }

                    _stateProvider = null;
                    _observer = null;
                }
            }
        }
    }
}