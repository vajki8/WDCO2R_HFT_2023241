using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDCO2R_HFT_2023241.Models;
using WDCO2R_HFT_2023241.Repository;

namespace WDCO2R_HFT_2023241.Logic
{
    class BoardGameLogic : IBoardGameLogic
    {
        IBoardGameRepository Boardgamerepo;
        public BoardGameLogic(IBoardGameRepository boardgamerepo)
        {
            Boardgamerepo = boardgamerepo;
        }

        public void Create(BoardGames boardgame)
        {
            if(boardgame.Title == null)
            {
                throw new NullReferenceException();
            }
            else if(boardgame.Type == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                Boardgamerepo.Create(boardgame);
            }
        }

        public void Delete(int boardgameId)
        {
            if(boardgameId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Boardgamerepo.Delete(boardgameId);
            }
        }

        public BoardGames Read(int boardgameId)
        {
            if(boardgameId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                return Boardgamerepo.Read(boardgameId);
            }
        }

        public IEnumerable<BoardGames> ReadAll()
        {
            return Boardgamerepo.ReadAll();
        }

        public void Update(BoardGames boardgame)
        {
            if (boardgame == null)
            {
                throw new NullReferenceException();
            }
            else if (boardgame.Title == null)
            {
                throw new NullReferenceException();
            }
            else if (boardgame.Type == null)
            {
                throw new NullReferenceException();
            }
            else if (boardgame.BoardGameId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Boardgamerepo.Update(boardgame);
            }

        }
    }
}
