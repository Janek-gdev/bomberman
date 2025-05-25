using System;
using System.Collections;
using Bomberman.Bombing;
using UnityEngine;

namespace Bomberman.Player
{
    public class Explosion : MonoBehaviour
    {
        private BombModel _bombModel;
        private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[10];

        public void Initialize(BombModel bombModel)
        {
            _bombModel = bombModel;
            StartCoroutine(RemoveExplosion());
        }

        private void Update()
        {
            var hits = Physics2D.BoxCastNonAlloc(transform.position, Vector2.one, 0, Vector2.zero, _raycastResults);
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