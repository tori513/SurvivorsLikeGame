using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI skillLevelTxt;

    [SerializeField]
    Image coolImage;

    public void Set(float timer, float coolTime, int skillLevel)
    {
        skillLevelTxt.text = skillLevel.ToString("F0");
        coolImage.fillAmount = coolTime / timer;
    }
}
