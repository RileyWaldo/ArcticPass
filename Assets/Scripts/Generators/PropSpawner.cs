using System.Collections.Generic;
using UnityEngine;
using ArcticPass.Core;

namespace ArcticPass.Generator
{
    public class PropSpawner : MonoBehaviour
    {
        [SerializeField] List<PropDetails> props = new List<PropDetails>();

        PassGenerator passGenerator;
        float radius;

        private void Start()
        {
            passGenerator = FindObjectOfType<PassGenerator>();
            radius = passGenerator.GetChunkSize() / 2;
        }

        public void PopulatePass(List<Vector3> passData)
        {
            foreach(Vector3 node in passData)
            {
                foreach(PropDetails prop in props)
                {
                    for(int i = prop.GetDensity(); i>0; i--)
                    {
                        if(Random.value <= prop.GetSpawnChance())
                        {
                            Vector3 randomPos = new Vector3(node.x, node.y, 0);
                            Prop newProp = Instantiate(prop.GetProp(), randomPos, Quaternion.identity);
                            newProp.transform.parent = transform;
                        }
                    }
                }
            }
        }

        public void PopulateCave()
        {

        }

        [System.Serializable]
        private class PropDetails
        {
            [SerializeField] Prop propPrefab = null;
            [SerializeField] int density = 10;
            [Range(0f, 1f)][SerializeField] float spawnChance = 1f;

            public Prop GetProp()
            {
                return propPrefab;
            }

            public int GetDensity()
            {
                return density;
            }

            public float GetSpawnChance()
            {
                return spawnChance;
            }
        }
    }
}
