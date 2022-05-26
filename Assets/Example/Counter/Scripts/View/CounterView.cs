using System;
using TMPro;
using UniRedux.Redux;
using UnityEngine;
using UnityEngine.UI;
using static Example.Counter.Scripts.Selectors;

namespace Example.Counter.Scripts.View
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private int _decrementValue = 2;
        [SerializeField] private TextMeshProUGUI _counterText1;
        [SerializeField] private TextMeshProUGUI _counterText2;
        [SerializeField] private Button _incrementButton;
        [SerializeField] private Button _decrementButton;
        
        private IDisposable _disposable;

        private void Start()
        {
            var bag = DisposableBag.CreateBuilder();
            
            GlobalStore.Select<CounterState, Counter>(CounterSelector).Select(CountSelector)
            //_disposable = GlobalStore.GetStore<CounterState>().Select(CounterSelector).Select(CountSelector)
                //.Select(FullCountSelector)
                .Subscribe(DisplayCount1).AddTo(bag);
            
            GlobalStore.GetStore<CounterState>().Select(FullCountSelector)
                .Subscribe(DisplayCount2).AddTo(bag);
            
            _disposable = bag.Build();
            
            _incrementButton.onClick.AddListener(OnIncrement);
            _decrementButton.onClick.AddListener(OnDecrement);
        }

        private void DisplayCount1(int count) => _counterText1.text = count.ToString();
        private void DisplayCount2(int count) => _counterText2.text = count.ToString();

        private void OnIncrement() => GlobalStore.Dispatch<CounterState, IncrementAction>();

        private void OnDecrement() => GlobalStore.GetStore<CounterState>().Dispatch(new DecrementAction{Value = _decrementValue});

        private void OnDestroy()
        {
            _disposable.Dispose();
            
            _incrementButton.onClick.RemoveAllListeners();
            _decrementButton.onClick.RemoveAllListeners();
        }
    }
}