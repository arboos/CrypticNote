using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    public DeleteNote DeleteNoteManager;
    public AllNotesViewer AllNotesViewerManager;
    public AllNotes allNotes;
    
    public GameObject StartScreen;
    public GameObject NewNote;
    public GameObject MyNotes;

    public GameObject DeleteNote_Warning_Window;
    
    [HideInInspector] public string saveFile = "Assets/Resources/SavedNotes.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void ReadFile()
    {
        // allNotes = JsonUtility.FromJson<AllNotes>(File.ReadAllText("Assets/Resources/SavedNotes.json"));
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            print("File ");
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            allNotes = JsonUtility.FromJson<AllNotes>(fileContents);
        }
    }
    
    public void WriteFile()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(allNotes);

        // Write JSON to file.
        File.WriteAllText(UIManager.Instance.saveFile, jsonString);
    }


}
