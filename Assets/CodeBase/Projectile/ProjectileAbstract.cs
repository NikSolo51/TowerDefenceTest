using CodeBase.Infrastructure.ObjectsPool;
using UnityEngine;

namespace CodeBase.Projectile
{
    public abstract class ProjectileAbstract : MonoBehaviour
    {
        public abstract void SetPool(ObjectPool<ProjectileAbstract> pool);
    }
}