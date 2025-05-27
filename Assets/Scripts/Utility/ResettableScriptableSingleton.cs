using UnityEditor;
using UnityEngine;

namespace Bomberman
{
    /// <summary>
    /// A singleton version of <see cref="ResettableScriptableObject"/>
    /// </summary>
    public abstract class ResettableScriptableSingleton<T> : ScriptableSingleton<T> 
#if UNITY_EDITOR
        , ISerializationCallbackReceiver
#endif
        where T : ScriptableObject
    {
#if UNITY_EDITOR
        public void OnBeforeSerialize()
        {
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            ResetAfterPlayInEditor();
        }
#endif

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        protected virtual void ResetAfterPlayInEditor()
        {
        }
    }
}