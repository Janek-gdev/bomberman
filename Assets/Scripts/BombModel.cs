using UnityEngine;

namespace Bomberman.Bombing
{
    public class BombModel : ResettableScriptableObject
    {
        [SerializeField] private int _baseExplosionRange;
        private int _explosionRange;

        public int ExplosionRange
        {
            get
            {
                if (_explosionRange == 0)
                {
                    _explosionRange = _baseExplosionRange;
                }
                return _explosionRange;
            }
            set => _explosionRange = value;
        }

        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            ExplosionRange = _baseExplosionRange;
        }
    }
}