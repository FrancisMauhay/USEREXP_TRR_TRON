using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour {

    void Start() {
        SoundManager.Instance.Play("menu", 2);
    }

    public void StartGame() {
        SoundManager.Instance.soundSource.Stop(); // stops bgm before switching to the game scene
        SceneManager.LoadScene("Game Screen");
    }

    public void QuitGame() {
    #if UNITY_STANDALONE
        Application.Quit();
    #endif
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
