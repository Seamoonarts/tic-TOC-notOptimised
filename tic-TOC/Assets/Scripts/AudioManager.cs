using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

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
        Sound s = Array.Find(sounds, sounds => sounds.nombre == nombre);
        if (s == null)
            return;
        s.source.Play();
    }
    

    public void Stop(string nombre)
    {
        Sound s = Array.Find(sounds, sounds => sounds.nombre == nombre);
        if (s == null)
            return;
        s.source.Stop();
    }

    public void Pause(string nombre)
    {
        Sound s = Array.Find(sounds, sounds => sounds.nombre == nombre);
        if (s == null)
            return;
        s.source.Pause();
    }
    // FindObjectOfType<AudioManager>().Play("nombredelaudio");
}
