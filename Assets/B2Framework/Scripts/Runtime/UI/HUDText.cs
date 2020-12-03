using TMPro;
using UnityEngine;

namespace B2Framework
{
    public class HUDText : HUDObject
    {
        [SerializeField] private Animator m_Animator;
        public Animator animator
        {
            get
            {
                if (m_Animator == null)
                {
                    m_Animator = GetComponentInChildren<Animator>();
                }
                return m_Animator;
            }
        }
        [SerializeField] private TextMeshProUGUI m_Text;

        public string text
        {
            get
            {
                return m_Text.text;
            }
            set
            {
                m_Text.text = value;
            }
        }
        public HUDText Play(string stateName)
        {
            // animator?.CrossFade(stateName, 0);
            animator?.Play(stateName);
            return this;
        }
        public HUDText Pause()
        {
            if (animator != null) animator.speed = 0;
            return this;
        }
        public HUDText Resume()
        {
            if (animator != null) animator.speed = 1;
            return this;
        }
        public bool IsPlaying
        {
            get
            {
                if (m_Animator == null) return false;
                var info = m_Animator.GetCurrentAnimatorStateInfo(0);
                // info.IsName("??");
                return info.normalizedTime < 1;
            }
        }
        protected override void Update()
        {
            base.Update();
            if (!IsPlaying) Recycle();
        }
    }
}