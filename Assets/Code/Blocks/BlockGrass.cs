using UnityEngine;
using System.Collections;

namespace Assets.Code.Blocks
{
    class BlockGrass : Block
    {
        public BlockGrass()
            : base()
        {
            
        }

        public override Tile TexturePosition(Direction direction)
        {
            var tile = new Tile();

            switch (direction)
            {
                case Direction.Up:
                    tile.X = 2;
                    tile.Y = 0;
                    return tile;
                case Direction.Down:
                    tile.X = 1;
                    tile.Y = 0;
                    return tile;
                case Direction.East:
                case Direction.North:
                case Direction.South:
                case Direction.West:
                    tile.X = 3;
                    tile.Y = 0;
                    return tile;
            }

            return tile;
        }
    }
}
