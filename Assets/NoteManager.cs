using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField noteHeader;
    [SerializeField] private TMP_InputField noteText;

    [SerializeField] private Button saveButton;

    private bool fileReaded = false;

    private void Start()
    {
        saveButton.onClick.AddListener(delegate{SaveNote();});
    }

    void Update()
    {
        if (!fileReaded)
        {
            ReadFile();
            note = allNotes.notes[0];
            noteHeader.text = note.header;
            noteText.text = note.text;
            fileReaded = true;
        }
    }

    public void SaveNote()
    {
        note.header = noteHeader.text;
        note.text = noteText.text;
        WriteFile();
        print("Note saved");
    }
    
    // Create a field for the save file.
    private string saveFile = "Assets/Resources/SavedNotes.json";

    // Create a GameData field.
    public Note note;
    public AllNotes allNotes;

    void Awake()
    {
        // Update the path once the persistent path exists.
        //saveFile = Resources.Load("SavedNotes.json").ConvertTo<string>();
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
        File.WriteAllText(saveFile, jsonString);
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
