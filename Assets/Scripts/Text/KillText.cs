using TMPro;
using UnityEngine;

public class KillText : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = GameManager.Instance.count.ToString();
    }
}
