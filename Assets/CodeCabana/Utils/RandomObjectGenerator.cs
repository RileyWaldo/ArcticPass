using System.Collections.Generic;
using UnityEngine;

namespace CodeCabana.Utils
{
    public abstract class RandomObjectGenerator<T> : ScriptableObject
    {
        [SerializeField] List<T> genericList = new List<T>();

        public void AddItem(T item)
        {
            genericList.Add(item);
        }

        public int GetSize()
        {
            return genericList.Count;
        }

        public T GetRandomItem()
        {
            return genericList[Random.Range(0, GetSize())];
        }

        public T GetItemByIndex(int index)
        {
            if (index >= 0 && index < GetSize())
                return genericList[index];

            Debug.LogWarning(name + " Index out of range.");
            return genericList[0];
        }
    }
}
