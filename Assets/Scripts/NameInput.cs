using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NameInput : MonoBehaviour
{
    public TextMeshProUGUI text;
    public void InputName(string sName)
    {
        text.text = sName;
    }
}
