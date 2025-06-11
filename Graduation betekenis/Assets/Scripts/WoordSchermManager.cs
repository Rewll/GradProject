using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WoordSchermManager : MonoBehaviour
{
    [SerializeField] private CollageCreateState colManagerRef;
    [Space]
    public TMP_InputField tekstInputField;
    public TMP_Text voorbeeldTekst;
    
    private string _getyptWoord;
    public float wordScaleFactor = 0.1f;
    public float wordMaxScale = 6f;
    
    public void SetVoorbeeld()
    {
        _getyptWoord = tekstInputField.text;
        voorbeeldTekst.text = _getyptWoord;
    }

    public void ToevoegenAanCollage()
    {
        colManagerRef.AddWordToCollage(_getyptWoord, voorbeeldTekst.color, wordScaleFactor,wordMaxScale);
    }

}
