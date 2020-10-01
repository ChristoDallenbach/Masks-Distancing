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
    private PlayerControls playerControls;

    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource sadMusic;
    [SerializeField]
    private AudioSource neutralMusic;
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
        if (playerControls.Mood > 7)
        {
            if (!music.isPlaying)
            {
                sadMusic.Stop();
                neutralMusic.Stop();
                music.Play();
            }
        }
        else if (playerControls.Mood > 4) 
        {
            if (!neutralMusic.isPlaying)
            {
                music.Stop();
                neutralMusic.Play();
                sadMusic.Stop();
            }
        }
        else
        {
            if (!sadMusic.isPlaying)
            {
                music.Stop();
                neutralMusic.Stop();
                sadMusic.Play();
            }
        }
        
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
        music.volume = volumeSlider.value;
        sadMusic.volume = volumeSlider.value;
        neutralMusic.volume = volumeSlider.value;
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
        else if (soundSlider.value > .5f)
        {
            soundImage.sprite = soundImages[1];
        }
        else
        {
            soundImage.sprite = soundImages[2];
        }
        sfx.volume = soundSlider.value;
    }


}
