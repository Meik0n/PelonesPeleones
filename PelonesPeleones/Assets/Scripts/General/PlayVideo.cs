using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour
{
    private RawImage image;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(PlayClip());
    }

    private IEnumerator PlayClip()
    {
        videoPlayer.Prepare();

        while(videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(1f);
            yield break;
        }
        image.texture = videoPlayer.texture;
        videoPlayer.Play();
        yield return null;
    }
}
