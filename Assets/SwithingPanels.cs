using UnityEngine;
using System.Collections; // For IEnumerator

public class SwitchingPanelScript : MonoBehaviour
{
    [Header("Main Panels")]
    public GameObject lobbyPanelGO;
    public GameObject modePanelGO;
    public GameObject listOfLobbyPanelGO;

    [Header("Lobby Panels (under Lobbies parent)")]
    public GameObject lobbiesParentGO; // 👈 Parent GameObject that contains custom/inside lobby panels
    public GameObject customLobbyPanelGO;
    public GameObject insideLobbyPanelGO;

    [Header("Battle Panels")]
    public GameObject battleloadingPanelGO;
    public GameObject prebattlePanelGO;

    private void Awake()
    {
        // Ensure Lobby Panel is visible on start
        ShowOnlyPanel(lobbyPanelGO);
    }

    private void Start()
    {
        // Double confirm that the lobby panel is shown first
        ShowLobbyPanel();
    }

    // === Helper method ===
    private void ShowOnlyPanel(GameObject panelToShow)
    {
        // Hide all panels first
        if (lobbyPanelGO != null) lobbyPanelGO.SetActive(false);
        if (modePanelGO != null) modePanelGO.SetActive(false);
        if (listOfLobbyPanelGO != null) listOfLobbyPanelGO.SetActive(false);
        if (customLobbyPanelGO != null) customLobbyPanelGO.SetActive(false);
        if (insideLobbyPanelGO != null) insideLobbyPanelGO.SetActive(false);
        if (battleloadingPanelGO != null) battleloadingPanelGO.SetActive(false);
        if (prebattlePanelGO != null) prebattlePanelGO.SetActive(false);

        // If this panel is under Lobbies, make sure parent is active
        if (panelToShow == customLobbyPanelGO || panelToShow == insideLobbyPanelGO)
        {
            if (lobbiesParentGO != null)
                lobbiesParentGO.SetActive(true);
        }

        // Show the selected one
        panelToShow.SetActive(true);
    }

    // === Public button methods ===
    public void ShowModePanel() => ShowOnlyPanel(modePanelGO);
    public void ShowLobbyPanel() => ShowOnlyPanel(lobbyPanelGO);
    public void ShowListOfLobbyPanel() => ShowOnlyPanel(listOfLobbyPanelGO);
    public void ShowCustomLobbyPanel() => ShowOnlyPanel(customLobbyPanelGO);
    public void ShowInsideLobbyPanel() => ShowOnlyPanel(insideLobbyPanelGO);

    public void ShowBattleloadingPanel()
    {
        ShowOnlyPanel(battleloadingPanelGO);
        StartCoroutine(SwitchToPreBattleAfterDelay(3f));
    }

    public void ShowPreBattlePanel() => ShowOnlyPanel(prebattlePanelGO);

    // === Coroutine ===
    private IEnumerator SwitchToPreBattleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowPreBattlePanel();
    }
}
