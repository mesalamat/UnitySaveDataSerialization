using Godly.UnitySavingSystem.Serializables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Godly.UnitySavingSystem
{
    public class SavesManager : MonoBehaviour
    {
        public static SavesManager m_instance;
        [HideInInspector]
        internal DirectoryInfo saveDirectory;

        public List<SaveGameProfile> profiles = new List<SaveGameProfile>();
        [HideInInspector]
        public SaveGameProfile activeProfile;

        void OnEnable()
        {
            saveDirectory = Directory.CreateDirectory(Application.persistentDataPath + "/saves");
	    //Go through all Files in the Directory
            foreach (FileInfo info in saveDirectory.GetFiles())
            {
		//Check whether they're dat Files
                if (info.Name.EndsWith(".dat"))
                {
		    //Parse them if they are.
                    SaveGameProfile sgp = SaveGameProfile.FromFile(info.Name);
                    profiles.Add(sgp);
                }
            }
            /*
	   You can uncomment this in case you do not have a way to create a Profile yet, see @SaveSelectionDropDown
			 
            if (profiles.Count == 0)
            {
                activeProfile = new SaveGameProfile("TestProfile");
                profiles.Add(activeProfile);
            }
            else activeProfile = profiles[0];*/
            SceneManager.activeSceneChanged += SaveDuringSceneSwitch;
            Application.quitting += SaveProfile;
        }

        public void SaveProfile()
        {
	    // Uncomment this if you want ALL profiles to be saved with this method!
            //this.profiles.ForEach(sg => sg.SaveToFile());
            this.activeProfile.SaveToFile();
        }

	/*
	This is one of the functions used in the Example, not necessary to run the whole system, can also be changed etc.
	*/
        public void CreateSaveGameProfile(TMPro.TMP_InputField field)
        {
            SaveGameProfile sgp = new SaveGameProfile(field.text);
            profiles.Add(sgp);
        }

        public SaveGameProfile GetByName(string name)
        {
            foreach(SaveGameProfile g in profiles)
            {
                if (g.GetProfileName().Equals(name))
                {
                    return g;
                }
            }
            return null;
        }

        private void SaveDuringSceneSwitch(Scene arg0, Scene arg1)
        {
            SaveProfile();
        }

        void Awake()
        {
            if (m_instance != null)
            {
                Destroy(this);
                Debug.Log("Can't have two SaveManagers in one Scene");
            }
            else
            {
                m_instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

        }
    }
}
