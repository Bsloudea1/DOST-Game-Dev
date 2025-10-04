using UnityEngine;

public class UIManagerScript : MonoBehaviour
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
        lobbyPanelGO.SetActive(true);
        modePanelGO.SetActive(false);
        listOfLobbyPanelGO.SetActive(false);
        customLobbyPanelGO.SetActive(false);
        insideLobbyPanelGO.SetActive(false);
    }

    // Lobby → Mode
    public void ShowModePanel()
    {
        lobbyPanelGO.SetActive(false);
        modePanelGO.SetActive(true);
    }

    // Mode → Lobby (optional back)
    public void ShowLobbyPanel()
    {
        modePanelGO.SetActive(false);
        lobbyPanelGO.SetActive(true);
    }

    // Mode → List of Lobby
    public void ShowListOfLobbyPanel()
    {
        modePanelGO.SetActive(false);
        listOfLobbyPanelGO.SetActive(true);
    }

    // List of Lobby → Custom Lobby
    public void ShowCustomLobbyPanel()
    {
        listOfLobbyPanelGO.SetActive(false);
        customLobbyPanelGO.SetActive(true);
    }

    // Custom Lobby → Inside Lobby
    public void ShowInsideLobbyPanel()
    {
        customLobbyPanelGO.SetActive(false);
        insideLobbyPanelGO.SetActive(true);
    }

    // Optional: go back to previous panels
    public void ShowCustomLobbyFromInside()
    {
        insideLobbyPanelGO.SetActive(false);
        customLobbyPanelGO.SetActive(true);
    }

    public void ShowListOfLobbyFromCustom()
    {
        customLobbyPanelGO.SetActive(false);
        listOfLobbyPanelGO.SetActive(true);
    }
}
