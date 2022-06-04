using System;
using System.Collections.Generic;
using UniRedux.Redux;
using UnityEngine;
using UnityEngine.UI;
using static Example.ToDo.Scripts.Selectors;

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
        private IDisposable _disposables;
        
        private void Start()
        {
            InitActions();
            InitSelectors();
        }

        private void OnDestroy()
        {
            _addButton.onClick.RemoveAllListeners();
            _toDoInput.onSubmit.RemoveAllListeners();
            _clearButton.onClick.RemoveAllListeners();

            _disposables.Dispose();
        }

        private void InitActions()
        {
            _addButton.onClick.AddListener(OnAddButton);
            _toDoInput.onSubmit.AddListener(OnInputSubmit);
            _clearButton.onClick.AddListener(OnClearButton);
        }
        private void InitSelectors()
        {
            var bag = DisposableBag.CreateBuilder();
            
            GlobalStore.GetStore<ToDoState>().Select(FilterItemAdded).Subscribe(OnItemAdded).AddTo(bag);
            GlobalStore.GetStore<ToDoState>().Select(FilterItemRemoved).Subscribe(OnItemRemoved).AddTo(bag);
            GlobalStore.GetStore<ToDoState>().Select(FilterItemCompleted).Subscribe(OnItemCompleted).AddTo(bag);
            //GlobalStore.GetStore<ToDoState>().Select(FilterItemsCleared).Subscribe(OnItemsCleared).AddTo(bag);
            
            _disposables = bag.Build();
        }

        private void OnItemAdded(TodoItem item)
        {
            var itemView = Instantiate(_toDoItemPrefab, _todoList.content);
            _toDoItemViews.Add(item.Id, itemView);
            //itemView.Model.Value = item;
            /*itemView.OnCompleteAsObservable
                .Subscribe(item => GlobalStore.GetStore<ToDoState>().Dispatch(new CompleteTodoItemAction{Item = item}))
                .AddTo(_disposables);
            itemView.OnRemoveAsObservable.Subscribe(item => Store.Dispatch(new RemoveTodoItemAction {Item = item}))
                .AddTo(_disposables);*/
        }

        private void OnItemRemoved(TodoItem item)
        {
            _toDoItemViews[item.Id].Destroy();
            _toDoItemViews.Remove(item.Id);
        }

        private void OnItemCompleted(TodoItem item)
        {
            var view = _toDoItemViews[item.Id];
            //view.Model.Value = item;
        }

        /*private void OnItemsCleared()
        {
            foreach (var toDoItemView in _toDoItemViews)
            {
                toDoItemView.Value.Destroy();
            }
        }*/

        private void OnAddButton()
        {
            DispatchCreateTodoItem(_toDoInput.text);
        }

        private void OnInputSubmit(string text)
        {
            DispatchCreateTodoItem(text);
        }

        private void OnClearButton()
        {
            GlobalStore.GetStore<ToDoState>().Dispatch(new ClearTodoItemsAction());
        }

        private void DispatchCreateTodoItem(string text)
        {
            if (text != string.Empty)
            {
                _toDoInput.text = "";
                GlobalStore.GetStore<ToDoState>().Dispatch(new CreateTodoItemAction {Content = text});
            }
        }
    }
}