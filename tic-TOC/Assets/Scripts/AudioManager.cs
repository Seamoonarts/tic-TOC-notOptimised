using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Serial scriptSerial;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }

    void Start()
    {
        Play("Base");
        Play("Llamada");
    }

 
    public void Play(string nombre)
    {
        if (scriptSerial.intenso == true) {
            Sound s = Array.Find(sounds, sounds => sounds.nombre == nombre);
            if (s == null)
                return;
            s.source.Play();
            s.source.volume = 1f;
        } else {
            Sound s = Array.Find(sounds, sounds => sounds.nombre == nombre);
            if (s == null)
                return;
            s.source.Play();
            s.source.volume = s.volume;
        } 
    }
    

    public void Stop(string nombre)
    {
        Sound s = Array.Find(sounds, sounds => sounds.nombre == nombre);
        if (s == null)
            return;
        StartCoroutine(FadeOut(s.source, 0.2f, 0f));
       // s.source.Stop();
    }

    public void Pause(string nombre)
    {
        Sound s = Array.Find(sounds, sounds => sounds.nombre == nombre);
        if (s == null)
            return;
        s.source.Pause();
    }

    public IEnumerator FadeOut(AudioSource source, float duration, float targetVolume)
    {
        float time = 0f;
        float startVol = source.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            
            yield return null;
        }

        source.Stop();
        Debug.Log("STOP");

        yield break;
    }
}
