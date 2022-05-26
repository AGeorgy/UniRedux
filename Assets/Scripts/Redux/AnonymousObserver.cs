using System;

namespace UniRedux.Redux
{
    internal class AnonymousObserver<T> : IReduxObserver<T>
    {
        private readonly Action<T> _action;

        public AnonymousObserver(Action<T> action)
        {
            _action = action;
        }
        
        public void Invoke(T value)
        {
            ForceInvoke(value);
        }

        public void ForceInvoke(T value)
        {
            _action.Invoke(value);
        }
    }
}