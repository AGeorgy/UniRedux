using System;

namespace Example.Counter.Scripts
{
    [Serializable]
    public class CounterState
    {
        public int Count { get; set; }

        public static CounterState InitialState => 
            new CounterState
            {
                Count = 0
            };
    }
}