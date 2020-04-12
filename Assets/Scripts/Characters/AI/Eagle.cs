using ArcticPass.Control;
using ArcticPass.Audio;
using UnityEngine;

namespace ArcticPass.AI
{
    public class Eagle : MonoBehaviour
    {
        [SerializeField] float speed = 2f;
        [SerializeField] float destroyTime = 10f;
        [SerializeField] float maxLoopTime = 3f;
        [SerializeField] AudioClip eagleClip = null;

        Vector3 direction;
        float timerDestroy = 0f;
        float loopTimer = 0f;
        bool loop = true;

        Animator animator;

        private void Start()
        {
            AudioController.Get().PlaySound(eagleClip);
            animator = GetComponentInChildren<Animator>();
            loopTimer = Random.Range(1f, maxLoopTime);

            Vector3 playerPos = PlayerController.GetPlayer().transform.position;
            direction = playerPos - transform.position;
            direction.Normalize();

            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        private void Update()
        {
            CleanUp();
            Animation();
            transform.position += direction * speed * Time.deltaTime;
        }

        private void CleanUp()
        {
            timerDestroy += Time.deltaTime;
            if (timerDestroy >= destroyTime)
            {
                Destroy(gameObject);
            }
        }

        private void Animation()
        {
            loopTimer -= Time.deltaTime;
            if (loopTimer <= 0)
            {
                loopTimer = Random.Range(1f, maxLoopTime);
                loop = !loop;
            }

            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                if (loop)
                    animator.Play(0);
            }
        }
    }
}
