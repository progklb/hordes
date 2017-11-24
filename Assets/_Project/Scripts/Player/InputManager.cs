using UnityEngine;

namespace Hordes
{
    public class InputManager : MonoBehaviour
    {
        #region VARIABLES
        public CameraInput m_CamInput;
		public PlayerController m_Player;

        /// Reverses movement based on the facing direction of the player with regards to the camera.
        public bool m_ReverseInputWithDir = true;
        #endregion


        #region UNITY EVENTS
        void Update()
        {
            IsFacingCamera();

			ProcessMovement();
			ProcessAbilities();

            if (Input.GetKeyDown(KeyCode.T) && Input.GetKey(KeyCode.LeftShift))
            {
                Time.timeScale = Time.timeScale > 0.1f ? 0.1f : 1f;
            }
		}
		#endregion


		#region HELPER FUNCTIONS
		void ProcessMovement()
		{
			m_Player.SetSprinting(Input.GetKey(KeyCode.LeftShift));

            if (Input.GetKey(KeyCode.W))
            {
                m_Player.Move(m_Player.m_Body.transform.forward);
            }
			if (Input.GetKey(KeyCode.A))
			{
                m_Player.Move(m_Player.m_Body.transform.right * (IsFacingCamera() && m_ReverseInputWithDir ? 1f : -1f));
			}
			if (Input.GetKey(KeyCode.S))
			{
                m_Player.Move(-m_Player.m_Body.transform.forward);
			}
			if (Input.GetKey(KeyCode.D))
			{
                m_Player.Move(m_Player.m_Body.transform.right * (IsFacingCamera() && m_ReverseInputWithDir ? -1f : 1f));
			}

			m_Player.SetLookDirection(m_CamInput.pointerPosition - m_Player.transform.position);

			return;
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
				m_Player.Attack(m_CamInput.pointerPosition);
			}
		}
		#endregion


        #region HELPER FUNCTIONS
        bool IsFacingCamera()
        {
            var dot = Vector3.Dot(m_Player.facingDir, m_CamInput.cameraFacingDir);
            return dot <= 0f;
        }
        #endregion
	}
}