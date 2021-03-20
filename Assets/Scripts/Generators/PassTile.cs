using System.Collections.Generic;
using UnityEngine;

namespace ArcticPass.Generators
{
    public class PassTile : MonoBehaviour
    {
        [SerializeField] List<Transform> connectingPoints = new List<Transform>();

        public IEnumerable<Transform> GetConnections()
        {
            return connectingPoints;
        }
    }
}