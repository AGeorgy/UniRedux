using UnityEngine;

namespace Example.Services
{
    [CreateAssetMenu(fileName = "CounterSettings", menuName = "Counter/Settings")]
    public class CounterSettings : ScriptableObject
    {
        public int IncreaseRate = 2;
        public int DecreaseRate = 3;
    }
}