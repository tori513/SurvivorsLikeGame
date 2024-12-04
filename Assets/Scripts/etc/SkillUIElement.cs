using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillUIElement
{
    public TextMeshProUGUI text;         
    public TextMeshProUGUI level;  
    
    public Image buttonImage;

    public void Set(string description, Sprite sprite, int level)
    {
        text.text = description;           
        buttonImage.sprite = sprite;            
        this.level.text = level.ToString();     
    }
}
