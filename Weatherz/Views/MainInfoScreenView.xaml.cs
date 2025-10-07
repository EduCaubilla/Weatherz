using System;
using Microsoft.Maui.Controls;
using Weatherz.ViewModels;

namespace Weatherz
{
    public partial class MainInfoScreenView : ContentPage
    {
        private MainInfoScreenViewModel vm;

        public MainInfoScreenView()
        {
            InitializeComponent();

            vm = new MainInfoScreenViewModel();
            BindingContext = vm;

            // Apply initial gradient based on IsDaytime
            ApplyGradient(vm.IsDaytime);

            // Listen for property changes to update gradient when IsDaytime changes
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (vm != null)
            {
                await vm.OnAppearingAsync();
            }
        }

        private void Vm_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainInfoScreenViewModel.IsDaytime))
            {
                ApplyGradient(vm.IsDaytime);
            }
        }

        private void ApplyGradient(bool isDay)
        {
            // DynamicBrush is declared in XAML as a LinearGradientBrush; update its GradientStops
            if (DynamicBrush != null)
            {
                DynamicBrush.GradientStops.Clear();
                if (isDay)
                {
                    DynamicBrush.GradientStops.Add(new GradientStop { Color = Color.FromArgb("#86C8FF"), Offset = 0 });
                    DynamicBrush.GradientStops.Add(new GradientStop { Color = Color.FromArgb("#49B0FF"), Offset = 1 });
                }
                else
                {
                    DynamicBrush.GradientStops.Add(new GradientStop { Color = Color.FromArgb("#1D293C"), Offset = 0 });
                    DynamicBrush.GradientStops.Add(new GradientStop { Color = Color.FromArgb("#0E223B"), Offset = 1 });
                }
            }
        }
    }
}
