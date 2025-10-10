using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultButtons : MonoBehaviour
{
    public Button playAgainButton;
    public Button returnButton;

    private void Start()
    {
        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(PlayAgain);

        if (returnButton != null)
            returnButton.onClick.AddListener(ReturnToMenu);
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}