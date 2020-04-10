using UnityEngine;

namespace ArcticPass.Generator
{
    [CreateAssetMenu(fileName = "NameList", menuName = "ArcticPass/Create New Name List", order = 2)]
    public class NameList : ScriptableObject
    {
        [SerializeField] string[] firstFemaleNames;
        [SerializeField] string[] firstMaleNames;
        [SerializeField] string[] lastNames;
        [SerializeField] string[] villageFirstNames;
        [SerializeField] string[] villageLastNames;
        [SerializeField] string[] caveNames;

        public string GetFirstFemaleName()
        {
            return firstFemaleNames[Random.Range(0, firstFemaleNames.Length)];
        }

        public string GetFirstMaleName()
        {
            return firstMaleNames[Random.Range(0, firstMaleNames.Length)];
        }

        public string GetLastName()
        {
            return lastNames[Random.Range(0, lastNames.Length)];
        }

        public string GetVillageFirstName()
        {
            return villageFirstNames[Random.Range(0, villageFirstNames.Length)];
        }

        public string GetVillageLastName()
        {
            return villageLastNames[Random.Range(0, villageLastNames.Length)];
        }

        public string GetCaveName()
        {
            return caveNames[Random.Range(0, caveNames.Length)];
        }
    }
}
