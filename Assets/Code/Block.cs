using UnityEngine;
using System.Collections;

public class Block
{
    public enum Direction
    {
        North,
        East,
        South,
        West,
        Up,
        Down
    };

    public struct Tile
    {
        public int X;
        public int Y;
    }

    private const float TileSize = 0.25f;

    public Block()
    {

    }

    public virtual MeshData BlockData(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.UseRenderDataForCol = true;

        if (!chunk.GetBlock(x, y + 1, z).IsSolid(Direction.Down))
        {
            meshData = FaceDataUp(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y - 1, z).IsSolid(Direction.Up))
        {
            meshData = FaceDataDown(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y, z + 1).IsSolid(Direction.South))
        {
            meshData = FaceDataNorth(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x, y, z - 1).IsSolid(Direction.North))
        {
            meshData = FaceDataSouth(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x + 1, y, z).IsSolid(Direction.West))
        {
            meshData = FaceDataEast(chunk, x, y, z, meshData);
        }

        if (!chunk.GetBlock(x - 1, y, z).IsSolid(Direction.East))
        {
            meshData = FaceDataWest(chunk, x, y, z, meshData);
        }

        return meshData;
    }

    public virtual Tile TexturePosition(Direction direction)
    {
        var tile = new Tile
        {
            X = 0,
            Y = 0
        };

        return tile;
    }

    public virtual Vector2[] FaceUVs(Direction direction)
    {
        var uvs = new Vector2[4];
        Tile tilePos = TexturePosition(direction);

        uvs[0] = new Vector2(TileSize * tilePos.X + TileSize, TileSize * tilePos.Y);
        uvs[1] = new Vector2(TileSize * tilePos.X + TileSize, TileSize * tilePos.Y + TileSize);
        uvs[2] = new Vector2(TileSize * tilePos.X, TileSize * tilePos.Y + TileSize);
        uvs[3] = new Vector2(TileSize * tilePos.X, TileSize * tilePos.Y);

        return uvs;
    }

    protected virtual MeshData FaceDataUp(Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();
        meshData.Uv.AddRange(FaceUVs(Direction.Up));

        return meshData;
    }

    protected virtual MeshData FaceDataDown
     (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();
        meshData.Uv.AddRange(FaceUVs(Direction.Down));

        return meshData;
    }

    protected virtual MeshData FaceDataNorth
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();
        meshData.Uv.AddRange(FaceUVs(Direction.North));

        return meshData;
    }

    protected virtual MeshData FaceDataEast
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z + 0.5f));

        meshData.AddQuadTriangles();
        meshData.Uv.AddRange(FaceUVs(Direction.East));

        return meshData;
    }

    protected virtual MeshData FaceDataSouth
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x + 0.5f, y - 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();
        meshData.Uv.AddRange(FaceUVs(Direction.South));

        return meshData;
    }

    protected virtual MeshData FaceDataWest
        (Chunk chunk, int x, int y, int z, MeshData meshData)
    {
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z + 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y + 0.5f, z - 0.5f));
        meshData.AddVertex(new Vector3(x - 0.5f, y - 0.5f, z - 0.5f));

        meshData.AddQuadTriangles();
        meshData.Uv.AddRange(FaceUVs(Direction.West));

        return meshData;
    }

    public virtual bool IsSolid(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                return true;
            case Direction.South:
                return true;
            case Direction.East:
                return true;
            case Direction.West:
                return true;
            case Direction.Up:
                return true;
            case Direction.Down:
                return true;
        }

        return false;
    }
}
