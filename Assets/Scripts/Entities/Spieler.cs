using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Entities
{
    public class Spieler : Lebewesen
    {
        private Vector2 AktuelleGeschwindigkeit { get; set; }

        private bool StoppeXBewegung { get; set; }

        // Update is called once per frame
        void Update()
        {
            // Beende die X Bewegung nur, falls nicht in der Luft
            if (this.StoppeXBewegung && this.Rigidbody2D.linearVelocityY == 0)
            {
                this.AktuelleGeschwindigkeit = new Vector2(0, 0);
                this.StoppeXBewegung = false;
            }

            this.Bewegen(this.AktuelleGeschwindigkeit);
            this.UpdateAnimationsVariablen();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed && this.Rigidbody2D.linearVelocityY == 0)
            {
                var moveInput = context.ReadValue<Vector2>();
                this.AktuelleGeschwindigkeit = new Vector2(moveInput.x, this.AktuelleGeschwindigkeit.y);
                this.StoppeXBewegung = false;
            }
            else
            {
                this.StoppeXBewegung = true;
            }
        }

        protected void Ducken()
        {
            if (this.Rigidbody2D.linearVelocityX > 0 && this.Rigidbody2D.linearVelocityY == 0)
            {
                this.Animator.SetBool("IstGeduckt", true);
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            this.Springen();
        }
    }
}
