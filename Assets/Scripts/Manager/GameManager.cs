using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    GameObject fadeImage;

    [SerializeField]
    GameObject hp;

    [SerializeField]
    AudioClip clip;

    public bool isStop {  get; private set; }
    public int count { get; private set; }

    void Start()
    {
        StartCoroutine(SetActiveRoutine());
    }

    IEnumerator SetActiveRoutine()
    {
        yield return new WaitForSeconds(1.7f);
        fadeImage.SetActive(false);
    }

    public void StopGame()
    {
        isStop = true;
    }

    public void CountinueGame()
    {
        isStop = false;
    }

    public void GameOver()
    {
        if (isStop) return;

        StopGame();
        StartCoroutine(StopTimeRoutine());
    }

    IEnumerator StopTimeRoutine()
    {
        hp.SetActive(false);
        SoundManager.Instance.PlaySound(clip, 1f);
        yield return new WaitForSeconds(1f);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator WaitRoutine(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(0.4f);
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
    }

    public void CountEnemy()
    {
        count++;
    }

    public void ReGame()
    {
        StartCoroutine(WaitRoutine(1));
    }

    public void GoToMenu()
    {
        StartCoroutine(WaitRoutine(0));
    }
}
