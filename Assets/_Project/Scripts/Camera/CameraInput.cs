using UnityEngine;

namespace Hordes
{
    public class CameraInput : MonoBehaviour
    {
        #region PROPERTIES
        public Vector3 PointerPosition { get; private set; }
        #endregion


        #region VARIABLES
        [SerializeField]
        private Camera m_Camera;

        [SerializeField]
        private bool m_DrawDebugIndicator;
        private GameObject m_DebugIndicator;

        private Ray m_Ray;
        private RaycastHit m_Hit;
        #endregion


        #region UNITY EVENTS
        void Start()
        {
            if (m_DrawDebugIndicator)
            {
                m_DebugIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                m_DebugIndicator.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                var collider = m_DebugIndicator.GetComponent<Collider>();
                if (collider != null)
                {
                    Destroy(collider);
                }
            }
        }

        void Update()
        {
            m_Ray = m_Camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

            if (Physics.Raycast(m_Ray, out m_Hit) )
            {
                PointerPosition = m_Hit.point;
            }

            if (m_DrawDebugIndicator)
            {
                m_DebugIndicator.transform.position = PointerPosition;
            }
        }
        #endregion


    }
}
