namespace RogueHunters.Scenes;

using SadConsole;
using SadConsole.UI;
using SadConsole.UI.Controls;
using SadRogue.Primitives;
using System;
using System.Runtime.CompilerServices;

class MonstrumScreen : ScreenObject
{
    private ScreenSurface _mainSurface;
    private ControlsConsole _uiConsole;
    private string _monsterScreenPath = "Assets/Screens/MonsterGlossaryScreen.xp";
    private string _monsterFilePath = "Assets/Entities/MonsterEntries.json";
    private MonsterDatabase _monsterdb;
    private MonsterEntry[] _monsters;

    //UI
    private ListBox _monsterList;
    private Label _monsterName;
    private Label _monsterOrigin;
    private Label _monsterThreat;
    private Label _monsterDescription;
    private Label _monsterWeanesses;
    private Label _monsterLore;

    private Panel _monsterDescPanel;

    //Button Settings //tbd add this to game settings maybe
    public int btnWidth = 18;
    public int btnHeight = 1;
    public int centerX;

    public MonstrumScreen()
    {
        _monsterdb.LoadMonsters(_monsterFilePath);


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

        //Create the list of monsters
        _monsterList = new ListBox(18, 18);

        //Populate the list using the db
        foreach (MonsterEntry monster in _monsterdb.Monsters) 
        {
            _monsterList.Items.Add($"{monster.Id}.{monster.Name}");
        }

        //Position the list box
        _monsterList.Position = new Point(2, 7);
        
        //Add the list to control surface
        _uiConsole.Controls.Add(_monsterList);

        _monsterList.SelectedItemChanged += selectedMonsterChanged;


        //Initialize Labels
        _monsterName = new Label(30) { Position = new Point(43,10) };
        _monsterOrigin = new Label(30) { Position = new Point(45, 12) };
        _monsterThreat = new Label(30) { Position = new Point(51, 14) };

        //Initialize Panels
        _monsterDescPanel = new Panel(39, 6) { Position = new Point(37, 21)};

        //Add labels to UI Console
    }

    private void selectedMonsterChanged(object? sender, ListBox.SelectedItemEventArgs e) 
    {
        int selectedIndex = _monsterList.SelectedIndex;
        if (selectedIndex < 0) 
        {
            return;
        }

        MonsterEntry monster = _monsterdb.Monsters[selectedIndex];

        //Display the appropriate information on Labels
        _monsterName.DisplayText = $"{monster.Name}";
        _monsterOrigin.DisplayText = $"{monster.Origin}";
        _monsterThreat.DisplayText = $"{monster.ThreatLvl}";

        //Display the appropriate information on the Panels
        
    }
}