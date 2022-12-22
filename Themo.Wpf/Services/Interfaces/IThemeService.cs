using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Themo.Wpf.Events;

namespace Themo.Wpf.Services.Interfaces
{
    public interface IThemeService<TEnum> where TEnum : struct
    {
        event EventHandler<ThemeChangedEventArgs> ThemeChanged;
        void SetTheme(TEnum theme);
        TEnum GetCurrentTheme();
    }
}
