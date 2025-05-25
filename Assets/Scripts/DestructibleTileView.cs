using System;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Level
{
    public class DestructibleTileView : MonoBehaviour, IBombable
    {
        public WalkableTileModel TileModel;
        public void GetBombed()
        {
            Destroy(gameObject);
        }
    }
}