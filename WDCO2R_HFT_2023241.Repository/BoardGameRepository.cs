using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Repository
{
    public class BoardGameRepository : IBoardGameRepository
    {
        RentalDbContext context;

        public BoardGameRepository(RentalDbContext context)
        {
            this.context = context;
        }
        public void Create(BoardGames boardGames)
        {
            context.BoardGame.Add(boardGames);
            context.SaveChanges();
        }
        public void Delete(int boardgameid)
        {
            context.Remove(Read(boardgameid));
            context.SaveChanges();
        }

        public BoardGames Read(int boardgameid)
        {
            return context.BoardGame.FirstOrDefault(t => t.BoardGameId == boardgameid);
        }
        public void Update(BoardGames boardgames)
        {
            var updated = Read(boardgames.BoardGameId);
            updated.BoardGameId = boardgames.BoardGameId;
            updated.Title = boardgames.Title;
            updated.Type = boardgames.Type;
            context.SaveChanges();
        }
    }
}
//<