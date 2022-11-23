namespace UnityEngine.Pool
{
    public class ObjectPooling
    {
        public ObjectPooling(GameObject obj, Transform parent = null, int maxSize = 20)
        {
            if (parent == null) parent = new GameObject(obj.name).transform;
            this.parent = parent;
            origin = obj;
            pool = new ObjectPool<GameObject>(Spawn,
                                                Get,
                                                ReleasePool,
                                                Destroy,
                                                maxSize: maxSize);
        }


        Transform parent;
        GameObject origin;
        private IObjectPool<GameObject> pool;

        GameObject Spawn()
        {
            GameObject obj = Object.Instantiate(origin, parent);
            obj.SetActive(false);
            return obj;
        }
        void Get(GameObject obj)
        {
            obj.SetActive(true);
        }
        void ReleasePool(GameObject obj)
        {
            obj.SetActive(false);
        }
        void Destroy(GameObject obj)
        {
            Object.Destroy(obj);
        }


        public GameObject Get()
        {
            return pool.Get();
        }
        public void Release(GameObject obj)
        {
            pool.Release(obj);
        }
    }

}