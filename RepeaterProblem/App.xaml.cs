﻿using Avalonia;
using Avalonia.Markup.Xaml;

namespace RepeaterProblem
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
