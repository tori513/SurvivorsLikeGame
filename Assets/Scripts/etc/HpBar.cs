using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    Image image;

    public void SetHpBar(float hp, float maxHp)
    {
        image.fillAmount = hp/maxHp;
    }
}
