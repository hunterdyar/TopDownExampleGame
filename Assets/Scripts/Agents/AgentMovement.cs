using System;
using UnityEngine;

namespace Agents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AgentMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        [HideInInspector] public Vector2 movementDir;
        [HideInInspector] public Vector2 facingDir;
        [Header("Configuration")]
        [SerializeField]
        [Min(0)]
        [Tooltip("The Movement Speed in Units per Seconds")]
        private float velocity = 10;

        [Header("Dash Settings")] [SerializeField]
        private float dashDistance;
        
        [SerializeField] private float dashCooldownTime;
        private float _timeSinceLastDash;
        private bool _canDash;
        [SerializeField] private ContactFilter2D dashContactFilter;

        // [SerializeField] private float dashCooldown;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _canDash = true;
        }

        public void Move(Vector2 direction)
        {
            movementDir = direction;
        }

        public void RotateTo(Vector2 direction)
        {
            //smooth this?
            facingDir = direction;
        }

        public void SetCanDash(bool canDash)
        {
            _canDash = canDash;
        }
        public void Dash(Vector2 direction)
        {
            if (!_canDash || _timeSinceLastDash < dashCooldownTime)
            {
                //dont dash
                return;
            }

            RaycastHit2D[] results = new RaycastHit2D[5];
            if (_rigidbody.Cast(direction, dashContactFilter, results, dashDistance) > 0)
            {
                if (results[0].distance < 0.01f)
                {
                    //should this count as a dash?
                    //if not, return.
                    //return;
                }

                //hit a wall
                //transform.position = results[0].centroid;
                transform.position = transform.position + (Vector3)direction.normalized*results[0].distance;
            }
            else
            {
                //hit nothing
                transform.position = transform.position + (Vector3)direction.normalized * dashDistance;
            }

            _timeSinceLastDash = 0;
        }

        void Update()
        {
            _timeSinceLastDash = _timeSinceLastDash + Time.deltaTime;
        }

        private void FixedUpdate()
        {
            Vector2 delta = movementDir * velocity * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position+delta);
            float angle = Vector2.SignedAngle(Vector2.right, facingDir);
            _rigidbody.MoveRotation(angle);
        }
    }
}
