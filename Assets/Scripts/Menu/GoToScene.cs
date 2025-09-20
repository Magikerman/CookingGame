using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToScene : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }
}
