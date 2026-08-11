namespace RogueHunters.Scenes;

using SadConsole;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;
using System;

class MonstrumScreen : ScreenObject
{
    private ScreenSurface _mainSurface;
    private ControlsConsole _uiConsole;
    private String _monsterScreenPath = "Assets/Screens/MonsterGlossaryScreen.xp";
    private MonsterDatabase _monsterdb;
    private MonsterEntry[] _monsters;


    //Button Settings //tbd add this to game settings maybe
    public int btnWidth = 18;
    public int btnHeight = 1;
    public int centerX;

    public MonstrumScreen()
    {

        centerX = (GameSettings.GAME_WIDTH - btnWidth) / 2;

        //Get the rexpaint image and save it as a variable
        var rexDocument = SadConsole.Readers.REXPaintImage.Load(System.IO.File.OpenRead(_monsterScreenPath));

        var surface = rexDocument.ToCellSurface();
        var baseLayer = surface[0];

        // Create a surface that's the same size as the screen.
        _mainSurface = new ScreenSurface(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT);

        baseLayer.Copy(_mainSurface.Surface);

        _uiConsole = new ControlsConsole(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT);

        _uiConsole.UseKeyboard = true;
        _uiConsole.UseMouse = true;

        _uiConsole.Surface.DefaultBackground = Color.Transparent;
        _uiConsole.Surface.Clear();

    }
}