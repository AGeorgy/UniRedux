using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static Example.Main;
using static Example.ToDo.Scripts.Filters;

namespace Example.ToDo.Scripts.View
{
    public class ToDoView : MonoBehaviour
    {
        [SerializeField] private InputField _toDoInput;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _clearButton;
        [SerializeField] private ScrollRect _todoList;
        [SerializeField] private ToDoItemView _toDoItemPrefab;
        
        private Dictionary<Guid, ToDoItemView> _toDoItemViews = new Dictionary<Guid, ToDoItemView>();
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        
        private void Start()
        {
            InitActions();
            InitFilters();
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void InitActions()
        {
            // Add
            _addButton.OnClickAsObservable()
                .Select(_ => _toDoInput.text)
                .Merge(_toDoInput.OnEndEditAsObservable()
                    .Where(_ => Input.GetKeyDown(KeyCode.Return))
                )
                .Where(x => x != "")
                .Subscribe(text =>
                {
                    _toDoInput.text = ""; // clear input field
                    Store.Dispatch(new CreateTodoItemAction {Content = text});
                })
                .AddTo(_disposables);
            
            // Clear
            _clearButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    Store.Dispatch(new ClearTodoItemsAction());
                });
        }
        private void InitFilters()
        {
            Store.Filter(FilterItemAdded)
                .Subscribe(itemAdded =>
                {
                    var itemView = Instantiate(_toDoItemPrefab, _todoList.content);
                    _toDoItemViews.Add(itemAdded.Id, itemView);
                    itemView.Model.Value = itemAdded;
                    itemView.OnCompleteAsObservable
                        .Subscribe(item => Store.Dispatch(new CompleteTodoItemAction{Item = item}))
                        .AddTo(_disposables);
                    itemView.OnRemoveAsObservable.Subscribe(item => Store.Dispatch(new RemoveTodoItemAction {Item = item}))
                        .AddTo(_disposables);
                })
                .AddTo(_disposables);
            
            Store.Filter(FilterItemRemoved)
                .Subscribe(item =>
                {
                    _toDoItemViews[item.Id].Destroy();
                    _toDoItemViews.Remove(item.Id);
                })
                .AddTo(_disposables);
            
            Store.Filter(FilterItemsCleared)
                .Subscribe(_ =>
                {
                    foreach (var toDoItemView in _toDoItemViews)
                    {
                        toDoItemView.Value.Destroy();
                    }
                })
                .AddTo(_disposables);

            Store.Filter(FilterItemCompleted)
                .Subscribe(item =>
                {
                    var view = _toDoItemViews[item.Id];
                    view.Model.Value = item;
                })
                .AddTo(_disposables);
        }

    }
}