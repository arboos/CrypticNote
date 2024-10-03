using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInfo : MonoBehaviour
{
    public int selfIndex;

    [SerializeField] private Button deleteThisNote_Button;
    [SerializeField] private Button editNoteButton;
    
    public bool noteInited;

    private void OnEnable()
    {
        if (noteInited) return;
        
        deleteThisNote_Button.onClick.AddListener(delegate
        {
            UIManager.Instance.DeleteNote_Warning_Window.SetActive(true);
            UIManager.Instance.DeleteNoteManager.currentNoteToDelete = selfIndex;
            UIManager.Instance.AllNotesViewerManager.RenderNotes();
        });
        
        editNoteButton.onClick.AddListener(delegate
        {
            UIManager.Instance.NewNote.SetActive(true);
            UIManager.Instance.MyNotes.SetActive(false);
            UIManager.Instance.newNoteMan.OpenNote(this.selfIndex);
        });
        
        noteInited = true;
    }
}
