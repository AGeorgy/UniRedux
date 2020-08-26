using UnityEngine;

namespace Example
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
    }
}