using Microsoft.Xna.Framework;
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
        private static readonly string _path = "TileMaps/{0}.tmj";
        private List<TileSet> _tileSets = new List<TileSet>();

        /// <summary>
        /// Загрузка карты. Передаётся название файла карты. По умолчанию main.
        /// </summary>
        /// <param name="mapName"></param>
        public void LoadMap(string mapName = "main")
        {
            // Загрузка TileMap
            TileMap tileMap;
            using (StreamReader reader = new StreamReader(string.Format(_path, mapName)))
            {
                string data = reader.ReadToEnd();
                tileMap = JsonSerializer.Deserialize<TileMap>(data);
            }

            // Загрузка TileSet-ов по TileSetInfo
            List<TileSet> tileSets = new List<TileSet>();
            foreach (TileSetInfo tileSetInfo in tileMap.TileSets) 
            {
                TileSet tileSet = LoadTileSet(tileSetInfo.Source);
                tileSet.FirstGid = tileSetInfo.FirstGid;
                tileSets.Add(tileSet);
            }

            foreach (var chunk in tileMap.Layers[0].Chunks)
            {
                for (int i = 0; i < chunk.Data.Length; i++)
                {
                    foreach (var tileSet in tileSets)
                    {
                        if (tileSet.FirstGid - chunk.Data[i] < 0)
                        {
                            int number = chunk.Data[i] - tileSet.FirstGid;

                            int relativeColumn = number % tileSet.Columns * tileSet.TileWidth;
                            int relativeRow = number / tileSet.Columns * tileSet.TileHeight;

                            Rectangle sourceRectangle = new Rectangle(relativeColumn * tileSet.TileWidth, relativeRow * tileSet.TileHeight,
                                relativeColumn * tileSet.TileWidth + tileSet.TileWidth, relativeRow * tileSet.TileHeight + tileSet.TileHeight);

                            Vector2 position = new Vector2(i % chunk.Width, i / chunk.Height);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Загружает и парсит TileSet по его пути.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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
