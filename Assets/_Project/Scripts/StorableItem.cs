using System;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.Assets._Project.Scripts
{
    [Serializable]
    public class SerializedStorableItem
    {
        public string name;
        public float value;
    }


    public class StorableItem : MonoBehaviour
    {

        private void Start()
        {
            SerializedStorableItem item = new SerializedStorableItem();
            item.name = "f";
            item.value = 1f;

            PlayerPrefs.SetString("Item1", JsonUtility.ToJson(item));
            item = JsonUtility.FromJson<SerializedStorableItem>(PlayerPrefs.GetString("Item1"));

        }

    }
}
