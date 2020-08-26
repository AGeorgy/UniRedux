using System;
using Redux;
using UnityEngine;

namespace Example
{
    public class Main : MonoBehaviour
    {
        private static readonly IDisposable _disposable;

        public static readonly IFilterAndDispatcher<AppState> Store = AppStoreBuilder.Build(
            new PlayerPrefsService(),
            out _disposable);

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}