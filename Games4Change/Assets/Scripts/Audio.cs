using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [SerializeField]
    private Image volumeImage;
    [SerializeField]
    private Image soundImage;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Slider soundSlider;
    [SerializeField]
    private Sprite[] volumeImages;
    [SerializeField]
    private Sprite[] soundImages;

    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        soundSlider.onValueChanged.AddListener(delegate { ChangeSound(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeVolume() 
    {
        if (volumeSlider.value == 0.0001f) 
        {
            volumeImage.sprite = volumeImages[3];
        }
        else if (volumeSlider.value > .75f)
        {
            volumeImage.sprite = volumeImages[0];
        }
        else if (volumeSlider.value > .2f)
        {
            volumeImage.sprite = volumeImages[1];
        }
        else
        {
            volumeImage.sprite = volumeImages[2];

        }
        music.volume = Mathf.Log10(volumeSlider.value * 20);
    }


    public void ChangeSound()
    {
        if (soundSlider.value == 0.0001f)
        {
            soundImage.sprite = soundImages[3];
        }
        else if (soundSlider.value > .75f)
        {
            soundImage.sprite = soundImages[0];
        }
        else if (soundSlider.value > .2f)
        {
            soundImage.sprite = soundImages[1];
        }
        else
        {
            soundImage.sprite = soundImages[2];

        }
        sfx.volume = Mathf.Log10(soundSlider.value * 20);
    }


}
