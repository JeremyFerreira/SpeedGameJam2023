using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public void LoadScene (int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene (index);
    }
    public void Quit()
    {
        Application.Quit ();
    }
}
