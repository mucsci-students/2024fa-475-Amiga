using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameBehavior : MonoBehaviour
{
    public void Quit ()
    {
        print ("start");
        StartCoroutine (QuitCoroutine ());
    }

    private IEnumerator QuitCoroutine ()
    {
        yield return new WaitForSeconds (0.5f);
#if UNITY_EDITOR
print ("editor");
        UnityEditor.EditorApplication.isPlaying = false;
#else
print ("application");
        Application.Quit();
#endif
    }
}
