using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers.MapManager.MapElements;
using ZoFo.GameCore.GameObjects.MapObjects;
using ZoFo.GameCore.GameObjects.MapObjects.StopObjects;

namespace ZoFo.GameCore.GameManagers.MapManager
{
    public class MapManager
    {
        private static readonly string _templatePath = "Content/MapData/TileMaps/{0}.tmj";

        //private static readonly float _scale = 1.0f;
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
            TileMap tileMap =
                JsonSerializer.Deserialize<TileMap>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, string.Format(_templatePath, mapName))), options);

            // Загрузка TileSet-ов по TileSetInfo
            List<TileSet> tileSets = new List<TileSet>();
            foreach (TileSetInfo tileSetInfo in tileMap.TileSets)
            {
                TileSet tileSet = LoadTileSet(Path.Combine(AppContext.BaseDirectory, "Content", "MapData", "TileMaps", tileSetInfo.Source));
                tileSet.FirstGid = tileSetInfo.FirstGid;
                tileSets.Add(tileSet);
            }
            tileSets.Reverse();

            foreach (var layer in tileMap.Layers)
            {
                if (layer.Type == "objectgroup")
                {

                }
                else
                {
                    foreach (var chunk in layer.Chunks)
                    {
                        for (int i = 0; i < chunk.Data.Length; i++)
                        {
                            foreach (var tileSet in tileSets)
                            {
                                if (tileSet.FirstGid <= chunk.Data[i])
                                {
                                    int number = chunk.Data[i] - tileSet.FirstGid;

                                    int relativeColumn = number % tileSet.Columns;
                                    int relativeRow = number / tileSet.Columns; // относительно левого угла чанка

                                    Rectangle sourceRectangle = new Rectangle(relativeColumn * (tileSet.TileWidth + tileSet.Spacing) + tileSet.Margin,
                                        relativeRow * (tileSet.TileHeight + tileSet.Spacing) + tileSet.Margin,
                                        tileSet.TileWidth, tileSet.TileHeight);

                                    Vector2 position = new Vector2(
                                        (i % chunk.Width) * tileMap.TileWidth + chunk.X * tileMap.TileWidth,
                                        (i / chunk.Height) * tileMap.TileHeight + chunk.Y * tileMap.TileHeight);

                                    Tile tile = tileSet.Tiles[number]; // По факту может быть StopObjectom, но на уровне Tiled это все в первую очередь Tile
                                    string textureName = Path.Combine(AppContext.BaseDirectory, "Content", "Textures", "TileSetImages",
                                        Path.GetFileName(tileSet.Image).Replace(".png", ""));
                                    switch (tile.Type)
                                    {
                                        case "Tile":
                                            AppManager.Instance.server.RegisterGameObject(new MapObject(position,
                                                new Vector2(tileSet.TileWidth, tileSet.TileHeight),
                                                sourceRectangle,
                                                textureName));
                                            break;

                                        case "StopObject":
                                            var collisionRectangles = LoadRectangles(tile); // Грузит коллизии обьектов

                                            AppManager.Instance.server.RegisterGameObject(new StopObject(position,
                                                new Vector2(tileSet.TileWidth, tileSet.TileHeight),
                                                sourceRectangle,
                                                textureName,
                                                collisionRectangles.ToArray()));
                                            break;

                                        default:
                                            break;
                                    }
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

        /// <summary>
        /// Загружает все квадраты коллизии тайла.
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        private List<Rectangle> LoadRectangles(Tile tile)
        {
            if (tile.Objectgroup == null)
            {
                return new List<Rectangle>() { new Rectangle(0, 0, 0, 0) };
            }

            List<Rectangle> collisionRectangles = new List<Rectangle>();
            foreach (var obj in tile.Objectgroup.Objects)
            {
                collisionRectangles.Add(new Rectangle((int)obj.X, (int)obj.Y, (int)obj.Width, (int)obj.Height));
            }

            return collisionRectangles;
        }
    }
}