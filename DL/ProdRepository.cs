using System;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace DL
{
    public class GameRepository
    {
        private Entities.DemoDbContext _context;
        public GameRepository(Entities.DemoDbContext p_context)
        {
            _context=p_context;
        }
        public List<Games> GetAllGames()
        {
            List<int> age = new List<int>();
             return _context.Games.Select(
                game =>
                new Games()
                {
                    gName=game.GameName,
                    gReleaseDate=(DateTime)game.ReleaseDate,
                    gMSRP=(float)game.Msrp,
                    gSystem=game.OnSystem
                }).ToList();
        }
        public List<Games> SearchGames(string p_game)
        {
            return _context.Games
                .Where(game => game.GameName==p_game)
                .Select(
                game =>
                new Games()
                {
                    gName=game.GameName,
                    gReleaseDate=(DateTime)game.ReleaseDate,
                    gMSRP=(float)game.Msrp,
                    gSystem=game.OnSystem
                }).ToList(); 
        }
        
    }
    public class SystemRepository
    {
        private Entities.DemoDbContext _context;
        public SystemRepository(Entities.DemoDbContext p_context)
        {
            _context=p_context;
        }
        public List<Systems> GetAllSystems()
        {
             return _context.Systems.Select(
                syst =>
                new Systems()
                {
                    sName=syst.Name,
                    sMSRP=(float)syst.Msrp,
                    sReleaseDate=(DateTime)syst.ReleaseDate
                }
            ).ToList();
        }
        public List<Systems> SearchSystems(string p_system)
        {
            return _context.Systems
                .Where(syst => syst.Name == p_system)
            .Select(
                syst =>
                new Systems()
                {
                    sName=syst.Name,
                    sMSRP=(float)syst.Msrp,
                    sReleaseDate=(DateTime)syst.ReleaseDate
                }
            ).ToList();
        }
        
    }
}