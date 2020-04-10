using UnityEngine;

namespace ArcticPass.Core
{
    public class DepthSorting : MonoBehaviour
    {
        [SerializeField] bool runOnce = false;

        SpriteRenderer render;

        private void Start()
        {
            render = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
        {
            render.sortingOrder = 1000 + Mathf.RoundToInt(-transform.position.y * 32);

            if (runOnce)
                Destroy(this);
        }
    }
}
