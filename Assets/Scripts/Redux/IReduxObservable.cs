using System;

namespace UniRedux.Redux
{
    public interface IReduxObservable<out T>
    {
        public IDisposable Subscribe(IReduxObserver<T> value);
    }
}