using System;
using System.Collections;
using System.Collections.Generic;
using Bomberman.Level;
using Bomberman.Player;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Bombing
{
    public class BombView : MonoBehaviour, IBombable
    {
        [SerializeField] private LayerMask _explosionStoppingLayers;
        [SerializeField] private Explosion _centralExplosionPrefab;
        [SerializeField] private Explosion _connectorExplosionPrefab;
        [SerializeField] private Explosion _endExplosionPrefab;
        private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[10];

        private Coroutine _explosionTimer;
        public event Action<BombView> OnBombExploded;
        [SerializeField] private Animator _animator;

        private BombModel _bombModel;
        public WalkableTileModel Tile { get; private set; }
        public void Initialize(WalkableTileModel tileModel, BombModel bombModel)
        {
            Tile = tileModel;
            _bombModel = bombModel;
            Tile.IsBlocked = true;
            _explosionTimer = StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown()
        {
            yield return new WaitForSeconds(_bombModel.TimeToExplode);
            Explode();
        }

        private void Explode()
        {
            var explosions = new List<Explosion>();
            //todo pooling
            explosions.Add(Instantiate(_centralExplosionPrefab, transform.position, Quaternion.identity));
            ExplodeInDirection(Direction.Left, explosions);
            ExplodeInDirection(Direction.Right, explosions);
            ExplodeInDirection(Direction.Up, explosions);
            ExplodeInDirection(Direction.Down, explosions);

            foreach (var explosion in explosions)
            {
                explosion.Initialize(_bombModel);
            }
            OnBombExploded?.Invoke(this);
            Tile.IsBlocked = false;
            Destroy(gameObject);
        }

        private void ExplodeInDirection(Direction direction, List<Explosion> explosions)
        {
            var directionVector = DirectionUtility.GetDirectionVector(direction);
            var rotation = DirectionUtility.GetRotationFromDirection(direction);
            for (int i = 1; i < _bombModel.ExplosionRange + 1; i++)
            {
                var hits = Physics2D.RaycastNonAlloc(transform.position, directionVector, _raycastResults, 1f * i, _explosionStoppingLayers);
                if (hits == 0)
                {
                    explosions.Add(Instantiate(i == _bombModel.ExplosionRange ? _endExplosionPrefab : _connectorExplosionPrefab,
                        (Vector2)transform.position + (directionVector * i), rotation));
                }
                else
                {
                    for (int j = 0; j < hits; j++)
                    {
                        _raycastResults[j].transform.GetComponent<IBombable>()?.GetBombed();
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            DrawRay(Direction.Left);
            DrawRay(Direction.Right);
            DrawRay(Direction.Up);
            DrawRay(Direction.Down);
        }

        private void DrawRay(Direction direction)
        {
            var directionVector = DirectionUtility.GetDirectionVector(direction);
            for (int i = 1; i < _bombModel.ExplosionRange + 1; i++)
            {
                Debug.DrawRay(transform.position, directionVector);
            }
        }

        public void GetBombed()
        {
            StopCoroutine(_explosionTimer);
            Explode();
        }
    }
}