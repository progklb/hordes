namespace Hordes
{
    public class StandardEnemy : Enemy
    {
        #region UNITY EVENTS
        protected override void Update()
        {
            if (PlayerController.instance != null)
            {
                m_Agent.destination = PlayerController.instance.transform.position;
            }
        }
        #endregion
    }
}
