using UnityEngine;
using UnityEngine.UI;

public class ExpManager : Singleton<ExpManager>
{
    [SerializeField]
    Exp exp;

    [SerializeField]
    Image image;

    [SerializeField]
    GameObject expPanel;

    [SerializeField]
    Animator expAnimator;

    [SerializeField]
    AudioClip clip;

    [SerializeField]
    float currentScore = 0;

    float maxScore = 70;

    float firstScore = 30f;

    void Start()
    {
        image.fillAmount = 0;
    }

    void Update()
    {
        if (currentScore > firstScore)
        {
            InitScore();
        }
    }

    public void AddScore()
    {
        currentScore += exp.score;
        UpdateFillAmount();

        SoundManager.Instance.PlaySound(clip, 1f);
        expAnimator.SetTrigger("isAdd");
    }

    void InitScore()
    {
        GameManager.Instance.StopGame();
        currentScore = 0;
        UpdateFillAmount();

        expPanel.SetActive(true);
        firstScore = maxScore;
    }

    void UpdateFillAmount()
    {
        image.fillAmount = currentScore / firstScore;
    }
}
