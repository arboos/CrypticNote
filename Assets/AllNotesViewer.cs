using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class AllNotesViewer : MonoBehaviour
{

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform parent;
    
    private void OnEnable()
    {
        RenderNotes();
    }

    public void RenderNotes()
    {
        DeleteRenderedNotes();
        UIManager.Instance.ReadFile();
        
        int indexStart = 0;
        foreach (var note in UIManager.Instance.allNotes.notes)
        {
            GameObject spawnedNote = Instantiate(notePrefab, parent);
            spawnedNote.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = note.header;
            spawnedNote.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = note.text;
            spawnedNote.GetComponent<NoteInfo>().selfIndex = indexStart;
            indexStart++;
        }
    }

    private void DeleteRenderedNotes()
    {
        for(int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
    
}
