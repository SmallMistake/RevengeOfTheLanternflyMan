using UnityEngine;

namespace GBJam.Player 
{ 
    public class PlayerStateMachine : StateMachine {

        internal float horizontalMove = 0f;
        internal float verticalMove = 0f;
        internal Rigidbody2D playerRigidbody;
        internal Vector3 lastPositionOnSolidGround;
        public float moveSpeed = 40f;
        internal FallScript fallScript;
        internal SpriteRenderer spriteRenderer;
        public Animator animator;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            fallScript = transform.parent.GetComponentInChildren<FallScript>();
            playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
            SetState(new RegularState(this));
        }

        void Update()
        {
            State.Update();
        }

        void FixedUpdate()
        {
            State.FixedUpdate();
        }

        public void fall()
        {
            SetState(new FallState(this));
        }

        public void respawnAtSSpot()
        {
            SetState(new RespawnState(this));
        }

        public void setToRegularState()
        {
            SetState(new RegularState(this));
        }

        public void Die()
        {
            FindObjectOfType<GameOverScript>().Activate();
            SetState(new DieState(this));
        }

    }
}
