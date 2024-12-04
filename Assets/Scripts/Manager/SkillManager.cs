using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    WeaponManager weaponManager;

    [SerializeField]
    Button[] buttons;

    [SerializeField]
    TextMeshProUGUI[] texts;

    [SerializeField]
    TextMeshProUGUI[] levels;

    [SerializeField]
    Image[] buttonImages;

    [SerializeField]
    Sprite[] weaponSprites;

    [SerializeField]
    AudioClip clip;

    bool isFirst = true;

    void Start()
    {     
        isFirst = false;
    }

    IEnumerator StopTimeRoutine()
    {
        yield return new WaitForSeconds(0.45f);
        Time.timeScale = 0f;
    }

    void ShowOptions(WeaponType type)
    {
        int index = (int)type -1;
        string text = "";

        switch (type)
        {
            case WeaponType.Rocket:
                text = "에너지 생성기의 레벨이 증가합니다.";
                break;

            case WeaponType.Shield:
                text  = "에너지 보호막 레벨이 증가합니다.";
                break;

            case WeaponType.Stick:
                text = "에너지 검의 레벨이 증가합니다.";
                break;               
        }      
        texts[index].text = text;
        levels[index].text = weaponManager.GetWeaponLevel(type).ToString();
        buttonImages[index].sprite = weaponSprites[(int)type];

        buttons[index].onClick.RemoveAllListeners();
        buttons[index].onClick.AddListener(() => SetButton(type));
    }

    void SetButton(WeaponType type)
    {
        Time.timeScale = 1f;
        weaponManager.ActiveWeapon(type);
        weaponManager.IncreaseWeaponLevel(type);

        int level = weaponManager.GetWeaponLevel(type);
        int i = (int)type -1 ;
        levels[i].text = level.ToString();

        if (!isFirst)
        {
            weaponManager.DamageUp(type,1.2f);
        }

        SoundManager.Instance.PlaySound(clip, 1f);
        Player.Instance.SelectSkill();
        gameObject.SetActive(false);
        GameManager.Instance.CountinueGame();
    }

    private void OnEnable()
    {
        for (int index = 1; index < 4; index++)
        {
            ShowOptions((WeaponType)index);
        }
        StartCoroutine(StopTimeRoutine());
    }
}

public enum WeaponType
{
    Gun,
    Rocket,
    Shield,
    Stick
}
