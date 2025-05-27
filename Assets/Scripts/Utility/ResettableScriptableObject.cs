using UnityEngine;

namespace Bomberman
{
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