
using UnityEngine;

public class ConfigManager
{

    private static ConfigManager instance;
    public static ConfigManager Get()
    {
        if(instance == null)
        {
            instance = new ConfigManager();
        }
        return instance;
    }


    public ConfigManager()
    {
        
    }

    public void Save(string filename, object obj)
    {
        string json = JsonUtility.ToJson(obj);
        
    }

}
