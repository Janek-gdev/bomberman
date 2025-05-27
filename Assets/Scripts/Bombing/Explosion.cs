using System;
using System.Collections;
using Bomberman.Bombing;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Player
{
    public class Explosion : MonoBehaviour
    {
        private BombModel _bombModel;
        private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[10];

        private void OnEnable()
        {
            GameEvents.instance.OnLevelTeardown += Cleanup;
        }

        private void OnDisable()
        {
            GameEvents.instance.OnLevelTeardown -= Cleanup;
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