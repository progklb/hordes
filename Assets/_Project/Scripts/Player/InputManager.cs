using UnityEngine;

namespace Hordes
{
    public class InputManager : MonoBehaviour
    {
		#region PROPERTIES
		public Player player {  get { return Player.instance; } }
		#endregion


		#region VARIABLES
		[SerializeField] private CameraInput m_CamInput;

		/// Reverses movement based on the facing direction of the player with regards to the camera.
		[SerializeField] private bool m_ReverseInputWithDir = true;
        #endregion


        #region UNITY EVENTS
        void Update()
        {
			if (player == null)
			{
				return;
			}

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
			player.SetSprinting(Input.GetKey(KeyCode.LeftShift));

            if (Input.GetKey(KeyCode.W))
            {
				player.Move(player.body.transform.forward);
            }
			if (Input.GetKey(KeyCode.A))
			{
				player.Move(player.body.transform.right * (IsFacingCamera() && m_ReverseInputWithDir ? 1f : -1f));
			}
			if (Input.GetKey(KeyCode.S))
			{
				player.Move(-player.body.transform.forward);
			}
			if (Input.GetKey(KeyCode.D))
			{
				player.Move(player.body.transform.right * (IsFacingCamera() && m_ReverseInputWithDir ? -1f : 1f));
			}

			player.SetLookDirection(m_CamInput.pointerPosition - player.transform.position);

			return;
		}

		void ProcessAbilities()
		{
			if (Input.GetMouseButtonDown(1))
			{
				player.SetAttackReady(true);
			}
			else if (Input.GetMouseButtonUp(1))
			{
				player.SetAttackReady(false);
			}

			if (Input.GetMouseButtonDown(0))
			{
				player.Attack(m_CamInput.pointerPosition);
			}
		}
		#endregion


        #region HELPER FUNCTIONS
        bool IsFacingCamera()
        {
            var dot = Vector3.Dot(player.facingDir, m_CamInput.cameraFacingDir);
            return dot <= 0f;
        }
        #endregion
	}
}