using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColourpickerController : MonoBehaviour
{
    public float currentHue;
    public float currentSaturation;
    public float currentValue;

    
    [SerializeField] private RawImage hueImage;
    [SerializeField] private RawImage satValImage;
    [SerializeField] private RawImage outPutImage;
    
    [SerializeField]
    private Slider hueSlider;

    private Texture2D _hueTexture;
    private Texture2D _svTexture;
    private Texture2D _outPutTexture;

    [SerializeField] 
    public Image changeThisColour;

    private void Start()
    {
        CreateHueImage();
        CreateSVImage();
        CreateOutPutImage();
        UpdateOutPutImage();
    }

    private void CreateHueImage()
    {
        _hueTexture = new Texture2D(1, 16);
        _hueTexture.wrapMode = TextureWrapMode.Clamp;
        _hueTexture.name = "HueTexture";

        for (int i = 0; i < _hueTexture.height; i++)
        {
            _hueTexture.SetPixel(0, i, Color.HSVToRGB((float) i / _hueTexture.height, 1, 1));
        }
        
        _hueTexture.Apply();
        currentHue = 0;
        
        hueImage.texture = _hueTexture;
    }

    private void CreateSVImage()
    {
        _svTexture = new Texture2D(16, 16);
        _svTexture.wrapMode = TextureWrapMode.Clamp;
        _svTexture.name = "SatValTexture";
        
        for (int y = 0; y < _svTexture.height; y++)
        {
            for (int x = 0; x < _svTexture.width; x++)
            {
                _svTexture.SetPixel(x,y,Color.HSVToRGB(
                                            currentHue,
                                            (float)x/_svTexture.width,
                                            (float)y/_svTexture.height));
            }
        }
        
        _svTexture.Apply();
        currentSaturation = 0;
        currentValue = 100;

        satValImage.texture = _svTexture;
    }

    private void CreateOutPutImage()
    {
        _outPutTexture = new Texture2D(1, 16);
        _outPutTexture.wrapMode = TextureWrapMode.Clamp;
        _outPutTexture.name = "OutPutTexture";
        
        Color currentColour = Color.HSVToRGB(currentHue,currentSaturation,currentValue);

        for (int i = 0; i < _outPutTexture.height; i++)
        {
            _outPutTexture.SetPixel(0,i, currentColour);
        }
        _outPutTexture.Apply();
        outPutImage.texture = _outPutTexture;
    }

    private void UpdateOutPutImage()
    {
        Color currentColour = Color.HSVToRGB(currentHue,currentSaturation,currentValue);
        for (int i = 0; i < _outPutTexture.height; i++)
        {
            _outPutTexture.SetPixel(0,i, currentColour);
        }
        _outPutTexture.Apply();
        changeThisColour.color = currentColour;
    }

    public void SetSV(float S, float V)
    {
        currentSaturation = S;
        currentValue = V;
        
        UpdateOutPutImage();
    }

    public void UpdateSVImage()
    {
        currentHue = hueSlider.value;
        for (int y = 0; y < _svTexture.height; y++)
        {
            for (int x = 0; x < _svTexture.width; x++)
            {
                _svTexture.SetPixel(x,y,Color.HSVToRGB(
                                                currentHue,
                                                (float)x/_svTexture.width,
                                                (float)y/_svTexture.height));
            }
        }
        _svTexture.Apply();
         UpdateOutPutImage();
    }
}