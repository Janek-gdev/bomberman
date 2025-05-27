using UnityEngine;

namespace Bomberman.Pooling
{
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
    }
}