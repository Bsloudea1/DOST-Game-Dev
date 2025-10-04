using UnityEngine;

public class SwitchingPanelScript : MonoBehaviour
{
    // Assign all panels in the Inspector
    public GameObject lobbyPanelGO;
    public GameObject modePanelGO;
    public GameObject listOfLobbyPanelGO;
    public GameObject customLobbyPanelGO;
    public GameObject insideLobbyPanelGO;

    private void Start()
    {
        // Start with only the Lobby panel visible
        ShowOnlyPanel(lobbyPanelGO);
    }

    // Helper method: hide all panels first, then show the target panel
    private void ShowOnlyPanel(GameObject panelToShow)
    {
        lobbyPanelGO.SetActive(false);
        modePanelGO.SetActive(false);
        listOfLobbyPanelGO.SetActive(false);
        customLobbyPanelGO.SetActive(false);
        insideLobbyPanelGO.SetActive(false);

        panelToShow.SetActive(true);
    }

    // Public methods for buttons
    public void ShowModePanel() => ShowOnlyPanel(modePanelGO);
    public void ShowLobbyPanel() => ShowOnlyPanel(lobbyPanelGO);
    public void ShowListOfLobbyPanel() => ShowOnlyPanel(listOfLobbyPanelGO);
    public void ShowCustomLobbyPanel() => ShowOnlyPanel(customLobbyPanelGO);
    public void ShowInsideLobbyPanel() => ShowOnlyPanel(insideLobbyPanelGO);
}
