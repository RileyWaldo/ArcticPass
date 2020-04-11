using ArcticPass.Control;
using ArcticPass.Audio;
using UnityEngine;

namespace ArcticPass.AI
{
    public class Eagle : MonoBehaviour
    {
        [SerializeField] float speed = 2f;
        [SerializeField] float destroyTime = 10f;
        [SerializeField] AudioClip eagleClip = null;

        Vector3 direction;
        float timerDestroy = 0f;

        private void Start()
        {
            AudioController.Get().PlaySound(eagleClip);

            Vector3 playerPos = PlayerController.GetPlayer().transform.position;
            direction = playerPos - transform.position;
            direction.Normalize();

            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        private void Update()
        {
            timerDestroy += Time.deltaTime;
            if(timerDestroy >= destroyTime)
            {
                Destroy(gameObject);
            }

            transform.position += direction * speed * Time.deltaTime;
        }

    }
}
