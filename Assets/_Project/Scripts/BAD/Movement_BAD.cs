using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subway.BAD
{
    [RequireComponent(typeof(Animator), typeof(CharacterController))]
    public class Movement_BAD : MonoBehaviour
    {
        [SerializeField] float _speed = 4f;
        public bool game_started = true;
        private Animator Animator;
        int CURRENTLINE = 0;
        private CharacterController cc;
        public bool a = true;
        private Vector3 velocity;
        float padaemKogdaSkorostYMenshe = 1.5f;
        [HideInInspector] public float g = -9.8f;
        float uskoreniePadeniya;
        [SerializeField]
        private bool ItIsTrueWhenWeJUMP;
        float JUMP_SPEED = 16f;

        void Start()
        {
            Animator = GetComponent<Animator>();
            cc = GetComponent<CharacterController>();

            velocity.z = _speed;
        }

        private void Update()
        {
            if (game_started == false || a != true || cc == null)
                return;

            float hor;
            bool space;

            ReadInput(out hor, out space);

            velocity.x = hor * _speed;

            ApplyGravity();
            ProcessJump(space);

            MovePlayer();
        }

        private static void ReadInput(out float hor, out bool space)
        {
            hor = Input.GetAxis("Horizontal");
            space = Input.GetKeyDown(KeyCode.Space);
        }

        private void ApplyGravity()
        {
            if (cc.isGrounded == false)
            {
                if (velocity.y < padaemKogdaSkorostYMenshe)
                    velocity.y += g * uskoreniePadeniya * Time.deltaTime;
                else
                    velocity.y += g * Time.deltaTime;
            }
            else if (ItIsTrueWhenWeJUMP == false)
            {
                velocity.y = g;
            }
        }

        private void ProcessJump(bool space)
        {
            if (space == true && ItIsTrueWhenWeJUMP == false)
            {
                velocity.y += JUMP_SPEED;
                ItIsTrueWhenWeJUMP = true;
            }
            else
            {
                ItIsTrueWhenWeJUMP = false;
            }
        }

        private void MovePlayer()
        {
            cc.Move(velocity * Time.deltaTime);
        }
    }
}

