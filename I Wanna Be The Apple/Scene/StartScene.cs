using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class StartScene : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip selectSound;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void StartButtonOn()
    {
        audio.PlayOneShot(selectSound);
        SceneManager.LoadScene("stage_1");
    }

    public void RestartButtonOn()
    {
        audio.PlayOneShot(selectSound);
        SceneManager.LoadScene("stage_1");
    }

    public void QuitButtonOn()
    {
        audio.PlayOneShot(selectSound);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
