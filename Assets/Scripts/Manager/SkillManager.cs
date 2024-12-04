using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    [SerializeField]
    WeaponManager weaponManager;

    [SerializeField]
    SkillUIElement[] skillElements;

    [SerializeField]
    Button[] buttons;

    [SerializeField]
    Sprite[] weaponSprites;

    [SerializeField]
    AudioClip clip;

    bool isFirst = true;

    void OnEnable()
    {
        for (int index = 1; index < 4; index++)
        {
            SetSkillUI((WeaponType)index);
        }
        StartCoroutine(StopTimeRoutine());
    }

    void Start()
    {     
        isFirst = false;
    }

    IEnumerator StopTimeRoutine()
    {
        yield return new WaitForSeconds(0.45f);
        Time.timeScale = 0f;
    }

    void SetSkillUI(WeaponType type)
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

        int level = weaponManager.GetWeaponLevel(type);

        skillElements[index].Set(text, weaponSprites[(int)type], level);

        SetButton(index, type);
    }

    void SetButton(int index, WeaponType type)
    {
        buttons[index].onClick.RemoveAllListeners();
        buttons[index].onClick.AddListener(() => ActiveWeapon(type));
    }

    void ActiveWeapon(WeaponType type)
    {
        CountinueTime();
        ActiveWeaponManager(type);
        UpdateWeaponLevel(type);

        if (!isFirst)
        {
            DamageUp(type);
        }

        SoundManager.Instance.PlaySound(clip, 1f);
        Player.Instance.SelectSkill();
        gameObject.SetActive(false);
        GameManager.Instance.CountinueGame();
    }

    void CountinueTime()
    {
        Time.timeScale = 1f;
    }

    void ActiveWeaponManager(WeaponType type)
    {
        weaponManager.ActiveWeapon(type);
        weaponManager.IncreaseWeaponLevel(type);
    }

    void UpdateWeaponLevel(WeaponType type)
    {
        int level = weaponManager.GetWeaponLevel(type);
        int i = (int)type - 1;
        skillElements[i].level.text = level.ToString();
    }

    void DamageUp(WeaponType type)
    {
        weaponManager.DamageUp(type, 1.2f);
    }
}

public enum WeaponType
{
    Gun,
    Rocket,
    Shield,
    Stick
}
