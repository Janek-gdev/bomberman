using UnityEngine;

namespace Bomberman
{
    /// <summary>
    /// A scriptable object that has non-serialized data that needs reset between runs
    /// </summary>
    public abstract class ResettableScriptableObject : ScriptableObject
#if UNITY_EDITOR
        , ISerializationCallbackReceiver
#endif
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