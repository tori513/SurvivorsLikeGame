using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneController : MonoBehaviour
{
    [SerializeField]
    Image fadePanel;

    [SerializeField]
    GameObject option;

    public void ClickStartButton()
    {
        StartCoroutine(FadeRoutine());
    }

    public void ClickOptionButton()
    {
        option.SetActive(true);
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void CloseOptionPanel()
    {
        option.SetActive(false);
    }

    void NextScene()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator FadeRoutine()
    {
        fadePanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        NextScene();
    }
}
