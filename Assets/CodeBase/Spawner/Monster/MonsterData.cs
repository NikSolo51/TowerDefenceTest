using CodeBase.Infrastructure.ObjectsPool;
using CodeBase.Monster;
using UnityEngine;

namespace CodeBase.Spawner.Monster
{
    public class MonsterData
    {
        public GameObject m_monsterGO;
        public IResetObject m_resetObject;
        public MonsterMovement m_monsterMovement;
        public MonsterHP m_monsterHP;
        public DeactivateMonsterByDistance m_deactivateMonsterByDistance;

        public MonsterData(GameObject monsterGO, IResetObject mResetObject, MonsterMovement mMonsterMovement,
            MonsterHP mMonsterHp, DeactivateMonsterByDistance mDeactivateMonsterByDistance)
        {
            m_monsterGO = monsterGO;
            m_resetObject = mResetObject;
            m_monsterMovement = mMonsterMovement;
            m_monsterHP = mMonsterHp;
            m_deactivateMonsterByDistance = mDeactivateMonsterByDistance;
        }
    }
}