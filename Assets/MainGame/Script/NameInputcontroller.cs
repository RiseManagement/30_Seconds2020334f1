using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NameInputcontroller : MonoBehaviour
{
    public static string player_1name;
    public static string player_2name;
    public GameObject player_1obj;
    public GameObject player_2obj;

    void Start()
    {
        if (player_1obj == null)
        {
            Debug.LogError("player_1obj is not assigned. Please assign it in the Inspector.");
        }

        if (player_2obj == null)
        {
            Debug.LogError("player_2obj is not assigned. Please assign it in the Inspector.");
        }
    }

    public void SetInputPlayerName()
    {
        if (player_1obj == null || player_2obj == null)
        {
            Debug.LogError("Player objects are not assigned. Skipping name setting.");
            return;
        }

        Text player1Text = player_1obj.GetComponentInChildren<Text>();
        Text player2Text = player_2obj.GetComponentInChildren<Text>();

        if (player1Text != null)
        {
            player_1name = string.IsNullOrEmpty(player1Text.text) ? "Player 1" : player1Text.text;
        }
        else
        {
            Debug.LogError("Text component not found in player_1obj.");
        }

        if (player2Text != null)
        {
            player_2name = string.IsNullOrEmpty(player2Text.text) ? "Player 2" : player2Text.text;
        }
        else
        {
            Debug.LogError("Text component not found in player_2obj.");
        }
    }

    public void LoadSceneMainGame()
    {
        SetInputPlayerName();

        if (string.IsNullOrEmpty(player_1name) || string.IsNullOrEmpty(player_2name))
        {
            Debug.LogWarning("Player names are not properly set. Default names will be used.");
        }

        try
        {
            SceneTransitions.SceneLaod(SceneTransitions.SceneName.MAINGAMEFIRST);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to load scene: " + ex.Message);
        }

        // Alternatively, use Unity's built-in SceneManager
        // UnityEngine.SceneManagement.SceneManager.LoadScene("maingamefirst");
    }
}
