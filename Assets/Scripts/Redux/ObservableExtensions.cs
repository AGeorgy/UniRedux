using System;

namespace UniRedux.Redux
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IReduxObservable<T> source, Action<T> onNext)
        {
            return source.Subscribe(new AnonymousObserver<T>(onNext));
        }
    }
}