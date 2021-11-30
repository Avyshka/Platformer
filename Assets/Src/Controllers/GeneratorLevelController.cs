using System;
using Platformer.Enums;
using Platformer.MarchingSquares;
using Platformer.Views;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer.Controllers
{
    public class GeneratorLevelController
    {
        private const int CountWall = 4;
        private const bool IsMarchingSquaresMethod = false;

        private readonly Tilemap _tilemap;
        private readonly Tile _groundTile;
        private readonly Tile _grassTile;
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly bool _borders;

        private readonly int _fillPercent;
        private readonly int _smoothFactor;

        private readonly MapTile[,] _map;

        private MarchingSquaresGeneratorLevel _marchingSquaresGeneratorLevel = new MarchingSquaresGeneratorLevel();
        
        public GeneratorLevelController(GeneratorLevelView levelView)
        {
            _tilemap = levelView.Tilemap;
            _groundTile = levelView.GroundTile;
            _grassTile = levelView.GrassTile;
            _mapWidth = levelView.MapWidth;
            _mapHeight = levelView.MapHeight;
            _borders = levelView.Borders;
            _fillPercent = levelView.FillPercent;
            _smoothFactor = levelView.SmoothFactor;

            _map = new MapTile[_mapWidth, _mapHeight];
        }

        public void Init()
        {
            RandomFillMap();
            for (var i = 0; i < _smoothFactor; i++)
            {
                SmoothMap();
            }

            if (IsMarchingSquaresMethod)
            {
                _marchingSquaresGeneratorLevel.GenerateGrid(_map, 1);
                _marchingSquaresGeneratorLevel.DrawTilesOnMap(_tilemap, _groundTile);
            }
            else
            {
                AddGrass();
                DrawTiles();
            }
        }

        private void RandomFillMap()
        {
            var seed = Time.time.ToString();
            var pseudoRandom = new System.Random(seed.GetHashCode());

            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    if ((x == 0 || x == _mapWidth - 1 || y == 0 || y == _mapHeight - 1) && _borders)
                    {
                        _map[x, y] = MapTile.Dirt;
                    }
                    else
                    {
                        _map[x, y] = pseudoRandom.Next(0, 100) < _fillPercent ? MapTile.Dirt : MapTile.Empty;
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    var neighbourWallTiles = GetSurroundingWallCount(x, y);
                    if (neighbourWallTiles > CountWall)
                    {
                        _map[x, y] = MapTile.Dirt;
                    }
                    else if (neighbourWallTiles < CountWall)
                    {
                        _map[x, y] = MapTile.Empty;
                    }
                }
            }
        }

        private int GetSurroundingWallCount(int x, int y)
        {
            var wallCount = 0;
            for (var neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
            {
                for (var neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
                {
                    if (neighbourX >= 0 && neighbourX < _mapWidth && neighbourY >= 0 && neighbourY < _mapHeight)
                    {
                        if (neighbourX != x || neighbourY != y)
                            wallCount += (int) _map[neighbourX, neighbourY];
                    }
                    else
                    {
                        wallCount++;
                    }
                }
            }

            return wallCount;
        }

        private void AddGrass()
        {
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    var searchY = y + 1;
                    if (
                        _map[x, y] == MapTile.Dirt &&
                        searchY >= 0 &&
                        searchY < _mapHeight &&
                        _map[x, searchY] == MapTile.Empty)
                    {
                        _map[x, y] = MapTile.Grass;
                    }
                }
            }
        }

        private void DrawTiles()
        {
            for (var x = 0; x < _mapWidth; x++)
            {
                for (var y = 0; y < _mapHeight; y++)
                {
                    var posTile = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight / 2 + y, 0);
                    switch (_map[x, y])
                    {
                        case MapTile.Empty:
                            break;
                        case MapTile.Dirt:
                            _tilemap.SetTile(posTile, _groundTile);
                            break;
                        case MapTile.Grass:
                            _tilemap.SetTile(posTile, _grassTile);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
    }
}