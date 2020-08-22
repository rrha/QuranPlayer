using UnityEngine;
using UnityEngine.UI;


public class AudioVisualizer : MonoBehaviour
{
    public AudioSource audioSrc;
    public float minHeight = 15.0f;
    public float maxHeight = 425.0f;
    public float updateSentivity = 10.0f;
    public Image[] visualizerObjects;

    public void StartAudioVisualizer()
    {
        float[] spectrumData = new float[256]; 
        audioSrc.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        for (int i = 0; i < visualizerObjects.Length; i++)
        {
            Vector2 newSize = visualizerObjects[i].GetComponent<RectTransform>().rect.size;

            newSize.y = Mathf.Clamp(Mathf.Lerp(newSize.y, minHeight + (spectrumData[i] * (maxHeight - minHeight) * 5.0f), updateSentivity * 0.5f), minHeight, maxHeight);
            visualizerObjects[i].GetComponent<RectTransform>().sizeDelta = newSize;

        }
    }

}