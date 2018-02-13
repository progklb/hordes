using UnityEngine;

using System;

namespace Hordes
{
	[RequireComponent(typeof(Rigidbody))]
    public class Ammunition : MonoBehaviour
    {
        #region CONSTANTS
        protected const string ANIM_ATTACK_READY = "AttackReady";
        protected const string ANIM_ATTACK_SET = "AttackSet";
        protected const string ANIM_ATTACK = "Attack";
        #endregion


        #region EVENTS
        public static event Action<Ammunition> onDestroyed = delegate { };

		public event Action onCollected = delegate { };
		#endregion


		#region PROPERTIES
		/// Has this item been collected by the player?
		public bool isCollected { get; private set; }
        /// Has this item been shot?
        public bool isFired { get; private set; }
		/// The damage value that this ammunition deals
		public int damage { get { return m_Damage; } }
		/// The <see cref="Rigibody"/> of this instance.
		public Rigidbody body { get { return m_Body; } }
		#endregion


		#region VARIABLES
		[SerializeField] private GameObject m_UncollectedModel;
		[SerializeField] private GameObject m_CollectedModel;
		[SerializeField] private Animator m_Animator;

		[Space(10)]
		[Header("Optional Overrides")]
		[SerializeField] private GameObject m_SpawnedEffect;
		[SerializeField] private GameObject m_CollectedEffect;
		[SerializeField] private GameObject m_DestroyedEffect;

		[Space(10)]
		[SerializeField] private float m_FollowSmoothing = 5f;
		[SerializeField] private int m_Damage = 1;

		private Transform m_FollowTarget;
        private Vector3 m_FollowOffset;

		protected Rigidbody m_Body;
		protected Transform m_Transform;
		#endregion


		#region UNITY EVENTS
		protected virtual void Start()
        {
			m_Body = GetComponent<Rigidbody>();
            m_Transform = transform;

			// Apply default values if there are no overrides
			if (m_SpawnedEffect == null)	m_SpawnedEffect = AssetProvider.instance.ammoSpawnedEffectPrefab;
			if (m_CollectedEffect == null)	m_CollectedEffect = AssetProvider.instance.ammoCollectedEffectPrefab;
			if (m_DestroyedEffect == null)	m_DestroyedEffect = AssetProvider.instance.ammoDestroyedEffectPrefab;

			EffectsManager.instance.Instantiate(m_SpawnedEffect, transform.position, transform.rotation);
		}

        protected virtual void OnDestroy()
        {
            onDestroyed(this);
        }

        void FixedUpdate()
        {
			if (isFired || !Player.isAlive)
            {
                return;
            }

            if (isCollected && m_FollowTarget != null)
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

			EffectsManager.instance.Instantiate(m_CollectedEffect, m_Body.position);

            isCollected = true;
            onCollected();

            return this;
        }

        public virtual void Initialise(Transform followTarget, Vector3 followOffset = default(Vector3))
        {
            m_FollowTarget = followTarget;
            m_FollowOffset = followOffset;
        }

        public virtual void DestroySelf()
        {
            var effect = Instantiate(m_DestroyedEffect, EffectsManager.instance.transform);
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
            isFired = true;
            m_Animator.SetBool(ANIM_ATTACK, true);
        }

        public virtual void HandleOnTriggerEnter(Collider col) { }
        #endregion
	}
}
