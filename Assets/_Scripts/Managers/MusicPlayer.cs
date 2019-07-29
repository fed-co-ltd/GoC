using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }else{
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            var audio = GetComponent<AudioSource>();
            audio.volume = 0.5f;
        }
    }

}
