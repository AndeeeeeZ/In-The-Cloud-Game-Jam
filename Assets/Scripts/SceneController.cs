using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    public void ToNextScene()
    {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single); 
    }

    public void ToScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single); 
    }
}
