using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Example.ToDo.Scripts.View
{
    public class ToDoItemView : MonoBehaviour
    {
        public ReactiveProperty<TodoItem> Model { get; } = new ReactiveProperty<TodoItem>();
        public IObservable<TodoItem> OnRemoveAsObservable => _removeSubject.AsObservable();
        public IObservable<TodoItem> OnCompleteAsObservable => _completeSubject.AsObservable();
        
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Button _removeButton;
        
        private readonly Subject<TodoItem> _completeSubject = new Subject<TodoItem>();
        private readonly Subject<TodoItem> _removeSubject = new Subject<TodoItem>();
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            Model.Where(_ => gameObject.activeInHierarchy).Select(model => model.Completed).DistinctUntilChanged()
                .Subscribe(isCompleted => _toggle.isOn = isCompleted)
                .AddTo(_disposables);

            Model.Where(_ => gameObject.activeInHierarchy).Select(model => model.Content).DistinctUntilChanged()
                .Subscribe(content => _text.text = content)
                .AddTo(_disposables);

            _removeButton.OnClickAsObservable().Subscribe(_ => _removeSubject.OnNext(Model.Value));
            
            _toggle.OnValueChangedAsObservable().Subscribe(isOn =>
            {
                if(isOn != Model.Value.Completed)
                    _completeSubject.OnNext(Model.Value);
            });
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }
    }
}