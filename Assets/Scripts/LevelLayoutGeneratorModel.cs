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
        [SerializeField] private int _columns = 13;
        [SerializeField] private int _rows = 11;

        private List<Vector2> _freeCoords = new();
        private List<Vector2> _blockedCoords = new();

        public List<Vector2> FreeCoords
        {
            get
            {
                if (_freeCoords.Count == 0)
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

        private void GenerateCoords()
        {
            _blockedCoords = new List<Vector2>();
            _freeCoords = new List<Vector2>();
            for (var currentColumn = 0; currentColumn < _columns; currentColumn++)
            {
                for (var currentRow = 0; currentRow < _rows; currentRow++)
                {
                    var coord = new Vector2(currentColumn, currentRow);

                    // Generate blocked spaces on 2nd rows/columns
                    if (currentColumn % 2 == 1 && currentRow % 2 == 1)
                        _blockedCoords.Add(coord);
                    else
                        _freeCoords.Add(coord);
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
            _freeCoords = new List<Vector2>();
            _blockedCoords = new List<Vector2>();
        }
    }
}

