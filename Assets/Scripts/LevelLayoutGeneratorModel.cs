using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bomberman.Level
{
    [CreateAssetMenu(menuName = MenuName.LevelLayout + nameof(LevelLayoutGeneratorModel),
        fileName = nameof(LevelLayoutGeneratorModel))]
    public class LevelLayoutGeneratorModel : ResettableScriptableSingleton<LevelLayoutGeneratorModel>
    {
        public const float TileWidth = 1f;
        public const float HalfTileWidth = TileWidth/2;
        
        [SerializeField] private int _columns = 13;
        [SerializeField] private int _rows = 11;

        private Vector2[,] _freeCoords;
        private List<Vector2> _blockedCoords = new();

        public Vector2[,] FreeCoords
        {
            get
            {
                if (_freeCoords.Length == 0)
                {
                    GenerateCoords();
                }

                return _freeCoords;
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
            _freeCoords = new Vector2[_columns, _rows];
            for (var currentColumn = 0; currentColumn < _columns; currentColumn++)
            {
                for (var currentRow = 0; currentRow < _rows; currentRow++)
                {
                    var coord = new Vector2(currentColumn, currentRow);

                    // Generate blocked spaces on 2nd rows/columns
                    if (currentColumn % 2 == 1 && currentRow % 2 == 1)
                        _blockedCoords.Add(coord);
                    else
                        //slightly redundant data usage here, having an array whose indices hold the same information, but allows us to work off a separate scale later
                        _freeCoords[currentColumn, currentRow] = coord;
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

        protected override void ResetAfterPlayInEditor()
        {
            base.ResetAfterPlayInEditor();
            _freeCoords = new Vector2[_columns, _rows];
            _blockedCoords = new List<Vector2>();
        }
    }
}

