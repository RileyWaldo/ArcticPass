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
            partSystem = GetComponentInChildren<ParticleSystem>();

            ScaleShape();
            SetEmitter();
            SetPosition();
        }

        private void ScaleShape()
        {
            ParticleSystem.ShapeModule psShape = partSystem.shape;
            psShape.scale = new Vector3(Camera.main.orthographicSize * Camera.main.aspect * 2, 2f);
        }

        private void SetEmitter()
        {
            ParticleSystem.EmissionModule psEmission = partSystem.emission;
            psEmission.rateOverTime = numberOfParticles;
        }

        private void SetPosition()
        {
            Camera cam = Camera.main;
            viewPos = new Vector3(cam.transform.position.x, cam.transform.position.y + cam.orthographicSize, 0);
            transform.parent = Camera.main.transform;
            transform.localPosition = viewPos;
        }
    }
}
