using UnityEngine;

namespace Hordes
{
    public class StandardAmmunition : Ammunition
    {
		#region VARIABLES
		public float m_AttackSpeed = 100f;
        #endregion


        #region UNITY EVENTS
        public void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.layer == Layers.ENVIRONMENT)
            {
                if (isCollected)
                {
                    DestroySelf();
                }
            }
        }
        #endregion


        #region INHERITED METHODS
        public override void Initialise(Transform followTarget, Vector3 followOffset = default(Vector3))
        {
            base.Initialise(followTarget, followOffset);
        }

        public override void SetAttackReady(bool ready)
        {
            base.SetAttackReady(ready);
		}

        public override void SetAttackSet(bool set)
		{
            base.SetAttackSet(set);
		}

        public override void Attack(Vector3 targetPosition)
        {
            base.Attack(targetPosition);

			m_Body.velocity = (targetPosition - m_Body.position).normalized * m_AttackSpeed;
			m_Body.drag = 0;
		}
		#endregion
	}
}

