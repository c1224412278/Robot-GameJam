using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get { return _Instance; }
        set { _Instance = value; }
    }
    private static AudioManager _Instance;

    public GameData.AudioData theAudioData;

    [SerializeField] private AudioSource m_AudioSource;
    private void Awake()
    {
        _Instance = this;
    }
    private void Start()
    {
        if (m_AudioSource == null)
        {
            return;
        }
    }
    public void Fn_PlayBgm(AudioClip BgmClip)
    {
        m_AudioSource.clip = BgmClip;
        if (m_AudioSource.clip != null)
        {
            m_AudioSource.volume = 1f;
            m_AudioSource.loop = true;
            m_AudioSource.Play();
        }
    }
    public void Fn_StopBgm()
    {
        StartCoroutine(Fn_SetBgmVolume());
    }
    private IEnumerator Fn_SetBgmVolume()
    {
        while (m_AudioSource.volume > 0)
        {
            m_AudioSource.volume -= 2.5f * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    public void Fn_PlayAudioEffect(AudioClip clip)
    {
        m_AudioSource.PlayOneShot(clip);
    }
}
