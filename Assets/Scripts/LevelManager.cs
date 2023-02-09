using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 1.5f;
    ScoreKeeper scoreKeeper;
    private void Awake()
    {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene(1);
    }
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadEndMenu()
    {
        StartCoroutine(WaitAndLoad("EndMenu", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
