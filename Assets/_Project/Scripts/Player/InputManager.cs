using UnityEngine;

namespace Hordes
{
    public class InputManager : MonoBehaviour
    {
        #region VARIABLES
        public CameraInput m_CamInput;
		public PlayerController m_Player;

		public static System.Action<InputManager> man = delegate { }; 
        #endregion


        #region UNITY EVENTS
        void Update()
        {
			ProcessMovement();
			ProcessAbilities();
		}
		#endregion


		#region HELPER FUNCTIONS
		void ProcessMovement()
		{
			m_Player.SetSprinting(Input.GetKey(KeyCode.LeftShift));

			NewMethod();
			if (Input.GetKey(KeyCode.A))
			{
				m_Player.Move(-m_Player.m_Body.transform.right);
			}
			if (Input.GetKey(KeyCode.S))
			{
				m_Player.Move(-m_Player.m_Body.transform.forward);
			}
			if (Input.GetKey(KeyCode.D))
			{
				m_Player.Move(m_Player.m_Body.transform.right);
			}

			m_Player.SetLookDirection(m_CamInput.PointerPosition - m_Player.transform.position);

			return;
		}

		private void NewMethod()
		{
			if (Input.GetKey(KeyCode.W))
			{
				m_Player.Move(m_Player.m_Body.transform.forward);
			}
		}

		void ProcessAbilities()
		{
			if (Input.GetMouseButtonDown(1))
			{
				m_Player.SetAttackReady(true);
			}
			else if (Input.GetMouseButtonUp(1))
			{
				m_Player.SetAttackReady(false);
			}

			if (Input.GetMouseButtonDown(0))
			{
				m_Player.Attack(m_CamInput.PointerPosition);
			}
		}
		#endregion
	}
}