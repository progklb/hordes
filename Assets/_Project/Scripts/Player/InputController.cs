using UnityEngine;

namespace Hordes
{
    public class InputController : MonoBehaviour
    {
        #region VARIABLES
        public CameraInput m_CamInput;
        public Rigidbody m_PlayerBody;

        public float m_MovementSpeed = 500;
        public float m_SprintScaler = 2;

        private bool m_Sprinting;
        #endregion


        #region UNITY EVENTS
        void Update()
        {
            m_Sprinting = Input.GetKey(KeyCode.LeftShift);

            if (Input.GetKey(KeyCode.W))
            {
                m_PlayerBody.AddForce(NormalizeMovement(m_PlayerBody.transform.forward));
            }
            if (Input.GetKey(KeyCode.A))
            {
                m_PlayerBody.AddForce(NormalizeMovement(-m_PlayerBody.transform.right));
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_PlayerBody.AddForce(NormalizeMovement(-m_PlayerBody.transform.forward));
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_PlayerBody.AddForce(NormalizeMovement(m_PlayerBody.transform.right));
            }

            m_PlayerBody.transform.forward = m_CamInput.PointerPosition - m_PlayerBody.transform.position;
        }
        #endregion


        #region HELPER FUNCTIONS
        Vector3 NormalizeMovement(Vector3 force)
        {
            return force * m_MovementSpeed * Time.deltaTime * (m_Sprinting ? m_SprintScaler : 1f);
        }
        #endregion
    }
}