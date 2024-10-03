using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class EditNoteContainer : MonoBehaviour
{
    public TMP_InputField header;
    public TMP_InputField info;

    public GameObject EncMethods;
    
    public RadioButton AES_RB;
    public RadioButton DES_RB;

    public TMP_InputField key;
}
