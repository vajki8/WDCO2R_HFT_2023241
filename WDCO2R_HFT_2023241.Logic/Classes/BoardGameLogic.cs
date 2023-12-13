using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WDCO2R_HFT_2023241.Logic.InterFaces;
using WDCO2R_HFT_2023241.Models;
using WDCO2R_HFT_2023241.Repository;

namespace WDCO2R_HFT_2023241.Logic.Classes
{
    public class BoardGameLogic : IBoardGameLogic
    {
        IBoardGameRepository Boardgamerepo;
        public BoardGameLogic(IBoardGameRepository boardgamerepo)
        {
            Boardgamerepo = boardgamerepo;
        }

        public void Create(BoardGames boardgame)
        {
            if(boardgame.BoardGameId < 1)
            {
                throw new NullReferenceException("ID can't be less than 1");
            }
            else if (boardgame.Title == null || boardgame.Title == "")
            {
                throw new NullReferenceException("Name can't be empty");
            }
            else if (boardgame.Type == null || boardgame.Type == "")
            {
                throw new NullReferenceException("Type can't be empty");
            }
            else if (boardgame.Title.Length > 50 || boardgame.Type.Length >50)
            {
                throw new ArgumentException("The name or type is too long");
            }
            else
            {
                Boardgamerepo.Create(boardgame);
            }
        }

        public void Delete(int boardgameId)
        {
            if (boardgameId < 0)
            {
                throw new ArgumentOutOfRangeException("ID does not exist, can't be deleted");
            }
            else
            {
                Boardgamerepo.Delete(boardgameId);
            }
        }

        public BoardGames Read(int boardgameId)
        {
            if (boardgameId < 0)
            {
                throw new ArgumentOutOfRangeException("ID does not exist, can't be deleted");
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
            else if (boardgame.Title == null || boardgame.Title == "")
            {
                throw new NullReferenceException("Name can't be empty");
            }
            else if (boardgame.BoardGameId < 1)
            {
                throw new NullReferenceException("ID can't be less than 1");
            }
            else if (boardgame.Title.Length > 50 || boardgame.Type.Length > 50)
            {
                throw new ArgumentException("The name or type is too long");
            }
            else
            {
                Boardgamerepo.Update(boardgame);
            }

        }
    }
}
