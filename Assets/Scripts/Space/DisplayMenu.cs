using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayMenu : MonoBehaviour
{
    public Button ExitButton;
    public static Image image1, image2, image3;
    public Image[] pics = {image1, image2, image3};
    public Image preview = null;
    public static float x = 1, y = 1;
    public Slider XSlide, YSlide;
    public AudioSource click;

    public void BackroundM(int num)
    {
        preview.sprite = pics[num].sprite;
        PlayerPrefs.SetInt("index", num);
    }

    public void Xaxis(float value)
    {
        PlayerPrefs.SetFloat("X", value);
        //x = value;
    }

    public void Yaxis(float value)
    {
        PlayerPrefs.SetFloat("Y", value);
        //y = value;
    }
}