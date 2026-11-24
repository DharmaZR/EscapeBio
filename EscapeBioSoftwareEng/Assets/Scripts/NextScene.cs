using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadSceneAsync(1);
     }
}
