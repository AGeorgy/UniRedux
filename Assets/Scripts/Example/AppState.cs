using Example.Counter;
using Example.ToDo;

namespace Example
{
    public class AppState
    {
        public CounterState Counter { get; private set; }
        public ToDoState ToDo { get; private set; }
    
        public static AppState InitialState => 
            new AppState
            {
                Counter = CounterState.InitialState,
                ToDo = ToDoState.InitialState
            };
    
        private AppState(){}
    }
}