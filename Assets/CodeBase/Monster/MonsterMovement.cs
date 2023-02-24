using UnityEngine;

namespace CodeBase.Monster
{
    public class MonsterMovement : MonoBehaviour
    {
        private float m_speed = 0.1f;
        private Vector3 m_moveTarget;

        public void Construct(Vector3 moveTarget, float speed)
        {
            m_moveTarget = moveTarget;
            m_speed = speed;
        }

        void Update()
        {
            if (m_moveTarget == null)
                return;

            Vector3 translation = m_moveTarget - transform.position;
            if (translation.sqrMagnitude > m_speed)
            {
                translation = translation.normalized * m_speed * Time.deltaTime;
            }

            transform.Translate(translation);
        }

        public float GetSpeed()
        {
            return m_speed;
        }
    }
}