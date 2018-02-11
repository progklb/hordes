using UnityEngine;

using System;

using Utilities;

namespace Hordes
{
	[RequireComponent(typeof(AmmoController))]
    public class Player : MonoBehaviour
    {
		#region EVENTS
		public static event Action onPlayerDeath = delegate { };
		#endregion


		#region PROPERTIES
		public static Player instance { get; private set; }
		public static bool isAlive {  get { return instance != null; } }

        public Vector3 facingDir { get { return m_Body.transform.forward; } }
        #endregion


        #region VARIABLES
        public AmmoController m_AmmoController;
		public Rigidbody m_Body;

		public GameObject m_DeathEffect;

		public float m_MovementSpeed = 500;
		public float m_SprintScaler = 2;
		private bool m_IsSprinting;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
            if (instance == null)
            {
                instance = this;

				AmmunitionNotifier.onAmmoTouched += OnAmmoTouched;
			}
			else
            {
                LogContext.LogErrorFormat(this, "There is more than one player instance in the scene!");
            }
		}

        void OnDestroy()
        {
            instance = null;

			AmmunitionNotifier.onAmmoTouched -= OnAmmoTouched;
		}
		#endregion


		#region EVENT HANDLERS
		void OnAmmoTouched(Ammunition ammo)
        {
            m_AmmoController.Collect(ammo);
        }
		#endregion


		#region ATTACKING
		public void SetAttackReady(bool ready)
		{
			m_AmmoController.SetAttackReady(ready);
		}

		public void Attack(Vector3 targetPos)
		{
			m_AmmoController.Attack(targetPos);
		}
		#endregion


		#region MOVEMENT
		public void Move(Vector3 direction)
		{
			m_Body.AddForce(NormalizeMovement(direction));
		}

		public void SetSprinting(bool sprinting)
		{
			m_IsSprinting = sprinting;
		}

		public void SetLookDirection(Vector3 direction)
		{
			m_Body.transform.forward = direction;
		}

		Vector3 NormalizeMovement(Vector3 force)
		{
			return force * m_MovementSpeed * Time.deltaTime * (m_IsSprinting ? m_SprintScaler : 1f);
		}
		#endregion


		#region OTHER
		public void TakeDamage()
		{
			onPlayerDeath();

			Instantiate(m_DeathEffect, transform.position, Quaternion.identity);
			Destroy(m_Body.gameObject);
		}
		#endregion
	}
}
