using UnityEngine;

namespace ArcticPass.Control
{
    public class PlayerController : MonoBehaviour
    {
        //tunables
        [SerializeField] float moveSpeed = 4f;
        [SerializeField] float sprintFactor = 2f;
        [SerializeField] float friction = 1f;

        //cached referances
        Animator animator;
        Rigidbody2D rigidBody;

        const string horizontal = "Horizontal";
        const string verticle = "Vertical";

        Vector2 inputAxis = Vector2.zero;
        Vector2 inputLast = Vector2.zero;
        float inputSprint = 0;

        //static vars

        static PlayerController player;

        private void Awake()
        {
            player = this;
        }

        public static PlayerController GetPlayer()
        {
            return player;
        }

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            GetInput();
            UpdatePosition();
            Attack();
            Animate();
        }

        private void GetInput()
        {
            inputAxis.x = Input.GetAxisRaw(horizontal);
            inputAxis.y = Input.GetAxisRaw(verticle);
            if (inputAxis != Vector2.zero)
            {
                inputLast = inputAxis;
            }
            inputSprint = Input.GetAxis("Sprint");
        }


        private void Animate()
        {
            animator.speed = 1f;
            animator.SetFloat(horizontal, inputLast.x);
            animator.SetFloat(verticle, inputLast.y);
            if (!animator.GetBool("attack") && rigidBody.velocity.magnitude <= 1f)
            {    
                animator.speed = 0f;
            }
        }

        private void UpdatePosition()
        {
            //get input direction
            Vector2 addVelocity = new Vector2(inputAxis.x, inputAxis.y);
            addVelocity.Normalize();
            //check if sprinting
            float addSprint = 1f;
            if (inputSprint >= Mathf.Epsilon)
            {
                addSprint = sprintFactor;
                addVelocity *= sprintFactor;
            }
            //friction
            if (addVelocity.magnitude <= Mathf.Epsilon)
            {
                addVelocity = -rigidBody.velocity.normalized * friction;
            }
            //move rigidbody
            rigidBody.velocity += addVelocity;
            if (rigidBody.velocity.magnitude > moveSpeed * addSprint)
            {
                rigidBody.velocity = rigidBody.velocity.normalized * moveSpeed * addSprint;
            }
        }

        private void Attack()
        {
            if (Input.GetAxis("Fire2") > 0.5f)
            {
                animator.SetBool("attack", true);
            }
        }

    }
}