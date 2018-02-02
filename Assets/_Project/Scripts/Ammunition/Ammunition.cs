using UnityEngine;

using System;

namespace Hordes
{
    public class Ammunition : MonoBehaviour
    {
        #region CONSTANTS
        protected const string ANIM_ATTACK_READY = "AttackReady";
        protected const string ANIM_ATTACK_SET = "AttackSet";
        protected const string ANIM_ATTACK = "Attack";
        #endregion


        #region EVENTS
        public static event Action<Ammunition> onDestroyed = delegate { };
        public static event Action<Ammunition> onCollected = delegate { };
        #endregion


        #region PROPERTIES
        /// Has this item been collected by the player?
        public bool isCollected { get; private set; }
        /// Has this item been shot?
        public bool isFired { get; private set; }
        #endregion


        #region VARIABLES
        public GameObject m_UncollectedModel;
        public GameObject m_CollectedModel;
        public GameObject m_AmmoDestroyedEffect;
        public GameObject m_AmmoCollectedEffect;

        public Rigidbody m_Body;
        public Animator m_Animator;

        [Space(10)]
        public float m_FollowSmoothing = 5f;
        public float m_LookSmoothing = 5f;

        public int m_Damage = 1;

        protected Transform m_Transform;

        public Transform m_FollowTarget;
        private Vector3 m_FollowOffset;
        #endregion


        #region UNITY EVENTS
        protected virtual void Start()
        {
            m_Transform = transform;
        }

        protected virtual void OnDestroy()
        {
            onDestroyed(this);
        }

        void FixedUpdate()
        {
			if (isFired)
            {
                return;
            }

            if (isCollected)
            {
				Debug.DrawLine(m_Body.position, m_FollowTarget.position, Color.red, Time.smoothDeltaTime);
				Debug.DrawRay(m_FollowTarget.position, Vector3.up, Color.red, Time.smoothDeltaTime);
				
				m_Body.position = Vector3.Lerp(
                    m_Body.position,
                    m_FollowTarget.TransformPoint(m_FollowOffset),
                    Time.deltaTime * m_FollowSmoothing);

				m_Body.rotation = m_FollowTarget.rotation;
            }
        }
        #endregion


        #region PUBLIC API
        public virtual Ammunition Collect()
        {
            m_UncollectedModel.SetActive(false);
            m_CollectedModel.SetActive(true);

            var effect = Instantiate(m_AmmoCollectedEffect, EffectsManager.instance.transform);
            effect.transform.position = m_Body.position;

            isCollected = true;
            onCollected(this);

            return this;
        }

        public virtual void Initialise(Transform followTarget, Vector3 followOffset = default(Vector3))
        {
            m_FollowTarget = followTarget;
            m_FollowOffset = followOffset;
        }

        public virtual void DestroySelf()
        {
            var effect = Instantiate(m_AmmoDestroyedEffect, EffectsManager.instance.transform);
            effect.transform.position = m_Body.position - m_Body.velocity.normalized * 2;
            effect.transform.rotation = m_Body.rotation;

            Destroy(gameObject);
        }
		#endregion


        #region HELPER FUNCTIONS
        public virtual void SetAttackReady(bool ready)
        {
            m_Animator.SetBool(ANIM_ATTACK_READY, ready);
        }

        public virtual void SetAttackSet(bool set)
        {
            m_Animator.SetBool(ANIM_ATTACK_SET, set);
        }

        public virtual void Attack(Vector3 targetPosition)
        {
            // Shoot at target
            isFired = true;
            m_Animator.SetBool(ANIM_ATTACK, true);
        }

        public virtual void HandleOnTriggerEnter(Collider col) { }
        #endregion
	}
}
