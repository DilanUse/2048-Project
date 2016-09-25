using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlScenes : MonoBehaviour
{
    public void cargarScena( string nomScene )
    {
        SceneManager.LoadScene(nomScene, LoadSceneMode.Single);
    }


    public void cargarOtraScena(string nomScene)
    {
        SceneManager.LoadScene(nomScene, LoadSceneMode.Additive);
    }


    public void quitarScene( string nomScene )
    {
        SceneManager.UnloadScene(nomScene);
    }
}
