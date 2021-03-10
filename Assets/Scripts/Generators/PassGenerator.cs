using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ArcticPass.Control;

namespace ArcticPass.Generators
{
    public class PassGenerator : MonoBehaviour
    {
        [SerializeField] Tilemap tileMap = null;
        [SerializeField] Tile tile = null;
        [SerializeField] float passLength = 100;
        [SerializeField] float passWidth = 1000;
        [SerializeField] float passHeight = 1000;
        [SerializeField] int numberOfNodes = 1000;
        [SerializeField] float nodeMaxDist = 50f;
        [SerializeField] int chunkSize = 10;
        [SerializeField] int chunkNoise = 2;
        [SerializeField] float drawDistance = 2f;

        List<Node> nodes = new List<Node>();
        List<Node> path = new List<Node>();
        List<Vector3> pass = new List<Vector3>();

        private void Start()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            tileMap.ClearAllTiles();
            SpawnNodes();
            ConnectNodes();
            FindPath();
            GeneratePass();

            watch.Stop();
            print("Generated pass in: " + (watch.ElapsedMilliseconds / 1000f) + "seconds");

            CreatePassData();
            PopulatePass();
        }

        private void PopulatePass()
        {
            FindObjectOfType<PropSpawner>().PopulatePass(pass);
        }

        private void CreatePassData()
        {
            foreach (Node pos in path)
            {
                pass.Add(pos.position);
            }
        }

        private void SpawnNodes()
        {
            //spawn nodes
            while (numberOfNodes > 0)
            {
                var x = Random.Range(-passWidth / 2, passWidth / 2);
                var y = Random.Range(-passHeight / 2, passHeight / 2);
                var randomPos = new Vector3(x, y, 1f);
                var newNode = new Node(randomPos);
                nodes.Add(newNode);
                numberOfNodes--;
            }
        }

        private void ConnectNodes()
        {
            foreach(Node node in nodes)
            {
                node.links = node.FindNodesNear(nodes, nodeMaxDist);
            }
        }

        private void FindPath()
        {
            try
            {
                //Find start and end nodes
                Node startNode;
                Node endNode;
                while (true)
                {
                    startNode = nodes[UnityEngine.Random.Range(0, nodes.Count)];
                    endNode = nodes[UnityEngine.Random.Range(0, nodes.Count)];
                    if (Vector3.Distance(startNode.position, endNode.position) >= passLength)
                    {
                        break;
                    }
                }

                //Breadth first search
                Queue<Node> nodeStack = new Queue<Node>();
                Node currentNode;
                nodeStack.Enqueue(startNode);
                startNode.isExplored = true;
                while (nodeStack.Count > 0)
                {
                    currentNode = nodeStack.Dequeue();
                    if (currentNode == endNode)
                    {
                        break;
                    }
                    foreach (Node link in currentNode.links)
                    {
                        if (!link.isExplored)
                        {
                            link.isExplored = true;
                            link.exploredFrom = currentNode;
                            nodeStack.Enqueue(link);
                        }
                    }
                }

                //build path
                currentNode = endNode;
                while (currentNode != startNode)
                {
                    path.Add(currentNode);
                    currentNode = currentNode.exploredFrom;
                }
                path.Add(startNode);
                path.Reverse();
            }
            catch
            {
                Debug.LogError("Failed to generate pass...");
            }
            //clear nodes
            nodes = null;
        }

        private void GeneratePass()
        {
            Vector3 drawPoint = path[0].position;
            Node destinationNode = path[1];
            int index = 1;

            while ( index < path.Count - 1)
            {
                if(Vector3.Distance(drawPoint, destinationNode.position) > drawDistance)
                {
                    FillRect(Vector3Int.RoundToInt(drawPoint), chunkSize+Random.Range(0, chunkNoise+1), chunkSize + Random.Range(0, chunkNoise + 1));
                    drawPoint += (destinationNode.position - drawPoint).normalized * drawDistance;
                }
                else
                {
                    index++;
                    drawPoint = destinationNode.position;
                    destinationNode = path[index];
                }
            }
        }

        private void FillRect(Vector3Int position, int width, int height)
        {
            for(int x = -width/2; x<width/2; x++)
            {
                for(int y = -height/2; y<height/2; y++)
                {
                    Vector3Int offSet = new Vector3Int(x, y, 0);
                    tileMap.SetTile(position + offSet, tile);
                }
            }
        }

        //public methods

        public List<Vector3> GetPass()
        {
            return pass;
        }

        public float GetChunkSize()
        {
            return chunkSize;
        }

        //private node class
        private class Node
        {
            public Vector3 position;
            public bool isExplored = false;
            public Node exploredFrom = null;
            public List<Node> links = new List<Node>();

            public Node(Vector3 pos)
            {
                position = pos;
            }

            public List<Node> FindNodesNear(List<Node> nodes, float maxDist)
            {
                List<Node> selectedNodes = new List<Node>();

                foreach(Node node in nodes)
                {
                    if(node == this)
                    {
                        continue;
                    }
                    if(Vector3.Distance(position, node.position) <= maxDist)
                    {
                        selectedNodes.Add(node);
                    }
                }

                return selectedNodes;
            }

            public List<Node> FindNearestNodes(List<Node> nodes, int max)
            {
                int i;
                float[] nearest = new float[max];
                List<Node> selectedNodes = new List<Node>();
                for (i=0; i<max; i++)
                {
                    nearest[i] = Mathf.Infinity;
                    selectedNodes.Add(null);
                }

                foreach(Node node in nodes)
                {
                    if(node == this)
                    {
                        continue;
                    }
                    foreach(Node link in node.links)
                    {
                        if (link == node)
                            continue;
                    }
                    float dist = Vector3.Distance(position, node.position);
                    for (i=0; i<max; i++)
                    {
                        if (dist < nearest[i])
                        {
                            nearest[i] = dist;
                            selectedNodes[i] = node;
                            break;
                        }
                    }
                }
                return selectedNodes;
            }
        }

        //gizmos
        private void OnDrawGizmos()
        {
            //Gizmos.color = Color.blue;
            //foreach(Node node in nodes)
            //{
            //    Gizmos.DrawSphere(node.position, 2f);
            //    Gizmos.DrawWireSphere(node.position, nodeMaxDist);
            //    foreach(Node link in node.links)
            //    {
            //        Gizmos.DrawLine(node.position, link.position);
            //    }
            //}
            if (path.Count <= 0) { return; }

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(path[0].position, 2f);
            for (int i = 1; i < path.Count; i++)
            {
                Gizmos.DrawSphere(path[i].position, 2f);
                Gizmos.DrawLine(path[i].position, path[i].exploredFrom.position);
            }
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(path[0].position, 3f);
            Gizmos.DrawSphere(path[path.Count-1].position, 3f);
        }
    }
}
