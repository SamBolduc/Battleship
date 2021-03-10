using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Parameters : MonoBehaviour
{

    private bool _waitingForKey = false;
    private string _waitingTag = null;

    private void OnGUI()
    {
        if (!_waitingForKey || _waitingTag == null)
        {
            return;
        }

        Event e = Event.current;
        if (e.isKey)
        {
            HotkeysManager.Get().Set(_waitingTag, e.keyCode);
            _waitingForKey = false;
            _waitingTag = null;
        }
    }

    public void SetHotkey()
    {
        _waitingForKey = true;
        _waitingTag = gameObject.tag;
    }

    public void Apply()
    {
        HotkeysManager.Get().Save();
        SceneManager.LoadScene(0);
    }   

}
