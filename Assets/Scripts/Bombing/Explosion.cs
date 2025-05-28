using System.Collections;
using Bomberman.Player;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Bombing
{
    /// <summary>
    /// Represents a single instance/tile of an explosion
    /// </summary>
    public class Explosion : MonoBehaviour
    {
        private BombModel _bombModel;
        private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[10];

        private void OnEnable()
        {
            GameEvent.OnLevelTeardown += Cleanup;
        }

        private void OnDisable()
        {
            GameEvent.OnLevelTeardown -= Cleanup;
        }

        private void Cleanup()
        {
            Destroy(gameObject);
        }

        public void Initialize(BombModel bombModel)
        {
            _bombModel = bombModel;
            StartCoroutine(RemoveExplosion());
        }

        private void Update()
        {
            var hits = Physics2D.BoxCastNonAlloc(transform.position, new Vector2(0.5f, 0.5f), 0, Vector2.zero, _raycastResults, 0, _bombModel.BombableLayerMask);
            for (int i = 0; i < hits; i++)
            {
                _raycastResults[i].transform.GetComponent<IBombable>()?.GetBombed();
            }

        }

        private IEnumerator RemoveExplosion()
        {
            yield return new WaitForSeconds(_bombModel.ExplosionDuration);
            Destroy(gameObject);
        }
    }
}