using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImgFromUrl : MonoBehaviour
{

    public string surrahNumImg, ayaNumImg;
    public RawImage myRawImage, myRawImagebg;
    public Text userSurrahNumImg, userAyaNumImg,errorTxt;
    public GameObject mainCanvas, playerCanvas;
    public void LoadImg()
    {
        StartCoroutine(DownloadImage("https://everyayah.com/data/images_png/"+ userSurrahNumImg.text + "_"+ userAyaNumImg.text + ".png"));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            errorTxt.text = "Error\n" +
                "1-check if surah and ayah numbers are right!\n" +
                "2-check your internet connection...";
        }
        else
        {

            Texture2D texture2d = ((DownloadHandlerTexture)request.downloadHandler).texture;
            errorTxt.text = "";
            myRawImage.rectTransform.sizeDelta = new Vector2(texture2d.width +70f, texture2d.height+40f);
            myRawImagebg.rectTransform.sizeDelta = new Vector2(texture2d.width + 70f, texture2d.height+40f);
            myRawImage.texture = texture2d;
            mainCanvas.SetActive(false);
            playerCanvas.SetActive(true);
        }
    }
}
