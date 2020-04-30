using PlatformerEngine.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PlatformerEditor
{
    public class LevelTab : HardGroupElement
    {
        public LevelTab(UIManager uiManager, Vector2 position, Vector2 size, float layer, string name) : base(uiManager, position, size, layer, name)
        {
            PlatformerEditor actualGame = (PlatformerEditor)UIManager.Game;
            Elements.Add(new LevelElement(UIManager, new Vector2(0, 0), new Vector2(Size.X, Size.Y), 0.3f, "level"));
            actualGame.WorldLayerListElement = new WorldLayerListElement(UIManager, new Vector2(0, 0), new Vector2(128, 256), 0.4f, "list_layers");
            Elements.Add(actualGame.WorldLayerListElement);
            actualGame.ObjectListElement = new WorldItemListElement(UIManager, new Vector2(0, 0), new Vector2(128, 240), 0.4f, "list_objects");
            actualGame.TileListElement = new WorldItemListElement(UIManager, new Vector2(0, 0), new Vector2(128, 240), 0.4f, "list_tiles");
            TabbedElement worldItemTabs = new TabbedElement(UIManager, new Vector2(0, 256), new Vector2(128, 256), 0.4f, "tabs_worlditems", 16);
            worldItemTabs.AddTab("objects", actualGame.ObjectListElement, 64);
            worldItemTabs.AddTab("tiles", actualGame.TileListElement, 64);
            Elements.Add(worldItemTabs);
            Elements.Add(new SettingsElement(UIManager, new Vector2(actualGame.GraphicsDevice.Viewport.Width - 128, 0), new Vector2(128, 692), 0.4f, "box_settings"));
        }
    }
}
