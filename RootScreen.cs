namespace RogueHunters.Scenes;

using SadConsole;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadConsole.Readers;
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

        // Open Filestream to .xp file
        using (FileStream stream = File.OpenRead(_tittleScreenPath)) 
        {
            // Load image from stream
            REXPaintImage rexImage = REXPaintImage.Load(stream);

            // Convert Rexpaint Image to CellSurface
            ICellSurface cellSurface = rexImage.ToCellSurface()[0];

            // Wrap the surface inside a screensurface so it can be rendered
            _mainSurface =  new ScreenSurface(cellSurface);
        }
        
        //Create a new ControlConsole to hold the menu buttons
        _uiConsole = new ControlsConsole(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT);

        _uiConsole.UseKeyboard = true;
        _uiConsole.UseMouse = true;

        // set default background to transparent
        _uiConsole.Surface.DefaultBackground = Color.Transparent;

        // Clear surface so it adopts transparent color
        _uiConsole.Surface.Clear();

        //Create the buttons
        var btnNewHunter = new Button(btnWidth, btnHeight) 
        {
            Text = "New Hunter",
            Position = new Point(centerX, 40)
        };
        btnNewHunter.Click += BtnNewHunter_Click;
        _uiConsole.Controls.Add(btnNewHunter);

        var btnStartHutning = new Button(btnWidth, btnHeight)
        {
            Text = "Start Hunting",
            Position = new Point(centerX, 42)
        };
        _uiConsole.Controls.Add(btnStartHutning);

        var btnMonstrum = new Button(btnWidth, btnHeight)
        {
            Text = "Monstrum",
            Position = new Point(centerX, 44)
        };
        _uiConsole.Controls.Add(btnMonstrum);

        var btnOptions = new Button(btnWidth, btnHeight)
        {
            Text = "Options",
            Position = new Point(centerX, 46)
        };
        _uiConsole.Controls.Add(btnOptions);

        var btnExit = new Button(btnWidth, btnHeight)
        {
            Text = "Exit",
            Position = new Point(centerX, 48)
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
