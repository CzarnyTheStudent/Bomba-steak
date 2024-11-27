namespace GameTools
{
    public static class SetUpCoordinator
    {
        private static GameSetup _gameSetup;

        public static void RegisterGameSetup(GameSetup setup)
        {
            _gameSetup = setup;
        }

        public static GameSetup GetGameSetup() => _gameSetup;
    }
}