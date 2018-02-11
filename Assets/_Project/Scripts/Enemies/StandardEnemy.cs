namespace Hordes
{
    public class StandardEnemy : Enemy
    {
        #region UNITY EVENTS
        protected override void Update()
        {
            if (Player.isAlive)
            {
				m_Agent.isStopped = false;
				m_Agent.destination = Player.instance.transform.position;
            }
			else
			{
				m_Agent.isStopped = true;
			}
        }
        #endregion
    }
}
