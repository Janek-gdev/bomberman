using UnityEngine;

namespace Bomberman.Pooling
{
    /// <summary>
    /// Handles spawning of a prefab for pooling purposes
    /// </summary>
    /// <typeparam name="T">The type of object to spawn</typeparam>
    public abstract class Pool<T> : ScriptableObject where T : MonoBehaviour
    {
        [SerializeField] private T _poolableObject;

        //todo implement pooling, this just gives us an interface we can replace later
        
        public T Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            return Instantiate(_poolableObject, position, rotation, parent);
        }
        public T Spawn(Vector3 position, Quaternion rotation)
        {
            return Instantiate(_poolableObject, position, rotation);
        }

        public void ReturnToPool(T poolable)
        {
            Destroy(poolable.gameObject);
        }
    }
}