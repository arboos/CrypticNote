using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class NewNoteManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField noteHeader;
    [SerializeField] private TMP_InputField noteText;

    [SerializeField] private Button saveButton;

    private bool fileReaded = false;

    public Note note;

    public int noteID;

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
    
    public void SaveNote()
    { 
        UIManager.Instance.allNotes.notes[noteID].header = noteHeader.text;
        UIManager.Instance.allNotes.notes[noteID].text = noteText.text;
        UIManager.Instance.WriteFile();
        print("Note saved: " + note.text + note.header);

        noteHeader.text = "";
        noteText.text = "";
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
}
