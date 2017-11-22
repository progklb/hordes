using UnityEngine;

namespace Hordes
{
	[RequireComponent(typeof(HordeController))]
    public class PlayerController : MonoBehaviour
    {
        #region VARIABLES
        public HordeController m_HordeController;
		public Rigidbody m_Body;

		public float m_MovementSpeed = 500;
		public float m_SprintScaler = 2;

		private bool m_Sprinting;
		#endregion


		#region UNITY EVENTS
		void Start()
		{
			AmmunitionNotifier.onAmmoTouched += OnAmmoTouched;
		}
		#endregion


		#region EVENT HANDLERS
        void OnAmmoTouched(Ammunition ammo)
        {
            m_HordeController.Collect(ammo);
        }
		#endregion


		#region ATTACKING
		public void SetAttackReady(bool ready)
		{
			m_HordeController.SetAttackReady(ready);
		}

		public void Attack(Vector3 targetPos)
		{
			m_HordeController.Attack(targetPos);
		}
		#endregion


		#region MOVEMENT
		public void Move(Vector3 direction)
		{
			m_Body.AddForce(NormalizeMovement(direction));
		}

		public void SetSprinting(bool sprinting)
		{
			m_Sprinting = sprinting;
		}

		public void SetLookDirection(Vector3 direction)
		{
			m_Body.transform.forward = direction;
		}

		Vector3 NormalizeMovement(Vector3 force)
		{
			return force * m_MovementSpeed * Time.deltaTime * (m_Sprinting ? m_SprintScaler : 1f);
		}
		#endregion
	}
}
