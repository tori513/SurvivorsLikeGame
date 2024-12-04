using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    TextMeshProUGUI damageText;
    RectTransform rect;

    ObjectPool pool;

    void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
    }

    IEnumerator ReturnRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        pool.ReturnObject(gameObject);
    }

    public void MakeText(float damage, Vector2 enemyPos)
    {
        if (damageText == null) return;
        damageText.text = damage.ToString("F0");
        Vector2 screenPos = Camera.main.WorldToScreenPoint(enemyPos);
        rect.position = screenPos;

        StartCoroutine(ReturnRoutine());
    }

    public void Set(ObjectPool pool)
    {
        this.pool = pool;
    }
}
