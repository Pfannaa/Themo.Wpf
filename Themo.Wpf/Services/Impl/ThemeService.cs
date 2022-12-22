using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Themo.Wpf.Events;
using Themo.Wpf.Services.Interfaces;

namespace Themo.Wpf.Services.Impl
{
    public class ThemeService<TEnum> : IThemeService<TEnum> where TEnum : struct
    {
        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

        private TEnum? _currentTheme;
        public TEnum? CurrentTheme
        {
            get
            {
                if (_currentTheme == null) _currentTheme = GetCurrentTheme();
                return _currentTheme;
            }
            set => _currentTheme = value;
        }

        private ResourceDictionary ThemeDictionary
        {
            get => Application.Current.Resources.MergedDictionaries[0];
            set => Application.Current.Resources.MergedDictionaries[0] = value;
        }

        private void ChangeTheme(Uri uri, TEnum theme)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
            ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(theme));
        }

        public void SetTheme(TEnum theme)
        {
            try
            {
                ChangeTheme(new Uri($"/Themes/{theme}.xaml", UriKind.Relative), theme);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEnum GetCurrentTheme()
        {
            var loadedDictionary = Application.Current.Resources.MergedDictionaries[0];
            string dictionaryContent = loadedDictionary.Source.ToString();
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().FirstOrDefault(x => dictionaryContent.EndsWith(x.ToString()));
        }
    }
}
