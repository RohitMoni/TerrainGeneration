using UnityEngine;
using System.Collections;
using Assets.Code;
using Assets.Code.Blocks;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{
    public World WorldRef;
    public WorldPos WorldPosition;

    public static int ChunkSizeX = 16;
    public static int ChunkSizeY = 16;
    public static int ChunkSizeZ = ChunkSizeX;

    private readonly Block[, ,] _blocks = new Block[ChunkSizeX, ChunkSizeY, ChunkSizeZ];

    public bool ShouldUpdate = true;

    private MeshFilter _filter;
    private MeshCollider _coll;

	// Use this for initialization
	void Start ()
	{
	    _filter = GetComponent<MeshFilter>();
	    _coll = GetComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (ShouldUpdate)
	    {
	        ShouldUpdate = false;
            UpdateChunk();
	    }
	}

    public void SetBlock(int x, int y, int z, Block block)
    {
        if (InRangeX(x) && InRangeY(y) && InRangeZ(z))
        {
            _blocks[x, y, z] = block;
        }
        else
        {
            WorldRef.SetBlock(WorldPosition.X + x, WorldPosition.Y + y, WorldPosition.Z + z, block);
        }
    }

    public Block GetBlock(int x, int y, int z)
    {
        if (InRangeX(x) && InRangeY(y) && InRangeZ(z))
            return _blocks[x, y, z];
        
        return WorldRef.GetBlock(WorldPosition.X + x, WorldPosition.Y + y, WorldPosition.Z + z);
    }

    public static bool InRangeX(int index)
    {
        if (index < 0 || index >= ChunkSizeX)
            return false;

        return true;
    }

    public static bool InRangeY(int index)
    {
        if (index < 0 || index >= ChunkSizeY)
            return false;

        return true;
    }

    public static bool InRangeZ(int index)
    {
        if (index < 0 || index >= ChunkSizeZ)
            return false;

        return true;
    }

    /* UpdateChunk()
     * Updates the chunk based on its contents
     */
    private void UpdateChunk()
    {
        MeshData meshData = new MeshData();

        for (int x = 0; x < ChunkSizeX; x++)
        {
            for (int y = 0; y < ChunkSizeY; y++)
            {
                for (int z = 0; z < ChunkSizeZ; z++)
                {
                    meshData = _blocks[x, y, z].BlockData(this, x, y, z, meshData);
                }
            }
        }

        RenderMesh(meshData);
    }

    /* RenderMesh()
     * Sends the calculated mesh information to
     * the mesh and collision components
     */
    void RenderMesh(MeshData meshData)
    {
        _filter.mesh.Clear();
        _filter.mesh.vertices = meshData.Vertices.ToArray();
        _filter.mesh.triangles = meshData.Triangles.ToArray();

        _filter.mesh.uv = meshData.Uv.ToArray();
        _filter.mesh.RecalculateNormals();

        _coll.sharedMesh = null;
        var mesh = new Mesh
        {
            vertices = meshData.ColVertices.ToArray(),
            triangles = meshData.ColTriangles.ToArray()
        };
        mesh.RecalculateNormals();

        _coll.sharedMesh = mesh;
    }
}
