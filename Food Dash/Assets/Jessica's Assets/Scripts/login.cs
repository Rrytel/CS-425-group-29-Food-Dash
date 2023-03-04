using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class VRLoginUI : MonoBehaviourPunCallbacks
{
    // Login UI elements
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Text errorMessage;

    // Other variables
    private string username;
    private string password;

    void Start()
    {
        // Add click listener to the login button
        loginButton.onClick.AddListener(Login);
    }

    void Login()
    {
        // Get the username and password from the input fields
        username = usernameInput.text;
        password = passwordInput.text;

        // Check if the username and password are valid
        if (username == "" || password == "")
        {
            // Show an error message
            errorMessage.text = "Please enter a valid username and password.";
            return;
        }

        // Connect to the Photon Network
        PhotonNetwork.ConnectUsingSettings();

        // Set the player's nickname to the username
        PhotonNetwork.NickName = username;
    }

    public override void OnConnectedToMaster()
    {
        // Log the user in and load the main menu
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        // TODO: Load the main menu scene or start the game
    }
}
/* This script should work, provided that you have set up the Photon Network correctly in your Unity project and imported the necessary libraries.

To use this script, you need to attach it to the game object that contains your login UI elements in the Unity editor. Then, drag and drop the InputField and Button objects from the Unity editor to the public fields in the script. Finally, make sure that the "On Click" event for the login button is set to call the Login() method in the script.

Keep in mind that this script is just an example and may need to be modified to fit your specific needs. For example, you may need to add additional validation checks or modify the login process to work with your own authentication system. You will also need to implement the logic for loading the main menu or starting the game once the user is logged in. */
