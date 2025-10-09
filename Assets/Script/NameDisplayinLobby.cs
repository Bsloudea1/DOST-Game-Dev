using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject customLobbyPanel;
    public GameObject insideLobbyPanel;

    [Header("Input Fields")]
    public TMP_InputField lobbyNameInput;
    public TMP_InputField lobbyPasswordInput;

    [Header("Display Texts")]
    public TMP_Text lobbyNameDisplay;
    public TMP_Text passwordDisplay;

    [Header("Buttons")]
    public Button postCreateBtn;

    void Start()
    {
        postCreateBtn.onClick.AddListener(OnCreateLobby);
    }

    void OnCreateLobby()
    {
        string lobbyName = lobbyNameInput.text;
        string lobbyPassword = lobbyPasswordInput.text;

        lobbyNameDisplay.text = lobbyName;
        passwordDisplay.text = lobbyPassword;

        customLobbyPanel.SetActive(false);
        insideLobbyPanel.SetActive(true);
    }
}
