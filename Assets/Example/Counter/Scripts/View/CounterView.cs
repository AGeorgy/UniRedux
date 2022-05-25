using System;
using Redux;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Example.Counter.Scripts.Selectors;

namespace Example.Counter.Scripts.View
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private int _decrementValue = 2;
        [SerializeField] private TextMeshProUGUI _counterText;
        [SerializeField] private Button _incrementButton;
        [SerializeField] private Button _decrementButton;
        
        private IDisposable _disposable;

        private void Start()
        {
            _disposable = GlobalStore.GetStore<AppState>().Select(CountSelector)
                .Subscribe(count => _counterText.text = count.ToString());

            _incrementButton.onClick.AddListener(OnIncrement);
            _decrementButton.onClick.AddListener(OnDecrement);
        }

        private void OnIncrement() => GlobalStore.GetStore<AppState>().Dispatch<IncrementAction>();

        private void OnDecrement() => GlobalStore.GetStore<AppState>().Dispatch(new DecrementAction{Value = _decrementValue});

        private void OnDestroy()
        {
            _disposable.Dispose();
            
            _incrementButton.onClick.RemoveAllListeners();
            _decrementButton.onClick.RemoveAllListeners();
        }
    }
}