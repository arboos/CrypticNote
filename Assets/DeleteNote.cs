using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeleteNote : MonoBehaviour
{
    public int currentNoteToDelete;
    
    public void Delete()
    {
        if (currentNoteToDelete < 0)
        {
            Debug.LogError("CURRENT NOTE TO DELETE IS LESS THAN 0!!!");
            return;
        }
        
        print("Start deleting");
        UIManager.Instance.ReadFile();
        
        print("Lengths after reading: " + UIManager.Instance.allNotes.notes.Length);
            
        Note[] allNotesNew = new Note[UIManager.Instance.allNotes.notes.Length-1];

        bool indexReaded = false;
        for(int i = 0; i < UIManager.Instance.allNotes.notes.Length; i++)
        {
            if (i == currentNoteToDelete)
            {
                indexReaded = true;
            }
            else if (!indexReaded)
            {
                allNotesNew[i] = UIManager.Instance.allNotes.notes[i];
            }
            else
            {
                allNotesNew[i-1] = UIManager.Instance.allNotes.notes[i];
            }
        }
        print("Lengths after first for: " + UIManager.Instance.allNotes.notes.Length);

        UIManager.Instance.allNotes.notes = new Note[allNotesNew.Length];
        
        for(int i = 0; i < allNotesNew.Length; i++)
        {
            UIManager.Instance.allNotes.notes[i] = allNotesNew[i];
        }
        
        UIManager.Instance.WriteFile();
        
        UIManager.Instance.ReadFile();
        print("Delete ends");
        print("Lengths after end: " + UIManager.Instance.allNotes.notes.Length);
        currentNoteToDelete = -1;
    }
}
