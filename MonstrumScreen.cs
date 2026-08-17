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
    private Label _monsterWeaknesses;

    //Button Settings //tbd add this to game settings maybe
    public int btnWidth = 18;
    public int btnHeight = 1;
    public int centerX;

    public MonstrumScreen()
    {
        _monsterdb = new MonsterDatabase();
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
        _monsterList.Position = new Point(3, 7);
        
        //Add the list to control surface
        _uiConsole.Controls.Add(_monsterList);

        _monsterList.SelectedItemChanged += selectedMonsterChanged;


        //Initialize Labels
        _monsterName = new Label(20) { Position = new Point(43,10) };
        _monsterOrigin = new Label(20) { Position = new Point(45, 12) };
        _monsterThreat = new Label(20) { Position = new Point(51, 14) };


        //Add labels to UI Console
        _uiConsole.Controls.Add(_monsterName);
        _uiConsole.Controls.Add(_monsterOrigin);
        _uiConsole.Controls.Add(_monsterThreat);
    }

    private void selectedMonsterChanged(object? sender, ListBox.SelectedItemEventArgs e) 
    {
        _uiConsole.IsDirty = true;

        //Clear the Description, Weakness and Lore sections
        _uiConsole.Clear(37, 21,39);
        _uiConsole.Clear(37, 22, 39);
        _uiConsole.Clear(37, 23, 39);
        _uiConsole.Clear(37, 24, 39);
        _uiConsole.Clear(37, 25, 39);
        _uiConsole.Clear(37, 26, 39);

        _uiConsole.Clear(37, 33, 39);
        _uiConsole.Clear(37, 34, 39);
        _uiConsole.Clear(37, 35, 39);
        _uiConsole.Clear(37, 36, 39);

        _uiConsole.Clear(37, 46, 39);
        _uiConsole.Clear(37, 47, 39);
        _uiConsole.Clear(37, 48, 39);
        _uiConsole.Clear(37, 49, 39);
        _uiConsole.Clear(37, 50, 39);
        _uiConsole.Clear(37, 51, 39);
        _uiConsole.Clear(37, 52, 39);

        int selectedIndex = _monsterList.SelectedIndex;
        if (selectedIndex < 0) 
        {
            return;
        }

        MonsterEntry monster = _monsterdb.Monsters[selectedIndex];

        //Display the appropriate information on Labels
        _monsterName.DisplayText = $"{monster.Name}";
        _monsterOrigin.DisplayText = $"{monster.Origin}";

        //Handle Threat Level Text and Color
        switch (monster.ThreatLvl)
        {
            case ThreatLevel.Low:
                _monsterThreat.DisplayText = "Low";
                break;
            case ThreatLevel.Medium:
                _monsterThreat.DisplayText = "Medium";
                break;
            case ThreatLevel.High:
                _monsterThreat.DisplayText = "High";
                break;
            case ThreatLevel.Extreme:
                _monsterThreat.DisplayText = "Extreme";
                break;
            default:
                break;
        }

        //Set the Threat Level Label Text Color
        _monsterThreat.TextColor = threatColor(monster.ThreatLvl);

        //Get wrapped lines for Description and print them line-by-line
        int descY = 21;
        foreach (string line in monster.Description.WordWrap(39))
        {
            _uiConsole.Print(37, descY++, line);
        }

        //Get Weakness entries and print them line-by-line
        int weaknessY = 33;
        foreach(string weakness in monster.Weaknesses) 
        {
            _uiConsole.Print(37, weaknessY++, weakness);
        }

        //Get wrapped lines for Lore and print them line-by-line
        int loreY = 46;
        foreach (string line in monster.Lore.WordWrap(39))
        {
            _uiConsole.Print(37, loreY++, line);
        }

    }

    public ScreenSurface GetMainSurface() 
    {
        return _mainSurface;
    }

    public ControlsConsole GetControlConsole() 
    {
        return _uiConsole;
    }

    #region Getting the previous Console and Surface to be used in the Back Button
    public ScreenSurface SetPreviousScreenSurface(ScreenSurface screenSurface) 
    {
        ScreenSurface previousScreen = screenSurface;
        return previousScreen;
    }

    public ControlsConsole SetPreviousControlsConsole(ControlsConsole controlsConsole) 
    {
        ControlsConsole previousControlsConsole = controlsConsole;
        return previousControlsConsole;
    }
    #endregion

    private Color threatColor (ThreatLevel threatLvl) 
    {
        Color selectedColor = Color.AntiqueWhite;

        switch (threatLvl)
        {
            case ThreatLevel.Low: //Low
                selectedColor = Color.AnsiGreen;
                break;
            case ThreatLevel.Medium: //Medium
                selectedColor= Color.AnsiMagenta;
                break;
            case ThreatLevel.High: //High
                selectedColor = Color.AnsiRed;
                break;
            case ThreatLevel.Extreme: //Extreme
                selectedColor = Color.DarkRed;
                break;
            default:
                break;
        }

        return selectedColor;
    }
}