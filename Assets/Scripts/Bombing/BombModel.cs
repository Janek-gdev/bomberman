using UnityEngine;

namespace Bomberman.Bombing
{
    [CreateAssetMenu(menuName = MenuName.Bombing + nameof(BombModel), fileName = nameof(BombModel))]
    public class BombModel : ResettableScriptableObject
    {

        [SerializeField] private int _playerWalkedOffLayer;
        public int PlayerWalkedOffLayer => _playerWalkedOffLayer;
        
        [SerializeField] private LayerMask _explosionStoppingLayers;
        public LayerMask ExplosionStoppingLayers => _explosionStoppingLayers;
        
        [SerializeField] private LayerMask _baseBombableLayerMask;
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
        [SerializeField] private float _explosionDuration;
        public float ExplosionDuration => _explosionDuration;
        
        [SerializeField] private float _timeToExplode;
        public float TimeToExplode => _timeToExplode;
        
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
            _bombableLayerMask = _baseBombableLayerMask;
        }
    }
}