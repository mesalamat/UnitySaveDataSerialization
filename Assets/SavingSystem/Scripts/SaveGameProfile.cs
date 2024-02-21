using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using Godly.UnitySavingSystem.Serializables.Example;

namespace Godly.UnitySavingSystem.Serializables
{
    [Serializable]
    public class SaveGameProfile
    {
        private string name;
        private long createdTime;
        private readonly ArrayList toSerialize;

        public SaveGameProfile(string name)
        {
            this.name = name;
            this.createdTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            toSerialize = new ArrayList();
	    //All non-internal(this class) values of type T you want to save need to be registered here or somewhere during Runtime. Re-registering upon parsing is not necessary.
            RegisterSerializable(new CollectibleSerializable(Collectible.CollectibleType.COIN, 0));
        }

        public string GetProfileName()
        {
            return name;
        }

        public string GetTimeString()
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(this.createdTime).LocalDateTime.ToString("hh:mm");
        }


        public static SaveGameProfile FromFile(String name)
        {
            FileStream fs = File.OpenRead(SavesManager.m_instance.saveDirectory.FullName + "/" + name);
            BinaryFormatter bf = new BinaryFormatter();
            SaveGameProfile profile = (SaveGameProfile)bf.Deserialize(fs);
            fs.Close();
            return profile ?? new SaveGameProfile(name);
        }



        public void RegisterSerializable<T>(SavingSerializable<T> serializable)
        {
            if (toSerialize.Contains(serializable)) return;
            toSerialize.Add(serializable);
        }


        public Task SaveToFile()
        {

            //We wanna run this async so it doesn't slow down the Main Process
            return Task.Run(() =>
            {
                BinaryFormatter bf = new BinaryFormatter();
                string path = SavesManager.m_instance.saveDirectory.FullName + "/" + name + ".dat";
                FileStream fileStream = File.Open(path, FileMode.OpenOrCreate);
                bf.Serialize(fileStream, this);
                fileStream.Close();
            });
            
        }

        internal void SetValue<T>(string v, T Value)
        {
            foreach (SavingSerializable<T> s in this.toSerialize)
            {
                if (s.GetIdentifier().Equals(v))
                {
                    
                    s.SetValue(Value);
                }
            }
        }

        internal T GetValue<T>(string v)
        {
           foreach(SavingSerializable<T> s in this.toSerialize)
            {
                if (s.GetIdentifier().Equals(v))
                {
                    return s.GetValue();
                }
            }
            return default;
        }
    }
}
