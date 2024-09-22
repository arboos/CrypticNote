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
            spawnedNote.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = note.dateModified;
            spawnedNote.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = note.header;
            spawnedNote.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = note.text;
            spawnedNote.GetComponent<NoteInfo>().selfIndex = indexStart;
            
            spawnedNote.transform.GetChild(0).GetComponent<ThemeColorChange>().ChangeColor(UIManager.Instance.isLightTheme);
            spawnedNote.transform.GetChild(1).GetComponent<ThemeColorChange>().ChangeColor(UIManager.Instance.isLightTheme);
            spawnedNote.transform.GetChild(3).GetComponent<ThemeColorChange>().ChangeColor(UIManager.Instance.isLightTheme);
            spawnedNote.transform.GetComponent<ThemeColorChange>().ChangeColor(UIManager.Instance.isLightTheme);
            
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
