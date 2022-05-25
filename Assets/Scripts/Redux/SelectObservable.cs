using System;

namespace Redux
{
    public class SelectObservable<TState, TPartialState> : IObservable<TPartialState>, IObserver<TState>
    {
        private readonly IObservable<TState> _source;
        private readonly Func<TState, TPartialState> _selector;
        private IObserver<TPartialState> _observer;
        private TPartialState _lastState;

        public SelectObservable(IObservable<TState> source, Func<TState, TPartialState> selector)
        {
            _lastState = default;
            _source = source;
            _selector = selector;
        }

        public IDisposable Subscribe(IObserver<TPartialState> observer)
        {
            _observer = observer;
            return _source.Subscribe(this);
        }

        public void OnCompleted()
        {
            _observer?.OnCompleted();
        }

        public void OnError(Exception error)
        {
            _observer?.OnError(error);
        }

        public void OnNext(TState value)
        {
            if(_selector == null || _observer == null) return;
            var state = _selector.Invoke(value);
            if (!Equals(_lastState, state))
            {
                _lastState = state;
                _observer.OnNext(_lastState);
            }
        }
    }
}