using WDCO2R_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

namespace WDCO2R_HFT_2023242.WpfClient
{
    public class BoardGameWindowViewModel : ObservableRecipient
    {
        public RestCollection<BoardGame> BoardGames { get; set; }

        private BoardGame selectedBoardGame;

        public ICommand CreateBoardGameCommand { get; set; }
        public ICommand UpdateBoardGameCommand { get; set; }
        public ICommand DeleteBoardGameCommand { get; set; }

        public BoardGame SelectedBoardGame
        {
            get { return selectedBoardGame; }
            set
            {
                if (value != null)
                {
                    selectedBoardGame = new BoardGame()
                    {
                        BoardGameId = value.BoardGameId,
                        Title = value.Title,
                        Type = value.Type,
                    };
                }
                OnPropertyChanged();
                (DeleteBoardGameCommand as RelayCommand).NotifyCanExecuteChanged();
                (UpdateBoardGameCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public BoardGameWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                BoardGames = new RestCollection<BoardGame>("http://localhost:35357/", "BoardGame", "hub");

                CreateBoardGameCommand = new RelayCommand(() =>
                {
                    BoardGames.Add(new BoardGame()
                    {
                        Title = SelectedBoardGame.Title,
                        Type = SelectedBoardGame.Type,
                    });
                });
                UpdateBoardGameCommand = new RelayCommand(() =>
                {
                    BoardGames.Update(SelectedBoardGame);
                });
                DeleteBoardGameCommand = new RelayCommand(() =>
                {
                    BoardGames.Delete(SelectedBoardGame.BoardGameId);
                },
                () => { return SelectedBoardGame != null; });
                SelectedBoardGame = new BoardGame() { Title = "New Game", Type ="new tpye" };
            }
        }
    }
}
