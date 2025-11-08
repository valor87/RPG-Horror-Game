using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    [SerializeField]  AudioClip clip;
    AudioSource AS;
    private void Start()
    {
         AS = this.AddComponent<AudioSource>();
    }
    public void PlaysoundOnce()
    {
        AS.PlayOneShot(clip);
    }
}
