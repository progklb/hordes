using System.Collections.Generic;

using UnityEngine;

namespace Hordes
{
    public class HordeController : MonoBehaviour
    {
        #region CONSTANTS
        private const int STARTING_ROW = 1;
        private const int STARTING_COL = 0;
        #endregion


        #region PROPERTIES
        private int colCapacity { get { return m_CurrentRow + 1; } }
		private Ammunition lastAmmo { get { return m_Horde.Count > 0 ? m_Horde[m_Horde.Count - 1] : null; } }
        #endregion


        #region VARIABLES
        public float m_RowSpacing = 1;
        public float m_ColSpacing = 1;

        private List<Ammunition> m_Horde = new List<Ammunition>();
        private int m_CurrentRow = STARTING_ROW;
        private int m_CurrentCol = STARTING_COL;

		private bool m_AttackReady = false;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            Ammunition.onDestroyed += HandleSlavedDestroyed;
        }
        #endregion


        #region PUBLIC API
        public void Collect(Ammunition ammo)
        {
            if (!ammo.isCollected)
            {
                ammo.Collect();
                m_Horde.Add(ammo);

                ResetFormation();
            }
		}
		#endregion


		#region PUBLIC API - ATTACKING
		public void SetAttackReady(bool ready)
		{
			m_AttackReady = ready;

            foreach (var ammo in m_Horde)
			{
				ammo.SetAttackReady(ready);
			}

			if (lastAmmo != null)
			{
				lastAmmo.SetAttackSet(ready);
			}
		}

		public void Attack(Vector3 targetPos)
		{
			if (m_AttackReady)
			{
				// Shoot
				if (lastAmmo != null)
				{
					lastAmmo.Attack(targetPos);
					m_Horde.RemoveAt(m_Horde.Count - 1);
					RecalculatePreviousPosition();

					if (lastAmmo != null)
					{
						// Select next round for shooting
						lastAmmo.SetAttackSet(true);
					}
				}
			}
		}
		#endregion


		#region HELPER FUNCTIONS
        void HandleSlavedDestroyed(Ammunition slave)
        {
            m_Horde.Remove(slave);
            ResetFormation();
        }
        #endregion


        #region HELPER FUNCTIONS - FORMATION
		void RecalculatePreviousPosition()
		{
			if (m_CurrentCol > 0)
			{
				m_CurrentCol--;
			}
			else
			{
				m_CurrentRow--;
				m_CurrentCol = colCapacity - 1;
			}
		}

		/// <summary>
		/// Returns a vector that represents the offset to a target body by following it in a delta formation.
		/// </summary>
		/// <returns>Offset to parent.</returns>
		Vector3 GetNextPosition()
        {
			var rowOffset = -(m_CurrentRow * m_RowSpacing);

            var rowWidth = (colCapacity - 1) * m_ColSpacing;
            var rowStart = -(rowWidth / 2);

            var colOffset = rowStart + (m_CurrentCol++ * m_ColSpacing);

            if (m_CurrentCol >= colCapacity)
            {
                m_CurrentCol = 0;
                m_CurrentRow++;
            }

            return new Vector3(colOffset, 0, rowOffset);
        }

        void ResetFormation()
        {
            m_CurrentRow = STARTING_ROW;
            m_CurrentCol = STARTING_COL;

            foreach (var ammo in m_Horde)
            {
                ammo.Initialise(transform, GetNextPosition());

                lastAmmo.SetAttackReady(m_AttackReady);
                lastAmmo.SetAttackSet(m_AttackReady && ammo == lastAmmo);
            }
        }
        #endregion
    }
}
