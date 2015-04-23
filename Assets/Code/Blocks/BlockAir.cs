using UnityEngine;
using System.Collections;

namespace Assets.Code.Blocks
{
    class BlockAir : Block
    {
        public BlockAir()
            : base()
        {
            
        }

        public override MeshData BlockData
            (Chunk chunk, int x, int y, int z, MeshData meshData)
        {
            meshData.UseRenderDataForCol = true;

            return meshData;
        }

        public override bool IsSolid(Direction direction)
        {
            return false;
        }
    }
}
