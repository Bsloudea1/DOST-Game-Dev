using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSlotUI : MonoBehaviour
{
    public Image portrait;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI deathsText;
    public TextMeshProUGUI scoreText;
    public GameObject mvpBadge; // toggle for MVP visual
    public CanvasGroup canvasGroup; // for fade-in animation

    private void Reset()
    {
        // If fields are not wired, try to auto-find by name (useful during prototyping)
        if (portrait == null) portrait = transform.Find("Portrait")?.GetComponent<Image>();
        if (nameText == null) nameText = transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
        if (killsText == null) killsText = transform.Find("KillsText")?.GetComponent<TextMeshProUGUI>();
        if (deathsText == null) deathsText = transform.Find("DeathsText")?.GetComponent<TextMeshProUGUI>();
        if (scoreText == null) scoreText = transform.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        if (mvpBadge == null) mvpBadge = transform.Find("MVPBadge")?.gameObject;
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetData(PlayerResult data, bool isMVP)
    {
        nameText.text = data.playerName;
        killsText.text = data.kills.ToString();
        deathsText.text = data.deaths.ToString();
        scoreText.text = data.score.ToString();
        if (data.portrait != null) portrait.sprite = data.portrait;
        if (mvpBadge != null) mvpBadge.SetActive(isMVP);
    }
}