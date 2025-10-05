using UnityEngine;
using TMPro; // Needed for TextMeshPro UI components
using UnityEngine.UI; // Needed for Button

public class CustomToLobbyInput : MonoBehaviour
{
    [Header("Input Fields (from CustomLobbyPanel)")]
    public TMP_InputField lobbyNameInput;   // Input for lobby name
    public TMP_InputField passwordInput;    // Input for password

    [Header("Text Display (from InsideLobbyPanel)")]
    public TMP_Text lobbyNameDisplay;       // Where to show lobby name
    public TMP_Text passwordDisplay;        // Where to show password

    [Header("Panels")]
    public GameObject customLobbyPanel;     // Custom Lobby Panel
    public GameObject insideLobbyPanel;     // Inside Lobby Panel

    [Header("Buttons")]
    public Button postCreateBtn;            // Button that triggers the panel switch

    private void Start()
    {
        // Add listener to the button
        postCreateBtn.onClick.AddListener(OnPostCreateClicked);
    }

    private void OnDestroy()
    {
        // Remove listener when object is destroyed (good practice)
        postCreateBtn.onClick.RemoveListener(OnPostCreateClicked);
    }

    // Function called when PostCreateBtn is clicked
    public void OnPostCreateClicked()
    {
        // Get the text from input fields
        string lobbyName = lobbyNameInput.text;
        string password = passwordInput.text;

        // ✅ Debug logs to check if input values are being read
        Debug.Log("Lobby Name Entered: " + lobbyName);
        Debug.Log("Password Entered: " + password);

        // Display the entered lobby name and password
        lobbyNameDisplay.text = "Lobby: " + lobbyName;
        passwordDisplay.text = "Password: " + password;

        // Switch panels
        customLobbyPanel.SetActive(false);
        insideLobbyPanel.SetActive(true);

        // Optional: Clear input fields after switching
        lobbyNameInput.text = "";
        passwordInput.text = "";
    }

}
