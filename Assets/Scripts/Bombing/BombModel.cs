using UnityEngine;

namespace Bomberman.Bombing
{
    /// <summary>
    /// Data used to generate bombs and their corresponding explosions
    /// </summary>
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.Bombing + nameof(BombModel), fileName = nameof(BombModel))]
    public class BombModel : ResettableScriptableObject
    {

        [SerializeField] private int _playerWalkedOffLayer;
        [SerializeField] private int _baseExplosionRange;
        [SerializeField] private LayerMask _explosionStoppingLayers;
        [SerializeField] private LayerMask _baseBombableLayerMask;
        [SerializeField] private float _explosionDuration;
        [SerializeField] private float _timeToExplode;

        public int PlayerWalkedOffLayer => _playerWalkedOffLayer;
        public LayerMask ExplosionStoppingLayers => _explosionStoppingLayers;
        private LayerMask _bombableLayerMask = -1;
        public LayerMask BombableLayerMask
        {
            get
            {
                if (_bombableLayerMask == -1)
                {
                    _bombableLayerMask = _baseBombableLayerMask;
                }
                return _bombableLayerMask;
            }
            set => _bombableLayerMask = value;
        }
        public float ExplosionDuration => _explosionDuration;
        
        public float TimeToExplode => _timeToExplode;
        
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
            _bombableLayerMask = _baseBombableLayerMask;
        }
    }
}