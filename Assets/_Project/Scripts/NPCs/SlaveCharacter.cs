using UnityEngine;

namespace Hordes
{
    public class SlaveCharacter : Character
    {
        #region VARIABLES
        public float m_FollowSmoothing = 5;
        public float m_LookSmoothing = 5;

		private Transform m_Transform;

        private Transform m_FollowTarget;
        private Vector3 m_FollowOffset;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            m_Transform = transform;
        }

        void FixedUpdate()
        {
            m_Components.m_Rigidbody.position = Vector3.Lerp(
                m_Components.transform.position,
                m_FollowTarget.TransformPoint(m_FollowOffset),
                Time.deltaTime * m_FollowSmoothing);

            m_Components.transform.forward = Vector3.Lerp(m_Components.transform.forward, m_FollowTarget.forward, Time.deltaTime * m_LookSmoothing);
        }
        #endregion

        #region PUBLIC API
        public void Initialise(Transform followTarget, Vector3 followOffset = default(Vector3))
        {
            m_FollowTarget = followTarget;
            m_FollowOffset = followOffset;
        }

        public void ReadyAttack()
        {
            // Spin
        }

        public void Attack()
        {
            // Shoot at target
        }
        #endregion
    }
}

