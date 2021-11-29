using UnityEngine;
using UnityEngine.Tilemaps;

namespace Platformer.Views
{
    public class GeneratorLevelView : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Tile _groundTile;
        [SerializeField] private Tile _grassTile;
        [SerializeField] private int _mapWidth;
        [SerializeField] private int _mapHeight;
        [SerializeField] private bool _borders;

        [SerializeField] [Range(0, 100)] private int _fillPercent;
        [SerializeField] [Range(0, 100)] private int _smoothFactor;

        public Tilemap Tilemap => _tilemap;

        public Tile GroundTile => _groundTile;

        public Tile GrassTile => _grassTile;

        public int MapWidth => _mapWidth;

        public int MapHeight => _mapHeight;

        public bool Borders => _borders;

        public int FillPercent => _fillPercent;

        public int SmoothFactor => _smoothFactor;
    }
}