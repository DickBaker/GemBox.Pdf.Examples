﻿using System.Windows;
using GemBox.Pdf;
using Microsoft.Win32;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        ComponentInfo.SetLicense("FREE-LIMITED-KEY");
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        var fileDialog = new OpenFileDialog
        {
            Filter = "PDF files (*.pdf)|*.pdf"
        };

        if (fileDialog.ShowDialog() == true)
        {
            using var document = PdfDocument.Load(fileDialog.FileName);
            ImageControl.Source = document.ConvertToImageSource(SaveOptions.Image);
        }
    }
}