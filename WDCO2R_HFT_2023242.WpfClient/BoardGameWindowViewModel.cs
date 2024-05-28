using WDCO2R_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WDCO2R_HFT_2023242.WpfClient
{
    public class BoardGameWindowViewModel
    {
        public RestCollection<BoardGames> BoardGames { get; set; }

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
                BoardGames = new RestCollection<BoardGames>("http://localhost:35357/", "boradgames");
            }
        }
    }
}
