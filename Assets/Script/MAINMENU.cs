using UnityEngine;
using UnityEngine.SceneManagement;

public class MAINMENU : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Map1");
    }
}
