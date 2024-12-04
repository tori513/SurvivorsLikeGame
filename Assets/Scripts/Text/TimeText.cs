using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    TextMeshProUGUI text;

    float timer;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60); 
        int seconds = Mathf.FloorToInt(timer % 60);

        text.text = $"{minutes:00}:{seconds:00}";
    }
}
