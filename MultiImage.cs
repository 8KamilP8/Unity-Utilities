using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
[RequireComponent(typeof(Image))]
public class MultiImage : MonoBehaviour<Image>
{    
    [SerializeField] private Sprite[] spritePool;
    
    public void ChangeImage(int i) {
        component.sprite =spritePool[i];
    }
    public void ChangeImage(IntParser parser) {
        component.sprite = spritePool[parser.ParseToInt()];
    }
}
