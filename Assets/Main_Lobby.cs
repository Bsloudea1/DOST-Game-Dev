using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject lobbyPanel;
    public GameObject modePanel;

    // Call this to switch from Lobby to Mode
    public void ShowMode_Panel()
    {
        lobbyPanel.SetActive(false);  // hide Lobby_Panel
        modePanel.SetActive(true);    // show Mode_Panel
    }

    // Optional: Call this to go back to Lobby
    public void ShowLobby_Panel()
    {
        modePanel.SetActive(false);   // hide Mode_Panel
        lobbyPanel.SetActive(true);   // show Lobby_Panel
    }

    // Optional: Hide Mode_Panel at start
    private void Start()
    {
        modePanel.SetActive(false);   // ensure Mode_Panel is hidden initially
        lobbyPanel.SetActive(true);   // ensure Lobby_Panel is visible initially
    }
}
