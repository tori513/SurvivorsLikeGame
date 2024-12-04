using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    public void SlideVolume()
    {
        AudioListener.volume = slider.value;
    }
}
