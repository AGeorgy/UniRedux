using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static Example.Main;
using static Example.Counter.Filters;

namespace Example.Counter
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private int _decrementValue = 1;
        [SerializeField] private TextMeshProUGUI _counterText;
        [SerializeField] private Button _incrementButton;
        [SerializeField] private Button _decrementButton;

        private void Start()
        {
            Store.Filter(FilterCount)
                .Subscribe(count => _counterText.text = count.ToString())
                .AddTo(this);

            _incrementButton.OnClickAsObservable()
                .Subscribe(_ => Store.Dispatch(new IncrementAction()))
                .AddTo(this);

            _decrementButton.OnClickAsObservable()
                .Subscribe(_ => Store.Dispatch(new DecrementAction {Value = _decrementValue}))
                .AddTo(this);
            
        }
    }
}