using System.Collections.Generic;
using UnityEngine;

namespace Bomberman.Level
{
    /// <summary>
    /// Spawns the blocker/free tiles in the level
    /// Separate from general level spawning to allow use as a template
    /// </summary>
    [CreateAssetMenu(menuName = ScriptableObjectMenuName.LevelLayout + nameof(LevelLayoutGeneratorModel),
        fileName = nameof(LevelLayoutGeneratorModel))]
    public class LevelLayoutGeneratorModel : ResettableScriptableSingleton<LevelLayoutGeneratorModel>
    {
        public const float TileWidth = 1f;
        public const float HalfTileWidth = TileWidth/2;
        
        [SerializeField] private int _columns = 13;
        [SerializeField] private int _rows = 11;

        private List<WalkableTileModel> _walkableTiles;
        private WalkableTileModel[,] _indexedWalkableTiles;
        private List<Vector2> _blockedCoords = new();

        public List<WalkableTileModel> WalkableTiles
        {
            get
            {
                if (_walkableTiles.Count == 0)
                {
                    GenerateCoords();
                }

                return _walkableTiles;
            }
        }
        
        public List<Vector2> BlockedCoords
        {
            get
            {
                if (_blockedCoords.Count == 0)
                {
                    GenerateCoords();
                }

                return _blockedCoords;
            }
        }
        
        //Overshoot by half width to account for the fact the columns and min don't include sprite size
        public Vector2 MinTilePosition => new(-TileWidth-HalfTileWidth, -TileWidth-HalfTileWidth);
        public Vector2 MaxTilePosition => new(_columns + HalfTileWidth, _rows + HalfTileWidth);

        private void GenerateCoords()
        {
            _blockedCoords = new List<Vector2>();
            _walkableTiles = new List<WalkableTileModel>();
            _indexedWalkableTiles = new WalkableTileModel[_columns,_rows];
            for (var currentColumn = 0; currentColumn < _columns; currentColumn++)
            {
                for (var currentRow = 0; currentRow < _rows; currentRow++)
                {
                    var coord = new Vector2(currentColumn, currentRow);

                    // Generate blocked spaces on 2nd rows/columns
                    if (currentColumn % 2 == 1 && currentRow % 2 == 1)
                    {
                        _blockedCoords.Add(coord);
                    }
                    else
                    {
                        var walkable = new WalkableTileModel(coord);
                        _walkableTiles.Add(walkable);
                        _indexedWalkableTiles[currentColumn, currentRow] = walkable;
                    }
                }
            }

            
            for (var row = -1; row <= _rows; row++)
            {
                _blockedCoords.Add(new Vector2(-1, row)); // Left
                _blockedCoords.Add(new Vector2(_columns, row)); // Right
            }

            for (var col = -1; col <= _columns; col++)
            {
                _blockedCoords.Add(new Vector2(col, -1)); // Top
                _blockedCoords.Add(new Vector2(col, _rows)); // Bottom
            }
        }

        public WalkableTileModel GetClosestFreeTile(Transform transform)
        {
            return GetClosestFreeTile(transform.position);
        }

        public WalkableTileModel GetClosestFreeTile(Vector2 position)
        {
            return _indexedWalkableTiles[Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y)];
        }

        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            _indexedWalkableTiles = new WalkableTileModel[_columns,_rows];
            _walkableTiles = new List<WalkableTileModel>();
            _blockedCoords = new List<Vector2>();
        }

    }
}

