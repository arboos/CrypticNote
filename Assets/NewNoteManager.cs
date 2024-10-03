using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;
using Toggle = UnityEngine.UI.Toggle;

public class NewNoteManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField noteHeader;
    [SerializeField] private TMP_InputField noteText;

    [SerializeField] private Button saveButton;
    [SerializeField] private TMP_InputField encKey_TMP;

    private bool fileReaded = false;

    public Note note;

    public int noteID;

    public GameObject aesRB;
    public GameObject desRB;
    public GameObject encKeyHeader;
    public GameObject encKeyText;
    
    private string[] monthStr = new []{"ZERO", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};

    public ToggleGroup toogleGroup;
    private string[] letters = new string[]
    {
        "a", "A", "b", "B", "c", "C", "d", "D", "e", "E", "f", "F", "g", "G", "h", "H",
        "i", "I", "j", "J", "k", "K", "l", "L", "m", "M", "n", "N", "o", "O", "p", "P",
        "q", "Q", "r", "R", "s", "S", "t", "T", "u", "U", "v", "V", "w", "W", "x", "X",
        "y", "Y", "z", "Z"
    };
    
    private void Start()
    {
        saveButton.onClick.AddListener(delegate
        {
            SaveNote(); 
            UIManager.Instance.MyNotes.SetActive(true); 
            UIManager.Instance.NewNote.SetActive(false);
        });
    }

    public void OpenNote()
    {
        UIManager.Instance.ReadFile();
        
        noteID = UIManager.Instance.allNotes.notes.Length;
        Note[] allNotesNew = new Note[noteID+1];

        for(int i = 0; i < UIManager.Instance.allNotes.notes.Length; i++)
        {
            allNotesNew[i] = UIManager.Instance.allNotes.notes[i];
        }

        UIManager.Instance.allNotes.notes = new Note[noteID+1];
        
        for(int i = 0; i < allNotesNew.Length; i++)
        {
            UIManager.Instance.allNotes.notes[i] = allNotesNew[i];
        }
        UIManager.Instance.WriteFile();
        
        UIManager.Instance.ReadFile();
        note = UIManager.Instance.allNotes.notes[noteID];
        
        fileReaded = true;
        
        SaveNote();
        UIManager.Instance.WriteFile();
        
        UIManager.Instance.ReadFile();
    }
    
    public void OpenNote(int index)
    {
        UIManager.Instance.ReadFile();
        
        noteID = index;
        
        note = UIManager.Instance.allNotes.notes[noteID];

        noteHeader.text = note.header;
        noteText.text = note.text;

        encKey_TMP.text = note.encKey;
        
        switch (note.encType)
        {
            case "AES":
                encKeyHeader.SetActive(false);
                encKeyText.SetActive(false);
                aesRB.GetComponent<Toggle>().isOn = true;
                desRB.GetComponent<Toggle>().isOn = false;
                break;
            
            case "DES":
                encKeyHeader.SetActive(true);
                encKeyText.SetActive(true);
                aesRB.GetComponent<Toggle>().isOn = false;
                desRB.GetComponent<Toggle>().isOn = true;
                break;
            
        }
        
        encKey_TMP.text = note.encKey;
        fileReaded = true;
    }

    public void GenerateRandomKey()
    {
        string finalKey = "";
        for (int i = 0; i < 8; i++)
        {
            finalKey += letters[Random.Range(0, letters.Length)];
            encKey_TMP.text = finalKey;
        }
    }
    
    public void SaveNote()
    { 
        UIManager.Instance.allNotes.notes[noteID].header = noteHeader.text;
        UIManager.Instance.allNotes.notes[noteID].text = noteText.text;
        UIManager.Instance.allNotes.notes[noteID].dateModified = DateTime.Now.Day.ToString() + " " +
                                                                 monthStr[DateTime.Now.Month] + " " +
                                                                 DateTime.Now.Year.ToString();

        UIManager.Instance.allNotes.notes[noteID].encType = toogleGroup.GetFirstActiveToggle().name;
        note.encKey = encKey_TMP.text;
        UIManager.Instance.allNotes.notes[noteID].encKey = encKey_TMP.text;
        
        switch (UIManager.Instance.allNotes.notes[noteID].encType)
        {
            case "DES":
                if (note.encKey.Length > 8)
                {
                    string keyAns = "";
                    for(int i = 0; i < 8; i++)
                    {
                        keyAns += note.encKey[i];
                    }
                    UIManager.Instance.allNotes.notes[noteID].encKey = keyAns;
                }
                else if (note.encKey.Length < 8)
                {
                    string keyAns = "";
                    for(int i = 0; i < 8; i++)
                    {
                        if (note.encKey.Length > i)
                        {
                            keyAns += note.encKey[i];
                        }
                        else
                        {
                            keyAns += "a";
                        }
                    }
                    UIManager.Instance.allNotes.notes[noteID].encKey = keyAns;
                }
                else
                {
                    UIManager.Instance.allNotes.notes[noteID].encKey = encKey_TMP.text;
                }
                break;
            
            case "AES":
                UIManager.Instance.allNotes.notes[noteID].encKey = "A60A5770FE5E7AB200BA9CFC94E4E8B0";
                break;
        }
        
        
        UIManager.Instance.WriteFile();
        UIManager.Instance.ReadFile();
        

        noteHeader.text = "";
        noteText.text = "";
        note.encKey = "ABCDEFGH";

        print("Note saved: " + note.text + note.header + note.encType + note.encKey);
    }
    
}

[System.Serializable]
public class AllNotes
{
    public Note[] notes;
}

[System.Serializable]
public class Note
{
    public string header;

    public string text;

    public string dateModified;
    
    public string encType;
    public string encKey;

}
