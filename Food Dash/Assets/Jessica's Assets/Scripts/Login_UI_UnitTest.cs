using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using Photon.Realtime;
using Photon.Pun;

public class VRLoginUITest
{
    private VRLoginUI loginUI;

    [SetUp]
    public void Setup()
    {
        // Create a new game object and attach the VRLoginUI script to it
        GameObject obj = new GameObject();
        loginUI = obj.AddComponent<VRLoginUI>();

        // Initialize the public fields in the VRLoginUI script
        loginUI.usernameInput = new InputField();
        loginUI.passwordInput = new InputField();
        loginUI.loginButton = new Button();
        loginUI.errorMessage = new Text();
    }

    [Test]
    public void Login_WithValidCredentials_ShouldConnectToPhotonNetworkAndLoadMainMenu()
    {
        // Set up the input fields with valid credentials
        loginUI.usernameInput.text = "testuser";
        loginUI.passwordInput.text = "testpassword";

        // Call the Login method on the VRLoginUI script
        loginUI.Login();

        // Check that the Photon Network is connected
        Assert.IsTrue(PhotonNetwork.IsConnected);

        // Check that the player's nickname is set to the username
        Assert.AreEqual(PhotonNetwork.NickName, "testuser");

        // Check that the main menu is loaded or the game is started
        // TODO: Implement this check once the main menu or game is implemented
    }

    [Test]
    public void Login_WithInvalidCredentials_ShouldShowErrorMessageAndNotConnectToPhotonNetwork()
    {
        // Set up the input fields with invalid credentials
        loginUI.usernameInput.text = "";
        loginUI.passwordInput.text = "";

        // Call the Login method on the VRLoginUI script
        loginUI.Login();

        // Check that the Photon Network is not connected
        Assert.IsFalse(PhotonNetwork.IsConnected);

        // Check that the error message is shown
        Assert.AreEqual(loginUI.errorMessage.text, "Please enter a valid username and password.");
    }
}
