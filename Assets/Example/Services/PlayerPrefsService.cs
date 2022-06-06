using UnityEngine;

namespace Example.Services
{
    public class PlayerPrefsService
    {
        public void Clear(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                Debug.Log(PlayerPrefs.GetString(key));
                PlayerPrefs.DeleteKey(key);
            }
        }

        public void Store(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        
        public string LoadString(string key)
        {
            return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : null;
        }
    }
}