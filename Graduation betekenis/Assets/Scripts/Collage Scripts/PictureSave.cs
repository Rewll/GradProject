using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PictureSave", menuName = "Scriptable Objects/PictureSave")]
public class PictureSave : ScriptableObject
{
    public List<Texture2D> pictures = new List<Texture2D>();
}
