using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitmarkerView : SpaceView
{

    public Image HitmarkImage;
    public AudioClip HitmarkerSound;
    public float hitmarkshowtime;
    public AudioSource HitmarkerSoundSource;
    public void Start()
    {
        HitmarkerSoundSource = GetComponent<AudioSource>();
    }

    public void getHitmarker()
    {
       // StopCoroutine("showhitmarker");
        HitmarkImage.enabled = true;
        HitmarkerSoundSource.PlayOneShot(HitmarkerSound);
        StartCoroutine(showhitmarker());
    }

    public IEnumerator showhitmarker()
    {
        yield return new WaitForSeconds(hitmarkshowtime);
        HitmarkImage.enabled = false;
    }
}
