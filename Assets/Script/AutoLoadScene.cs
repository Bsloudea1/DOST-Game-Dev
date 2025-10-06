using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AutoLoadScene : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneToLoad = "Map1";  // Target scene name
    public float delay = 2f;             // Seconds before loading

    private void OnEnable()
    {
        // Start coroutine when the panel becomes active
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }
}
