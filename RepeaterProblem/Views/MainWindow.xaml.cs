using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Avalonia.Threading;

namespace RepeaterProblem.Views
{
    public class MainWindow : Window
    {
        private readonly ItemsRepeater _repeater;

        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<ItemViewModel> items = new ObservableCollection<ItemViewModel>();

            _repeater = this.Find<ItemsRepeater>("repeater");

            for (int i = 0; i < 10000; i++)
            {
                items.Add(new ItemViewModel(i));
            }

            _repeater.Items = items;
        }

        private void OnBringItemInToView(object sender, RoutedEventArgs e)
        {
            var item = _repeater.GetOrCreateElement(5000);

            //This will not work.
            item.BringIntoView();

            //If you just post BringIntoView it will not work.
            //Dispatcher.UIThread.Post(item.BringIntoView);

            //If you force layout update it will work.
            //if (VisualRoot is TopLevel top)
            //{
            //    top.LayoutManager.ExecuteLayoutPass();
            //}

            //item.BringIntoView();

            //Waiting for layout pass to happend will work.
            //void OnLayoutUpdate(object s, EventArgs e2)
            //{
            //    item.BringIntoView();

            //    LayoutUpdated -= OnLayoutUpdate;
            //}

            //LayoutUpdated += OnLayoutUpdate;

            //If you post with dilay that will also work.
            //Dispatcher.UIThread.Post(async () =>
            //{
            //    await Task.Delay(1000);
            //    item.BringIntoView();
            //});
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        class ItemViewModel
        {
            public string Title { get; }

            public ItemViewModel( int id)
            {
                Title = $"Item {id}";
            }
        }
    }
}
