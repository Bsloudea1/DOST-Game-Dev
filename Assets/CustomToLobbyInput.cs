using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LobbySetupManager : MonoBehaviour
{
    // UI references for Custom Lobby Panel
    [SerializeField] private TMP_InputField lobbyNameInput;
    [SerializeField] private TMP_InputField lobbyPasswordInput;
    [SerializeField] private GameObject customLobbyPanel;

    // UI references for Inside Lobby Panel
    [SerializeField] private TMP_Text lobbyNameDisplay;
    [SerializeField] private TMP_Text passwordDisplay;
    [SerializeField] private GameObject insideLobbyPanel;

    // Button reference
    [SerializeField] private Button postCreateBtn;

    private void Start()
    {
        // Add listener to the button
        if (postCreateBtn != null)
        {
            postCreateBtn.onClick.AddListener(OnPostCreateClicked);
        }
    }

    private void OnDestroy()
    {
        // Remove listener to prevent potential memory leaks
        if (postCreateBtn != null)
        {
            postCreateBtn.onClick.RemoveListener(OnPostCreateClicked);
        }
    }

    private void OnPostCreateClicked()
    {
        // Get input data
        string lobbyName = lobbyNameInput.text;
        string lobbyPassword = lobbyPasswordInput.text;

        // Optional: Validate input
        if (lobbyName.Length < 4)
        {
            Debug.LogWarning("Lobby name must be at least 4 characters long.");
            return;
        }

        // Update display texts
        if (lobbyNameDisplay != null)
        {
            lobbyNameDisplay.text = "Lobby: " + lobbyName;
        }
        if (passwordDisplay != null)
        {
            passwordDisplay.text = "Password: " + lobbyPassword;
        }

        // Switch panels
        if (customLobbyPanel != null && insideLobbyPanel != null)
        {
            customLobbyPanel.SetActive(false);
            insideLobbyPanel.SetActive(true);
        }
    }
}