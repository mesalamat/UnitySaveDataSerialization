using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


namespace Godly.UnitySavingSystem.Serializables
{

    [Serializable]
    public class SavingSerializable<T>
    {


        private readonly string identifier;
        internal T value;
        public SavingSerializable(string identifier)
        {
            this.identifier = identifier;
        }


        public string GetIdentifier()
        {
            return identifier;
        }

        public T GetValue()
        {
            return value;
        }

        public void SetValue(T value)
        {
            this.value = value;
        }



    }
}
