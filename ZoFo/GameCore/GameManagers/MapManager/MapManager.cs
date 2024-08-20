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
using ZoFo.GameCore.GameObjects;
using ZoFo.GameCore.GameObjects.MapObjects;
using ZoFo.GameCore.GameObjects.MapObjects.StopObjects;

namespace ZoFo.GameCore.GameManagers.MapManager
{
    public class MapManager
    {
        private static readonly string _templatePath = "Content/MapData/TileMaps/{0}.tmj";
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; // Задача настроек для JsonSerialize
        private static readonly Dictionary<string, string> _classPath = new Dictionary<string, string>() { 
            { "Collectables", "ZoFo.GameCore.GameObjects." }, 
            { "Enemies", "ZoFo.GameCore.GameObjects." } 
        };

        //private static readonly float _scale = 1.0f;
        private List<TileSet> _tileSets = new List<TileSet>();
        private TileMap _tileMap = new TileMap();


        /// <summary>
        /// Загрузка карты. Передаётся название файла карты. По умолчанию main.
        /// </summary>
        /// <param name="mapName"></param>
        public void LoadMap(string mapName = "main")
        {
            // Загрузка TileMap
            _tileMap = JsonSerializer.Deserialize<TileMap>(File.ReadAllText(string.Format(_templatePath, mapName)), _options);

            // Загрузка TileSet-ов по TileSetInfo
            foreach (TileSetInfo tileSetInfo in _tileMap.TileSets)
            {
                TileSet tileSet = LoadTileSet(Path.Combine(AppContext.BaseDirectory, "Content", "MapData", "TileMaps", tileSetInfo.Source));
                tileSet.FirstGid = tileSetInfo.FirstGid;
                _tileSets.Add(tileSet);
            }
            _tileSets.Reverse();


            foreach (var layer in _tileMap.Layers)
            {
                if (layer.Type == "objectgroup")
                {
                    ProcessObjectLayers(layer);
                }
                else
                {
                    ProcessTileLayers(layer);
                }
            }
        }

        private void ProcessTileLayers(Layer layer)
        {
            foreach (var chunk in layer.Chunks)
            {
                for (int i = 0; i < chunk.Data.Length; i++)
                {
                    foreach (var tileSet in _tileSets)
                    {
                        if (tileSet.FirstGid <= chunk.Data[i])
                        {
                            int number = chunk.Data[i] - tileSet.FirstGid;
                            Tile tile = tileSet.Tiles[number]; // По факту может быть StopObjectom, но на уровне Tiled это все в первую очередь Tile

                            
                            if (tile.Animation is not null)
                            {

                            }

                            int relativeColumn = number % tileSet.Columns;
                            int relativeRow = number / tileSet.Columns; // относительно левого угла чанка

                            Rectangle sourceRectangle = new Rectangle(relativeColumn * (tileSet.TileWidth + tileSet.Spacing) + tileSet.Margin,
                                relativeRow * (tileSet.TileHeight + tileSet.Spacing) + tileSet.Margin,
                                tileSet.TileWidth, tileSet.TileHeight);

                            Vector2 position = new Vector2(
                                (i % chunk.Width) * _tileMap.TileWidth + chunk.X * _tileMap.TileWidth,
                                (i / chunk.Height) * _tileMap.TileHeight + chunk.Y * _tileMap.TileHeight);


                            switch (tile.Type)
                            {
                                case "Tile":
                                    AppManager.Instance.server.RegisterGameObject(new MapObject(position,
                                        new Vector2(tileSet.TileWidth, tileSet.TileHeight),
                                        sourceRectangle,
                                        "Textures/TileSetImages/" + Path.GetFileName(tileSet.Image).Replace(".png", "")));
                                    break;

                                case "StopObject":
                                    var collisionRectangles = LoadRectangles(tile); // Грузит коллизии обьектов

                                    AppManager.Instance.server.RegisterGameObject(new StopObject(position,
                                        new Vector2(tileSet.TileWidth, tileSet.TileHeight),
                                        sourceRectangle,
                                        "Textures/TileSetImages/" + Path.GetFileName(tileSet.Image).Replace(".png", ""),
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

        private void ProcessObjectLayers(Layer layer)
        {
            foreach (var item in layer.Objects)
            {
                string type = Path.GetFileName(item.Template).Replace(".tj", "");
                var collectable = Activator.CreateInstance(Type.GetType(_classPath[layer.Name] + type), new Vector2(item.X, item.Y));
                AppManager.Instance.server.RegisterGameObject(collectable as GameObject);
            }
        }

        /// <summary>
        /// Загружает и парсит TileSet по его пути.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private TileSet LoadTileSet(string path)
        {
            return JsonSerializer.Deserialize<TileSet>(File.ReadAllText(path), _options);
            
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

        private MapElements.Object LoadObject(string path)
        {
            return JsonSerializer.Deserialize<MapElements.Object>(File.ReadAllText(path), _options);
        }
    }
}