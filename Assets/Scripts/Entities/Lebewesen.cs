using UnityEngine;

namespace Assets.Scripts.Entities
{
    public abstract class Lebewesen : MonoBehaviour
    {
        #region Properties

        [field: SerializeField]
        protected Animator Animator { get; set; }

        [field: SerializeField]
        private int Sprungkraft { get; set; }

        [field: SerializeField]
        protected int Laufgeschwindigkeit { get; set; }

        [field: SerializeField]
        protected Rigidbody2D Rigidbody2D { get; set; }

        [field: SerializeField]
        private Transform Transform { get; set; }

        protected bool GucktNachRechts { get; set; }

        [field: SerializeField]
        public int Leben { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Bewegt das Objekt auf der Y-Achse nach oben. Innerhalb der Methode wird die Laufgeschwindigkeit mit eingerechnet
        /// </summary>
        protected void Springen()
        {
            if (this.Rigidbody2D.linearVelocityY == 0)
            {
                this.Rigidbody2D.linearVelocityY = this.Sprungkraft;
            }
        }

        /// <summary>
        /// Bewegt das Objekt auf der X-Achse (links, rechts). Innerhalb der Methode wird die Laufgeschwindigkeit mit eingerechnet
        /// </summary>
        protected void Bewegen(Vector2 target)
        {
            this.Rigidbody2D.linearVelocityX = target.x * this.Laufgeschwindigkeit;
            this.UpdateBlickrichtung();
        }

        protected void UpdateAnimationsVariablen()
        {
            this.Animator.SetBool("InBewegungX", this.Rigidbody2D.linearVelocityX != 0);
            this.Animator.SetBool("InBewegungY", this.Rigidbody2D.linearVelocityY != 0);
        }

        private void UpdateBlickrichtung()
        {
            if (this.Rigidbody2D.linearVelocityX > 0 && this.GucktNachRechts || this.Rigidbody2D.linearVelocityX < 0 && !this.GucktNachRechts)
            {
                this.GucktNachRechts = !this.GucktNachRechts;

                var currentScale = this.Transform.localScale;
                this.Transform.localScale = new Vector3(currentScale.x * -1, currentScale.y, currentScale.z);
            }
        }

        #endregion
    }
}
