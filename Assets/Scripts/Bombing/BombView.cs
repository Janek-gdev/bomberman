using System;
using System.Collections;
using System.Collections.Generic;
using Bomberman.Collisions;
using Bomberman.Level;
using Bomberman.Player;
using Bomberman.Utility;
using UnityEngine;

namespace Bomberman.Bombing
{
    /// <summary>
    /// Represents a bomb as placed in the world
    /// Spawns <see cref="Explosion"/> for damaging component
    /// </summary>
    public class BombView : MonoBehaviour, IBombable
    {
        [SerializeField] private PlayerTileDetector _playerTileDetector;
        [SerializeField] private Explosion _centralExplosionPrefab;
        [SerializeField] private Explosion _connectorExplosionPrefab;
        [SerializeField] private Explosion _endExplosionPrefab;
        
        private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[10];

        private Coroutine _explosionTimer;
        public event Action<BombView> OnBombExploded;

        private BombModel _bombModel;
        private WalkableTileModel _tile;
        
        public void Initialize(WalkableTileModel tileModel, BombModel bombModel)
        {
            _tile = tileModel;
            _bombModel = bombModel;
            _tile.IsBlocked = true;
            _explosionTimer = StartCoroutine(StartExplosionCountdown());
            _playerTileDetector.OnPlayerIsOnTileChanged += OnPlayerIsOnTileChanged;
        }

        private void OnPlayerIsOnTileChanged(bool _)
        {
            if (!_playerTileDetector.PlayerIsOnTile)
            {
                gameObject.layer = _bombModel.PlayerWalkedOffLayer;
                _playerTileDetector.OnPlayerIsOnTileChanged -= OnPlayerIsOnTileChanged;
            }
        }

        private IEnumerator StartExplosionCountdown()
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
            _tile.IsBlocked = false;
            Destroy(gameObject);
        }

        private void ExplodeInDirection(Direction direction, List<Explosion> explosions)
        {
            var directionVector = DirectionUtility.GetDirectionVector(direction);
            var rotation = DirectionUtility.GetRotationFromDirection(direction);
            for (int i = 1; i < _bombModel.ExplosionRange + 1; i++)
            {
                var hits = Physics2D.RaycastNonAlloc(transform.position, directionVector, _raycastResults, 1f * i, _bombModel.ExplosionStoppingLayers);
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
                        return;
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