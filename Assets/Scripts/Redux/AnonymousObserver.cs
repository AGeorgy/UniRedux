using System;

namespace Redux
{
    internal class AnonymousObserver<T> : IObserver<T>
    {
        private readonly Action<T> _onNext;

        public AnonymousObserver(Action<T> onNext)
        {
            _onNext = onNext;
        }

        public void OnNext(T value)
        {
            _onNext(value);
        }

        public void OnError(Exception error) { }

        public void OnCompleted() { }
    }
}