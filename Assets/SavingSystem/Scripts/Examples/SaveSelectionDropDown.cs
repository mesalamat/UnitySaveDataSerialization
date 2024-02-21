using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Godly.UnitySavingSystem.Serializables
{
    [RequireComponent(typeof(TMPro.TMP_Dropdown))]
    public class SaveSelectionDropDown : MonoBehaviour
    {

        //TextMeshPro Dropdown Object
        private TMPro.TMP_Dropdown dropDown;

        [SerializeField]
        [Tooltip("This is the Parent Object of the Profile Creation(Text etc.)")]
        private Transform profileCreation;


        void Start()
        {
            dropDown = GetComponent<TMPro.TMP_Dropdown>();
            dropDown.ClearOptions();

            List<TMPro.TMP_Dropdown.OptionData> options = new List<TMPro.TMP_Dropdown.OptionData>();

            foreach (SaveGameProfile g in SavesManager.m_instance.profiles)
            {
                options.Add(new TMPro.TMP_Dropdown.OptionData(g.GetProfileName()));
            }
            options.Add(new TMPro.TMP_Dropdown.OptionData("New Profile"));
            
            dropDown.AddOptions(options);
            dropDown.onValueChanged.AddListener(i =>
            {
                if(dropDown.options[i].text == "New Profile")
                {
                    profileCreation.gameObject.SetActive(true);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    string name = dropDown.options[i].text;
                    SaveGameProfile g = SavesManager.m_instance.GetByName(name);
                    if (g == null) return;
                    SavesManager.m_instance.activeProfile = g;
                }

            });
        }

        public void RefreshEntries()
        {
            dropDown.ClearOptions();

            List<TMPro.TMP_Dropdown.OptionData> options = new List<TMPro.TMP_Dropdown.OptionData>();

            foreach (SaveGameProfile g in SavesManager.m_instance.profiles)
            {
                options.Add(new TMPro.TMP_Dropdown.OptionData(g.GetProfileName()));
            }
            options.Add(new TMPro.TMP_Dropdown.OptionData("New Profile"));

            dropDown.AddOptions(options);
        }

        void OnEnable()
        {

        }

    }
}
