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
    private Label _monsterWeanesses;

    private Panel _monsterDescPanel;

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
        _monsterList.Position = new Point(2, 7);
        
        //Add the list to control surface
        _uiConsole.Controls.Add(_monsterList);

        _monsterList.SelectedItemChanged += selectedMonsterChanged;


        //Initialize Labels
        _monsterName = new Label(30) { Position = new Point(43,10) };
        _monsterOrigin = new Label(30) { Position = new Point(45, 12) };
        _monsterThreat = new Label(30) { Position = new Point(51, 14) };


        //Add labels to UI Console
        _uiConsole.Controls.Add(_monsterName);
        _uiConsole.Controls.Add(_monsterOrigin);
        _uiConsole.Controls.Add(_monsterThreat);

        //Add surfaces to Children
        //Maybe this is not the correct place to do this and this should be done in the Root Screen class
    }

    private void selectedMonsterChanged(object? sender, ListBox.SelectedItemEventArgs e) 
    {
        _uiConsole.IsDirty = true;
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

        //Display the appropriate information on the Panels
        _uiConsole.Print(37, 21, (ColoredString)monster.Description.WordWrap(39)); //Test to see if this actualy prints the description string in a desirable manner

        _uiConsole.Print(37, 26, (ColoredString)monster.Lore.WordWrap(39));


    }

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