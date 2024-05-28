using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;

namespace WDCO2R_HFT_2023241.Repository
{
    public interface IBoardGameRepository
    {
        void Create(BoardGame boardgame);
        void Delete(int boardgameid);
        BoardGame Read(int boardgameid);
        IQueryable<BoardGame> ReadAll();
        void Update(BoardGame boardgame);
    }
}
