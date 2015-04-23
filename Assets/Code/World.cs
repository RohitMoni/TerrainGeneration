using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Assets.Code.Blocks;
using UnityEngine;

namespace Assets.Code
{
    public class World : MonoBehaviour
    {
        public Dictionary<WorldPos, Chunk> Chunks = new Dictionary<WorldPos, Chunk>();

        public GameObject ChunkPrefab;

        void Start()
        {
            for (int x = -2; x < 2; x++)
            {
                for (int y = -1; y < 1; y++)
                {
                    for (int z = -1; z < 1; z++)
                    {
                        CreateChunk(x * Chunk.ChunkSizeX, y * Chunk.ChunkSizeY, z * Chunk.ChunkSizeZ);
                    }
                }
            }
        }

        public void CreateChunk(int x, int y, int z)
        {
            var worldPos = new WorldPos(x, y, z);

            //Instantiate the chunk at the coordinates using the chunk prefab
            GameObject newChunkObject = Instantiate(
                            ChunkPrefab, new Vector3(x, y, z),
                            Quaternion.Euler(Vector3.zero)
                        ) as GameObject;

            Chunk newChunk = newChunkObject.GetComponent<Chunk>();

            newChunk.WorldPosition = worldPos;
            newChunk.WorldRef = this;

            //Add it to the chunks dictionary with the position as the key
            Chunks.Add(worldPos, newChunk);
        }

        public Chunk GetChunk(int x, int y, int z)
        {
            var pos = new WorldPos();
            pos.X = Mathf.FloorToInt(x / (float)Chunk.ChunkSizeX) * Chunk.ChunkSizeX;
            pos.Y = Mathf.FloorToInt(y / (float)Chunk.ChunkSizeY) * Chunk.ChunkSizeX;
            pos.Z = Mathf.FloorToInt(z / (float)Chunk.ChunkSizeZ) * Chunk.ChunkSizeX;

            Chunk containerChunk = null;

            Chunks.TryGetValue(pos, out containerChunk);

            return containerChunk;
        }

        public Block GetBlock(int x, int y, int z)
        {
            Chunk containerChunk = GetChunk(x, y, z);

            if (containerChunk != null)
            {
                Block block = containerChunk.GetBlock(
                    x - containerChunk.WorldPosition.X,
                    y - containerChunk.WorldPosition.Y,
                    z - containerChunk.WorldPosition.Z);

                return block;
            }
            else
            {
                return new BlockAir();
            }
        }

        public void SetBlock(int x, int y, int z, Block block)
        {
            var chunk = GetChunk(x, y, z);

            if (chunk != null)
            {
                chunk.SetBlock(x - chunk.WorldPosition.X, y - chunk.WorldPosition.Y, z - chunk.WorldPosition.Z, block);
                chunk._update = true;
            }
        }
    }
}
