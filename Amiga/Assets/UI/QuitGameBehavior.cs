using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameBehavior : MonoBehaviour
{
    public void Quit ()
    {
        StartCoroutine (QuitCoroutine ());
    }

    private IEnumerator QuitCoroutine ()
    {
        yield return new WaitForSecondsRealtime (0.5f);
        Application.Quit();
    }
}
