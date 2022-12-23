public static class ParamsController
{
    public static class Maze
    {
        public const int CELL_SIZE = 20;
        public const int MAZE_WIDTH = 3;
        public const int MAZE_HEIGHT = 3;
        public const int TRAPS_AMOUNT = 0;
    }

    public static class Player
    {
        public const float DELAY_BEFORE_MOVING = 2;
        public const float DELAY_AFTER_WIN = 2.5f;
        public const float DELAY_AFTER_DIE = 2f;
        public const float DELAY_BEFORE_DIE = 0.52f;
        public const float DELAY_FADE = 1f;
    }

    public static class Xml
    {
        public const string LANGUAGE_FILE_NAME = "Languages";
        public const string BEFORE_GROUP_ID = "Settings/group[@id='";
        public const string BEFORE_STRING_ID = "']/string[@id='";
        public const string BEFORE_CURRENT_LANGUAGE = "']/text[@lang='";
        public const string AFTER_CURRENT_LANGUAGE = "']";
    }

    public static class Localization
    {
        public const string MENU_PANEL_ID = "MenuPanel"; 
        public const string EXIT_BUTTON_ID = "ExitButton";
        public const string CONTINUE_BUTTON_ID = "ContinueButton";
    }
}
