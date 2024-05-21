using UnityEngine;
using UnityEngine.UI;

public class SoundValue : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SoundVolume");
    }
    
    public void OnChanged()
    {
        SoundManager.Instance.GetComponent<AudioSource>().volume = GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("SoundVolume", GetComponent<Slider>().value);
    }
}
