namespace RogueHunters.Scenes;

using SadConsole;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;
using System;

class RootScreen : ScreenObject
{
    private ScreenSurface _mainSurface;
    private ControlsConsole _uiConsole;
    private String _tittleScreenPath = "Assets/Screens/TittleScreen.xp";


    //Button Settings
    public int btnWidth = 18;
    public int btnHeight = 1;
    public int centerX;

    public RootScreen()
    {

        centerX = (GameSettings.GAME_WIDTH - btnWidth) / 2;

        //Get the rexpaint image and save it as a variable
        var rexDocument = SadConsole.Readers.REXPaintImage.Load(System.IO.File.OpenRead(_tittleScreenPath));
        
        var surface = rexDocument.ToCellSurface();
        var baseLayer = surface[0];

        // Create a surface that's the same size as the screen.
        _mainSurface = new ScreenSurface(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT);

        baseLayer.Copy(_mainSurface.Surface);

        _uiConsole = new ControlsConsole(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT);

        _uiConsole.UseKeyboard = true;
        _uiConsole.UseMouse = true;

        //Create the buttons
        var btnNewHunter = new Button(btnWidth, btnHeight) 
        {
            Text = "New Hunter",
            Position = new Point(centerX, 12)
        };
        btnNewHunter.Click += BtnNewHunter_Click;
        _uiConsole.Controls.Add(btnNewHunter);

        var btnStartHutning = new Button(btnWidth, btnHeight)
        {
            Text = "Start Hunting",
            Position = new Point(centerX, 14)
        };
        _uiConsole.Controls.Add(btnStartHutning);

        var btnMonstrum = new Button(btnWidth, btnHeight)
        {
            Text = "Monstrum",
            Position = new Point(centerX, 16)
        };
        _uiConsole.Controls.Add(btnMonstrum);

        var btnOptions = new Button(btnWidth, btnHeight)
        {
            Text = "Options",
            Position = new Point(centerX, 18)
        };
        _uiConsole.Controls.Add(btnOptions);

        var btnExit = new Button(btnWidth, btnHeight)
        {
            Text = "Exit",
            Position = new Point(centerX, 20)
        };
        btnExit.Click += BtnQuit_Click;
        _uiConsole.Controls.Add(btnExit);

        // Add _mainSurface as a child object of this one. This object, RootScreen, is a simple object
        // and doesn't display anything itself. Since _mainSurface is going to be a child of it, _mainSurface
        // will be displayed.
        Children.Add(_mainSurface);
        Children.Add(_uiConsole);
    }

    void BtnNewHunter_Click(object sender, EventArgs e) 
    {
        System.Diagnostics.Debug.WriteLine("New Hunter Button Clicked!");
    }

    void BtnStartHunting_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Start Hunting Button Clicked!");
    }

    void BtnMonstrum_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Monstrum Button Clicked!");
    }

    void BtnOptions_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Options Button Clicked!");
    }

    void BtnQuit_Click(object sender, EventArgs e) 
    {
        Game.Instance.MonoGameInstance.Exit();
    }
}
