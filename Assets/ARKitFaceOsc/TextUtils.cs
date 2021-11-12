using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextUtils : MonoBehaviour
{
    /// <summary>
    /// SetValueToText: This method for onValueChange for UGUI on inspector.
    /// </summary>
    public float SetValueToText
    {
        set
        {
            if (mytext != null)
                mytext.text = value.ToString();
        }
    }

    private Text mytext;

    private void Start()
    {
        mytext = GetComponent<Text>();
    }
}
