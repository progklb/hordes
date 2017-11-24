using UnityEngine;

namespace Hordes
{
    public class CameraInput : MonoBehaviour
    {
        #region PROPERTIES
        public Vector3 cameraFacingDir { get { return m_CameraTransform.forward; } }
        public Vector3 pointerPosition { get; private set; }
        #endregion


        #region VARIABLES
        [SerializeField]
        private Camera m_Camera;
        private Transform m_CameraTransform;

        [SerializeField]
        private bool m_DrawDebugIndicator;
        private GameObject m_DebugIndicator;

        private Ray m_Ray;
        private RaycastHit m_Hit;
        #endregion

		
        #region UNITY EVENTS
        void Start()
        {
            m_CameraTransform = m_Camera.transform;

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
                pointerPosition = m_Hit.point;
            }

            if (m_DrawDebugIndicator)
            {
                m_DebugIndicator.transform.position = pointerPosition;
            }
        }
        #endregion
    }
}
