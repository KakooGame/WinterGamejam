using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip bgm;

    // Start is called before the first frame update
    void Start()
    {
        // 播放背景音乐
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}