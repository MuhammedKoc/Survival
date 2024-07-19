using System;

namespace General.ObjectPool
{
    [Serializable]
    public class PoolObjectSpawnData
    {
        public PooledObject pooledObject;
        public int spawnCountOnStart;
    }
}