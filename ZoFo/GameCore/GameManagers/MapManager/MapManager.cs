using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.MapManager.MapElements;

namespace ZoFo.GameCore.GameManagers.MapManager
{
    public class MapManager
    {
        private static readonly string _path = "/{0}.tmj";
        private List<TileSet> _tileSets = new List<TileSet>();

        public void LoadMap(string mapName = "main")
        {
            TileMap tileMap;
            using (StreamReader reader = new StreamReader(string.Format(_path, mapName)))
            {
                string data = reader.ReadToEnd();
                tileMap = JsonSerializer.Deserialize<TileMap>(data);
            }

            List<TileSet> tileSets = new List<TileSet>();
            foreach (TileSetInfo tileSetInfo in tileMap.TileSets) 
            {
                TileSet tileSet = LoadTileSet(tileSetInfo.Source);
                tileSet.FirstGid = tileSetInfo.FirstGid;
                tileSets.Add(tileSet);
            }

            foreach (var chunk in tileMap.Layers[0].Chunks)
            {
                foreach (var id in chunk.Data)
                {
                    for (int i = 0; i < tileSets.Count; i++)
                    {
                        if (tileSets[i].FirstGid - id < 0) 
                        {
                            int number = id - tileSets[i].FirstGid;
                            
                        }
                    }
                }
            }

        }

        private TileSet LoadTileSet(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string data = reader.ReadToEnd();
                return JsonSerializer.Deserialize<TileSet>(data);
            }
        }

    }
}
