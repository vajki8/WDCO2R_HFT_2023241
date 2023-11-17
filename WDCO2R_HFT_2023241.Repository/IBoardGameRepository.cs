using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Repository
{
    internal interface IBoardGameRepository
    {
        void Create(BoardGames boardgame);
        void Delete(int boardgameid);
        BoardGames Read(int boardgameid);
        IQueryable<BoardGames> ReadAll();
        void Update(BoardGames boardgame);
    }
}
