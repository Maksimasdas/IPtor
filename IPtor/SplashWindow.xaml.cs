using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Threading;

namespace IPtor.Views
{
    public partial class SplashWindow : Window
    {
        private readonly List<string> _phrases = new List<string>
        {
            "Запуск приложения...",
            "Наведение порядка в рабочей зоне...",
            "Подгоняем механиков...",
            "Загрузка сохранения...",
            "Синхронизация с базой данных...",
            "Подключение к сети...",
            "Проверка целостности данных...",
            "Ожидание сигнала...",
            "Анализ атмосферы в коллективе...",
            "Загрузка лексикона механиков...",
            "Проверка оборудования...",
            "Устанавливаем ментальную связь с работниками...",
            "Загрузка актуальной прошивки...",
            "Проверяем все ли на месте...",
            "Проверка болтов, гаек, ключей...",
            "Всё готово. Запуск..."
        };

        private DispatcherTimer _timer;
        private int _currentIndex = 0;

        public SplashWindow()
        {
            InitializeComponent();
            StartPhraseRotation();
        }

        private void StartPhraseRotation()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(350) // чаще меняем
            };
            _timer.Tick += (s, e) =>
            {
                _currentIndex++;
                if (_currentIndex >= _phrases.Count)
                {
                    _currentIndex = _phrases.Count - 1; // остаёмся на последней
                    return;
                }
                StatusText.Text = _phrases[_currentIndex];
            };
            _timer.Start();
        }

        // Обновление текста извне — НЕ останавливаем таймер,
        // чтобы фразы продолжали меняться
        public void UpdateStatus(string message)
        {
            StatusText.Text = message;
        }

        protected override void OnClosed(EventArgs e)
        {
            _timer?.Stop();
            base.OnClosed(e);
        }
    }
}