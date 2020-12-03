using UnityEngine;

namespace B2Framework
{
    public class HUDObject : MonoBehaviour
    {
        protected Transform m_Holder;
        protected bool hasHolder = false;
        public float offsetY = 0;
        public Transform holder
        {
            set
            {
                m_Holder = value;
                hasHolder = m_Holder != null;
            }
            get { return m_Holder; }
        }
        protected virtual void Update()
        {
            if (hasHolder && m_Holder == null) Recycle();
            if (m_Holder != null)
            {
                var position = m_Holder.position;
                position.y += offsetY;
                transform.localPosition = HUD.Instance.WorldToCanvasPoint(position);
            }
        }
        public virtual void Recycle()
        {
            hasHolder = false;
            m_Holder = null;
            HUD.Instance.Recycle(this);
        }
    }
}