using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Mainmenu : MonoBehaviour
{
    public TMP_InputField joinText, createText;
    public Button join, create, userNameButton;
    public TMP_InputField nameInput;
    public GameObject mainMenu, inputScreen;
    private List<string> curseWords;

    IEnumerator Start()
    {
        // Download curse words list
        using (WWW www = new WWW("https://raw.githubusercontent.com/nnagle123/badwords.txt/main/badwords.txt"))
        {
            yield return www;
            curseWords = new List<string>(www.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries));
        }

        join.onClick.AddListener(() =>
        {
            LobbyManager._instance.StartRoom(joinText.text);
        });

        create.onClick.AddListener(() =>
        {
            LobbyManager._instance.StartRoom(createText.text);
        });

        userNameButton.onClick.AddListener(() =>
        {
            Enter();
        });

        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("username", string.Empty)))
        {
            mainMenu.gameObject.SetActive(true);
            inputScreen.gameObject.SetActive(false);
        }
    }

    public void Enter()
{
    if (string.IsNullOrEmpty(nameInput.text) || nameInput.text == string.Empty)
        return;

    string inputNameLower = nameInput.text.ToLower(); // Convert input username to lowercase

    // Check for curse words
    foreach (string word in curseWords)
    {
        if (inputNameLower.Contains(word.ToLower())) // Convert curse word to lowercase for comparison
        {
            Debug.Log("Curse word detected.");
            return; // Exit function if curse word detected
        }
    }

    // If no curse words detected, set username
    PlayerPrefs.SetString("username", nameInput.text);
    mainMenu.gameObject.SetActive(true);
    inputScreen.gameObject.SetActive(false);
}
}
