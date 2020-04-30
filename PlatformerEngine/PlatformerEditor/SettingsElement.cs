using Microsoft.Xna.Framework;
using PlatformerEngine.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerEditor
{
    public class SettingsElement : GroupElement
    {
        public SettingsElement(UIManager uiManager, Vector2 position, Vector2 size, float layer, string name) : base(uiManager, position, size, layer, name)
        {
            PlatformerEditor actualGame = (PlatformerEditor)UIManager.Game;
            TextInputElement filenameInputElement = new TextInputElement(UIManager, new Vector2(0, 0), new Vector2(128, 24), 0.4f, "input_filename");
            ButtonElement loadButton = new ButtonElement(UIManager, new Vector2(0, 24), new Vector2(48, 24), 0.4f, "button_load", "load");
            loadButton.Click = () =>
            {
                string filename = filenameInputElement.Text;
                if (filename.Length == 0)
                {
                    return;
                }
                actualGame.LoadLevel(filename);
            };
            ButtonElement saveButton = new ButtonElement(UIManager, new Vector2(48, 24), new Vector2(48, 24), 0.4f, "button_save", "save");
            saveButton.Click = () =>
            {
                string filename = filenameInputElement.Text;
                if (filename.Length == 0)
                {
                    return;
                }
                actualGame.SaveLevel(filename);
            };
            Elements.Add(filenameInputElement);
            Elements.Add(loadButton);
            Elements.Add(saveButton);
            TextInputElement snapXInput = new TextInputElement(UIManager, new Vector2(0, 48), new Vector2(56, 24), 0.4f, "input_snap_x");
            TextInputElement snapYInput = new TextInputElement(UIManager, new Vector2(56, 48), new Vector2(56, 24), 0.4f, "input_snap_y");
            ButtonElement setSnapButton = new ButtonElement(UIManager, new Vector2(0, 72), new Vector2(64, 20), 0.4f, "button_snap_set", "set snap");
            setSnapButton.Click = () =>
            {
                LevelElement levelElement = (LevelElement)UIManager.GetUIElement("level");
                levelElement.Snap = new Vector2(int.Parse(snapXInput.Text), int.Parse(snapYInput.Text));
            };
            Elements.Add(snapXInput);
            Elements.Add(snapYInput);
            Elements.Add(setSnapButton);

            TextInputElement gravityXInput = new TextInputElement(UIManager, new Vector2(0, 92), new Vector2(56, 24), 0.4f, "input_gravity_x");
            TextInputElement gravityYInput = new TextInputElement(UIManager, new Vector2(56, 92), new Vector2(56, 24), 0.4f, "input_gravity_y");
            ButtonElement setGravityButton = new ButtonElement(UIManager, new Vector2(0, 116), new Vector2(64, 20), 0.4f, "button_gravity_set", "set gravity");
            setGravityButton.Click = () =>
            {
                LevelElement levelElement = (LevelElement)UIManager.GetUIElement("level");
                string xText = gravityXInput.Text;
                if (xText.Length == 0) xText = "0";
                string yText = gravityYInput.Text;
                if (yText.Length == 0) yText = "0";
                levelElement.Gravity = new Vector2(int.Parse(xText), int.Parse(yText));
            };
            Elements.Add(gravityXInput);
            Elements.Add(gravityYInput);
            Elements.Add(setGravityButton);

            TextInputElement roomWidthInput = new TextInputElement(UIManager, new Vector2(0, 136), new Vector2(56, 24), 0.4f, "input_room_height");
            TextInputElement roomHeightInput = new TextInputElement(UIManager, new Vector2(56, 136), new Vector2(56, 24), 0.4f, "input_room_width");
            ButtonElement setRoomSizeButton = new ButtonElement(UIManager, new Vector2(0, 160), new Vector2(64, 20), 0.4f, "button_room_size_set", "set room size");
            setRoomSizeButton.Click = () =>
            {
                LevelElement levelElement = (LevelElement)UIManager.GetUIElement("level");
                string xText = roomWidthInput.Text;
                if (xText.Length == 0) xText = "0";
                string yText = roomHeightInput.Text;
                if (yText.Length == 0) yText = "0";
                levelElement.LevelSize = new Vector2(int.Parse(xText), int.Parse(yText));
            };
            Elements.Add(roomWidthInput);
            Elements.Add(roomHeightInput);
            Elements.Add(setRoomSizeButton);
        }
    }
}
