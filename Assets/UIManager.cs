using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Baruah.EncryptionSystem;
using Baruah.EncryptionSystem.Manager;
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

    public ThemeColorChange[] themeChangedObjects;

    public bool isLightTheme;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ReadFile();
        
        foreach (var note in allNotes.notes)
        {
            switch (note.encType)
            {
                case "AES":
                    EncyptionManager.manager.AEC_key = note.encKey;
                    EncyptionManager.manager.encryptionSystem = new AESEncryptionSystem(EncyptionManager.manager.AEC_key, EncyptionManager.manager.AEC_iv);
                    break;
                    
                case "DES":
                    EncyptionManager.manager.DES_key = note.encKey;
                    EncyptionManager.manager.encryptionSystem = new DESEncryptionSystem(EncyptionManager.manager.DES_key);
                    break;
            }
            
            note.header = EncyptionManager.manager.Decrypt<string>(note.header);
            note.text = EncyptionManager.manager.Decrypt<string>(note.header);
        }
        
        WriteFile();
        ReadFile();
        
        themeChangedObjects = GameObject.FindObjectsOfType<ThemeColorChange>(includeInactive:true);
        ChangeTheme();
        ChangeTheme();
    }

    private void OnApplicationQuit()
    {
        ReadFile();
        
        foreach (var note in allNotes.notes)
        {
            switch (note.encType)
            {
                case "AES":
                    EncyptionManager.manager.AEC_key = note.encKey;
                    EncyptionManager.manager.encryptionSystem = new AESEncryptionSystem(EncyptionManager.manager.AEC_key, EncyptionManager.manager.AEC_iv);
                    break;
                    
                case "DES":
                    EncyptionManager.manager.DES_key = note.encKey;
                    EncyptionManager.manager.encryptionSystem = new DESEncryptionSystem(EncyptionManager.manager.DES_key);
                    break;
            }
            
            note.header = EncyptionManager.manager.Encrypt<string>(note.header);
            note.text = EncyptionManager.manager.Encrypt<string>(note.header);
        }
        
        WriteFile();
        ReadFile();
        
        themeChangedObjects = GameObject.FindObjectsOfType<ThemeColorChange>(includeInactive:true);
        ChangeTheme();
        ChangeTheme();
    }

    public void ChangeTheme()
    {
        isLightTheme = !isLightTheme;
        print(1);
        themeChangedObjects = GameObject.FindObjectsOfType<ThemeColorChange>(includeInactive:true);
        foreach (var toChange in themeChangedObjects)
        {
            toChange.ChangeColor();
        }
    }

    public void ReadFile()
    {
        // allNotes = JsonUtility.FromJson<AllNotes>(File.ReadAllText("Assets/Resources/SavedNotes.json"));
        // Does the file exist?
        if (File.Exists(saveFile))
        {
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
