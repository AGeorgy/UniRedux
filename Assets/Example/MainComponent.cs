using UnityEngine;

namespace Example
{
    public class MainComponent : MonoBehaviour
    {
        [SerializeField]
        private Main _main;
        
        private void Awake()
        {
            _main.Initialize();
        }
    }
}