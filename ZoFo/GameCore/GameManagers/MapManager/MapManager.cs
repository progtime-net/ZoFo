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
using ZoFo.GameCore.GameObjects.MapObjects;
using ZoFo.GameCore.GameObjects.MapObjects.StopObjects;
using ZoFo.GameCore.GameObjects.MapObjects.Tiles;

namespace ZoFo.GameCore.GameManagers.MapManager
{
    public class MapManager
    {

        private static readonly string _templatePath = "Content/MapData/TileMaps/{0}.tmj";
        private static readonly float _scale = 1.0f;
        private List<TileSet> _tileSets = new List<TileSet>();

        /// <summary>
        /// Загрузка карты. Передаётся название файла карты. По умолчанию main.
        /// </summary>
        /// <param name="mapName"></param>
        public void LoadMap(string mapName = "main")
        {
            // Загрузка TileMap
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            TileMap tileMap = JsonSerializer.Deserialize<TileMap>(File.ReadAllText(string.Format(_templatePath, mapName)), options);

            // Загрузка TileSet-ов по TileSetInfo
            List<TileSet> tileSets = new List<TileSet>();
            foreach (TileSetInfo tileSetInfo in tileMap.TileSets) 
            {
                TileSet tileSet = LoadTileSet("Content/MapData/"+tileSetInfo.Source);
                tileSet.FirstGid = tileSetInfo.FirstGid;
                tileSets.Add(tileSet);
            }

            foreach (var layer in tileMap.Layers)
            {
                foreach (var chunk in layer.Chunks)
                {
                    for (int i = 0; i < chunk.Data.Length; i++)
                    {
                        foreach (var tileSet in tileSets)
                        {
                            if (tileSet.FirstGid - chunk.Data[i] < 0)
                            {
                                int number = chunk.Data[i] - tileSet.FirstGid;

                                int relativeColumn = (number % tileSet.Columns) * tileSet.TileWidth;
                                int relativeRow = (number / tileSet.Columns) * tileSet.TileHeight;

                                Rectangle sourceRectangle = new Rectangle(relativeColumn * tileSet.TileWidth, relativeRow * tileSet.TileHeight,
                                   /* relativeColumn * tileSet.TileWidth +*/ tileSet.TileWidth, /*relativeRow * tileSet.TileHeight +*/ tileSet.TileHeight);

                                Vector2 position = new Vector2((i % chunk.Width) * tileSet.TileWidth + chunk.X * chunk.Width, (i / chunk.Height)*tileSet.TileHeight + chunk.Y * chunk.Height) ;

                                switch (layer.Class)
                                {
                                    case "Tile":
                                        AppManager.Instance.server.RegisterGameObject(new MapObject(position, new Vector2(tileSet.TileWidth * _scale, tileSet.TileHeight * _scale), sourceRectangle, "Textures/TileSets/"+tileSet.Name)); //fix naming
                                        break;
                                    case "StopObject":
                                        // new StopObject(position, new Vector2(tileSet.TileWidth * _scale, tileSet.TileHeight * _scale), sourceRectangle, tileSet.Name);
                                        break;
                                    default:
                                        break;
                                }

                            }
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
                var options = new JsonSerializerOptions //TODO Remove
                {
                    PropertyNameCaseInsensitive = true
                };
                string data = reader.ReadToEnd();
                return JsonSerializer.Deserialize<TileSet>(data, options);
            }
        }
    }
}
