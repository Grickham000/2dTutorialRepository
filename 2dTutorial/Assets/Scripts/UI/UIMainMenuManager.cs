
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuManager : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField] private GameObject mainMenuGameScreen;
    [SerializeField] private AudioClip gameSound;


    private void Awake()
    {
        mainMenuGameScreen.SetActive(true);
    }
    private void Update()
    {
    }
    #region Game options

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();//quits the game only when run from a builded app
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //Only executes when the unity editor is running
        #endif
    }
    #endregion
}
