using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteInfo : MonoBehaviour
{
    public int selfIndex;

    [SerializeField] private Button deleteThisNote_Button;

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
        noteInited = true;
    }
}
