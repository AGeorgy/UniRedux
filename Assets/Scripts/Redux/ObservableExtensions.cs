using System;

namespace Redux
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext)
        {
            return source.Subscribe(new AnonymousObserver<T>(onNext));
        }
    }
}