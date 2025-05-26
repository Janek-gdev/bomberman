using System;
using Bomberman.Player;
using UnityEngine;

namespace Bomberman.Enemies
{
    public class EnemyView : MonoBehaviour, IBombable
    {
        public event Action<EnemyView> OnEnemyDestroyed; 
        public void GetBombed()
        {
            OnEnemyDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}