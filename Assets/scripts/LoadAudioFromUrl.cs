using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
public class LoadAudioFromUrl : MonoBehaviour
{
    public Text surrahNumAudio, ayaNumAudio;
    public Text userSurrahNumAudio, userAyaNumAudio;
    public AudioVisualizer visualizerSrc;
    int userSurrahNumAudioAsNumber , userAyaNumAudioAsNumber;
    AudioSource audioSource;
    AudioClip myClip;
    public Slider mySlider;
    bool audioReady;
    public Text endTime, currentTime, errorTxt;
    public GameObject loopBtn, loopDarkBtn, soundBtn, muteBtn,mainCanvas,playerCanvas,pauseBtn,continueBtn,darkMode,lightMode,replayBtn;
    public Color lightM, darkM;
    public Button playBtn;
    public void PlayAudio()
    {
        int.TryParse(userSurrahNumAudio.text, out int userSurrahNumAudioAsNumber);
        int.TryParse(userAyaNumAudio.text, out int userAyaNumAudioAsNumber);
        audioSource = GetComponent<AudioSource>();

        if (userSurrahNumAudioAsNumber > 0 && userSurrahNumAudioAsNumber < 10 && userAyaNumAudioAsNumber > 0 && userAyaNumAudioAsNumber < 10)
        {
            StartCoroutine(GetAudioClip("https://everyayah.com/data/Menshawi_16kbps/" + "00" + surrahNumAudio.text + "00" + ayaNumAudio.text + ".mp3"));
        }
        else if (userSurrahNumAudioAsNumber > 0 && userSurrahNumAudioAsNumber < 10 && userAyaNumAudioAsNumber > 9 && userAyaNumAudioAsNumber < 100)
        {
            StartCoroutine(GetAudioClip("https://everyayah.com/data/Menshawi_16kbps/" + "00" + surrahNumAudio.text + "0" + ayaNumAudio.text + ".mp3"));
        }
        else if (userSurrahNumAudioAsNumber > 0 && userSurrahNumAudioAsNumber < 10 && userAyaNumAudioAsNumber > 99)
        {
            StartCoroutine(GetAudioClip("https://everyayah.com/data/Menshawi_16kbps/" + "00" + surrahNumAudio.text + ayaNumAudio.text + ".mp3"));
        }


        else if (userSurrahNumAudioAsNumber > 9 && userSurrahNumAudioAsNumber < 100 && userAyaNumAudioAsNumber > 0 && userAyaNumAudioAsNumber < 10)
        {
            StartCoroutine(GetAudioClip("https://everyayah.com/data/Menshawi_16kbps/" + "0" + surrahNumAudio.text + "00" + ayaNumAudio.text + ".mp3"));
        }
        else if (userSurrahNumAudioAsNumber > 9 && userSurrahNumAudioAsNumber < 100 && userAyaNumAudioAsNumber > 9 && userAyaNumAudioAsNumber < 100)
        {
            StartCoroutine(GetAudioClip("https://everyayah.com/data/Menshawi_16kbps/" + "0" + surrahNumAudio.text + "0" + ayaNumAudio.text + ".mp3"));
        }
        else if (userSurrahNumAudioAsNumber > 9 && userSurrahNumAudioAsNumber < 100 && userAyaNumAudioAsNumber > 99)
        {
            StartCoroutine(GetAudioClip("https://everyayah.com/data/Menshawi_16kbps/" + "0" + surrahNumAudio.text + ayaNumAudio.text + ".mp3"));
        }

        else if (userSurrahNumAudioAsNumber > 99 && userAyaNumAudioAsNumber > 0 && userAyaNumAudioAsNumber < 10)
        {
            StartCoroutine(GetAudioClip("https://everyayah.com/data/Menshawi_16kbps/" + surrahNumAudio.text + "00" +ayaNumAudio.text + ".mp3"));
        }
        else if (userSurrahNumAudioAsNumber > 99 && userAyaNumAudioAsNumber > 9 && userAyaNumAudioAsNumber < 100)
        {
            StartCoroutine(GetAudioClip("https://everyayah.com/data/Menshawi_16kbps/" + surrahNumAudio.text + "0"+ayaNumAudio.text + ".mp3"));
        }
        else if (userSurrahNumAudioAsNumber > 99 && userAyaNumAudioAsNumber > 99)
        {
            StartCoroutine(GetAudioClip("https://everyayah.com/data/Menshawi_16kbps/" + surrahNumAudio.text + ayaNumAudio.text + ".mp3"));
        }


    }
    private void Update()
    {
        if (audioReady) {
            currentTime.text = mySlider.value.ToString() + "s";
            mySlider.value = (float)Math.Round(audioSource.time, 1);
            visualizerSrc.StartAudioVisualizer();
            if (mySlider.value == mySlider.maxValue)
            {
                replayBtn.SetActive(true);
                audioReady = false;
            }
            else 
            {
                replayBtn.SetActive(false);
            }
            }
       
    }
   
    IEnumerator GetAudioClip(string url)
    {
      
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                errorTxt.text = "Error\n" +
                "1-check if surah and ayah numbers are right!\n" +
                "2-check your internet connection...";
            }
            else
            {
                myClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = myClip;
                audioSource.Play();
                errorTxt.text = "";
                mySlider.maxValue = (float)Math.Round(myClip.length, 1);
                endTime.text = mySlider.maxValue.ToString() + "s";
                audioReady = true;
                mainCanvas.SetActive(false);
                playerCanvas.SetActive(true);

            }
        }
    }
    public void Play() 
    {
        StartCoroutine(HideAndShow(5f));

    }
    IEnumerator HideAndShow(float delay)
    {
        playBtn.enabled =false;
        yield return new WaitForSeconds(delay);
        playBtn.enabled = true;
    }
    public void Pause() 
    {
        audioSource.Pause();
        pauseBtn.SetActive(false);
        continueBtn.SetActive(true);
    }
    public void Continue()
    {
        audioSource.UnPause();
        continueBtn.SetActive(false);
        pauseBtn.SetActive(true);
    }
    public void SoundOff() 
    {
        audioSource.volume = 0f;
        muteBtn.SetActive(true);
        soundBtn.SetActive(false);
    }
    public void SoundOn()
    {
        audioSource.volume = 1f;
        muteBtn.SetActive(false);
        soundBtn.SetActive(true);
    }
    public void Loop()
    {
        audioSource.loop = true;
        loopBtn.SetActive(false);
        loopDarkBtn.SetActive(true);
    }
    public void StopLoop()
    {
        audioSource.loop = false;
        loopBtn.SetActive(true);
        loopDarkBtn.SetActive(false);
    }
    public void Home() 
    {
        audioSource.Stop();
        playerCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }

    public void DarkMode()
    {
        Camera.main.backgroundColor = darkM;
        lightMode.SetActive(true);
        darkMode.SetActive(false);
    }
    public void LightMode()
    {
        Camera.main.backgroundColor = lightM;
        darkMode.SetActive(true);
        lightMode.SetActive(false);

    }
    public void Replay() 
    {
        PlayAudio();
        replayBtn.SetActive(false);
    }

}
