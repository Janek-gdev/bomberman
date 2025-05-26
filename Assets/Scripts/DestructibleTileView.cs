using System;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Level
{
    public class DestructibleTileView : MonoBehaviour, IBombable
    {
        public event Action<DestructibleTileView> OnTileDestroyed; 
        public WalkableTileModel TileModel;
        public void GetBombed()
        {
            TileModel.IsBlocked = false;
            OnTileDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}