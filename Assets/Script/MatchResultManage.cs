using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MatchResultManager : MonoBehaviour
{
    [Header("UI References")]
    public Image victoryImage;               // Victory image object
    public Image defeatImage;                // Defeat image object
    public Transform teamPanel;              // Parent for player slots
    public GameObject playerSlotPrefab;      // Prefab for player slots

    [Header("MVP Display")]
    public Image mvpImage;                   // MVP image object

    [Header("Buttons")]
    public Button playAgainButton;
    public Button returnButton;

    [Header("Animation Settings")]
    public float slotStagger = 0.12f;

    private void Start()
    {
        if (playAgainButton != null)
            playAgainButton.onClick.AddListener(OnPlayAgain);

        if (returnButton != null)
            returnButton.onClick.AddListener(OnReturnToMenu);

        // Disable all at start
        if (victoryImage != null) victoryImage.gameObject.SetActive(false);
        if (defeatImage != null) defeatImage.gameObject.SetActive(false);
        if (mvpImage != null) mvpImage.gameObject.SetActive(false);
    }

    private void ClearPanel()
    {
        for (int i = teamPanel.childCount - 1; i >= 0; i--)
            Destroy(teamPanel.GetChild(i).gameObject);
    }

    public void DisplayResults(List<PlayerResult> players, bool isVictory)
    {
        // ✅ Show correct result image
        if (victoryImage != null) victoryImage.gameObject.SetActive(isVictory);
        if (defeatImage != null) defeatImage.gameObject.SetActive(!isVictory);

        ClearPanel();

        if (players == null || players.Count == 0)
            return;

        // Find MVP
        PlayerResult mvp = players.OrderByDescending(p => p.score).FirstOrDefault();

        // ✅ Show MVP image
        if (mvpImage != null)
            mvpImage.gameObject.SetActive(true);

        // Create slots
        foreach (PlayerResult p in players)
        {
            GameObject go = Instantiate(playerSlotPrefab, teamPanel);
            PlayerSlotUI slotUI = go.GetComponent<PlayerSlotUI>();

            if (slotUI == null)
                slotUI = go.AddComponent<PlayerSlotUI>();

            slotUI.SetData(p, p == mvp);

            if (slotUI.canvasGroup == null)
            {
                CanvasGroup cg = go.GetComponent<CanvasGroup>();
                if (cg == null) cg = go.AddComponent<CanvasGroup>();
                slotUI.canvasGroup = cg;
            }

            slotUI.canvasGroup.alpha = 0f;
            go.transform.localScale = Vector3.one * 0.95f;
        }

        StartCoroutine(AnimateSlots());
    }

    private IEnumerator AnimateSlots()
    {
        for (int i = 0; i < teamPanel.childCount; i++)
        {
            GameObject child = teamPanel.GetChild(i).gameObject;
            CanvasGroup cg = child.GetComponent<CanvasGroup>();
            StartCoroutine(FadeInAndPop(child.transform, cg, 0.18f));
            yield return new WaitForSeconds(slotStagger);
        }
    }

    private IEnumerator FadeInAndPop(Transform t, CanvasGroup cg, float duration)
    {
        float elapsed = 0f;
        Vector3 startScale = Vector3.one * 0.95f;
        Vector3 targetScale = Vector3.one;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float k = elapsed / duration;

            if (cg != null)
                cg.alpha = Mathf.SmoothStep(0f, 1f, k);

            t.localScale = Vector3.Lerp(startScale, targetScale, Mathf.SmoothStep(0f, 1f, k));
            yield return null;
        }

        if (cg != null) cg.alpha = 1f;
        t.localScale = targetScale;
    }

    private void OnPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}