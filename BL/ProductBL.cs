using System;
using System.Collections.Generic;
using DL;
using Models;

namespace BL
{
    public class GamesBL
    {
        private GameRepository _repo;
        public GamesBL(GameRepository p_repo)
        {
            _repo=p_repo;
        }
        public List<Games> GetAllGames()
        {
            return _repo.GetAllGames();
        }
        public List<Games> SearchGame(string p_game)
        {
            return _repo.SearchGames(p_game);
        }

    }
    public class SystemsBL
    {
        private SystemRepository _repo;
        public SystemsBL(SystemRepository p_repo)
        {
            _repo=p_repo;
        }
        public List<Systems> GetAllSystems()
        {
            return _repo.GetAllSystems();
        }
        public List<Systems> SearchSystems(string p_system)
        {
            return _repo.SearchSystems(p_system);
        }
    }
}