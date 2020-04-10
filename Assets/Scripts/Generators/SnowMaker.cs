using UnityEngine;

namespace ArcticPass.Generator
{
    public class SnowMaker : MonoBehaviour
    {
        [SerializeField] int numberOfParticles = 50;

        ParticleSystem partSystem;

        Vector3 viewPos;

        private void Start()
        {
            viewPos = new Vector3(Camera.main.pixelWidth/2, Camera.main.scaledPixelHeight, 0);
            partSystem = GetComponentInChildren<ParticleSystem>();
            ParticleSystem.ShapeModule psShape = partSystem.shape;
            psShape.scale = new Vector3(Camera.main.orthographicSize * Camera.main.aspect * 2, 2f);
        }

        private void LateUpdate()
        {
            transform.position = Camera.main.ScreenToWorldPoint(viewPos);
        }
    }
}
