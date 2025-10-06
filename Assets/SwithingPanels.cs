using UnityEngine;
using System.Collections; // Needed for IEnumerator

public class SwitchingPanelScript : MonoBehaviour
{
    [Header("Panels")]
    public GameObject lobbyPanelGO;
    public GameObject modePanelGO;
    public GameObject listOfLobbyPanelGO;
    public GameObject customLobbyPanelGO;
    public GameObject insideLobbyPanelGO;
    public GameObject battleloadingPanelGO;
    public GameObject prebattlePanelGO;

    private void Awake()
    {
        // ✅ Ensure Lobby Panel is always visible when the scene starts
        ShowOnlyPanel(lobbyPanelGO);
    }

    private void Start()
    {
        // Just to double-confirm the lobby panel is the first thing you see
        ShowLobbyPanel();
    }

    // Helper: hide all panels first, then show one
    private void ShowOnlyPanel(GameObject panelToShow)
    {
        // Hide all panels first
        lobbyPanelGO.SetActive(false);
        modePanelGO.SetActive(false);
        listOfLobbyPanelGO.SetActive(false);
        customLobbyPanelGO.SetActive(false);
        insideLobbyPanelGO.SetActive(false);
        battleloadingPanelGO.SetActive(false);
        prebattlePanelGO.SetActive(false);

        // Show the selected one
        panelToShow.SetActive(true);
    }

    // === PUBLIC BUTTON FUNCTIONS ===
    public void ShowModePanel() => ShowOnlyPanel(modePanelGO);
    public void ShowLobbyPanel() => ShowOnlyPanel(lobbyPanelGO);
    public void ShowListOfLobbyPanel() => ShowOnlyPanel(listOfLobbyPanelGO);
    public void ShowCustomLobbyPanel() => ShowOnlyPanel(customLobbyPanelGO);
    public void ShowInsideLobbyPanel() => ShowOnlyPanel(insideLobbyPanelGO);

    public void ShowBattleloadingPanel()
    {
        ShowOnlyPanel(battleloadingPanelGO);
        StartCoroutine(SwitchToPreBattleAfterDelay(3f)); // ⏱ Wait 3 seconds
    }

    public void ShowPreBattlePanel() => ShowOnlyPanel(prebattlePanelGO);

    // === COROUTINE ===
    private IEnumerator SwitchToPreBattleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowPreBattlePanel();
    }
}
