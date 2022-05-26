using System;

namespace Example.Counter.Scripts
{
    [Serializable]
    public class CounterState
    {
        public int Count { get; set; }
        public Counter Counter { get; set; }

        public static CounterState InitialState => 
            new()
            {
                Count = 1,
                Counter = Counter.InitialState
            };

        private CounterState() { }
    }


    [Serializable]
    public class Counter
    {
        public int Count { get; set; }

        public static Counter InitialState => 
            new()
            {
                Count = 2
            };

        private Counter() { }
    }
}