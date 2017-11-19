﻿using UnityEngine;

namespace Hordes
{
    public class SlaveCharacter : Character
    {
		#region CONSTANTS
		private const string ANIM_ATTACK_READY = "AttackReady";
		private const string ANIM_ATTACK_SET = "AttackSet";
		private const string ANIM_ATTACK = "Attack";
		#endregion


		#region PROPERTIES
		public bool isAttacking { get; private set; }
		#endregion


		#region VARIABLES
		public Animator m_Animator;
		public GameObject m_SlaveDestroyedEffect;

		public float m_FollowSmoothing = 5f;
        public float m_LookSmoothing = 5f;

		public float m_AttackSpeed = 100f;

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
			if (isAttacking)
			{
				return;
			}

            m_Components.m_Body.position = Vector3.Lerp(
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

		public void SetAttackReady(bool ready)
        {
			m_Animator.SetBool(ANIM_ATTACK_READY, ready);
		}
		public void SetAttackSet(bool set)

		{
			m_Animator.SetBool(ANIM_ATTACK_SET, set);
		}

		public void Attack(Vector3 targetPosition)
        {
			// Shoot at target
			isAttacking = true;
			m_Components.m_Body.velocity = (targetPosition - m_Components.m_Body.position).normalized * m_AttackSpeed;
			m_Components.m_Body.drag = 0;

			m_Animator.SetBool(ANIM_ATTACK, true);
		}

		public override void HandleOnTriggerEnter(Collider col)
		{
            if (col.gameObject.layer == Layers.ENVIRONMENT)
			{
				if (isSlave)
				{
					DestroySelf();
				}
			}
		}

		public void DestroySelf()
		{
			var effect = Instantiate(m_SlaveDestroyedEffect, EffectsManager.instance.transform);
			effect.transform.position = m_Components.m_Body.position;
			effect.transform.rotation = m_Components.m_Body.rotation;

            Destroy(m_Components.gameObject);
		}
		#endregion
	}
}
