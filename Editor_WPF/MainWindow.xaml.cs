using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Editor_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    


    public partial class MainWindow : Window
    {
        bool normalized = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        //size combobox add elements
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Combobox_Sizes.ItemsSource = new List<int> { 8,9,10,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35};
            Combobox_Colors.ItemsSource = typeof(Colors).GetProperties();
        }


        //main window

        private void Btnminsize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Btnfullscreen_Click(object sender, RoutedEventArgs e)
        {
            if (normalized == true)
            {
                WindowState = WindowState.Maximized;
                normalized = false;
            }
            else
                WindowState = WindowState.Normal;
        }

        private void Exitbtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //comboboxs

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Richtextbox1.Selection.IsEmpty)
                Richtextbox1.FontFamily = new FontFamily(Combobox_Fonts.SelectedItem.ToString());
            else
                Richtextbox1.Selection.ApplyPropertyValue(FontFamilyProperty, new FontFamily(Combobox_Fonts.SelectedItem.ToString()));
        }

        private void Combobox_Sizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string value = Combobox_Sizes.SelectedItem.ToString();
            string valuerichtextbox = Richtextbox1.FontSize.ToString();
            if (int.Parse(value)> int.Parse(valuerichtextbox))
            {
                for (int i = ((int)Richtextbox1.FontSize); i < int.Parse(value); i++)
                    Richtextbox1.FontSize++;
            }
            else
            {
                for (int i = ((int)Richtextbox1.FontSize); i > int.Parse(value); i--)
                    Richtextbox1.FontSize--;
            }
        }



        private void Combobox_Colors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Color selectedColor = (Color)(Combobox_Colors.SelectedItem as PropertyInfo).GetValue(null, null);
            this.Richtextbox1.Foreground = new SolidColorBrush(selectedColor);
        }


        //fonts

        private void Bold_Click(object sender, RoutedEventArgs e)
        {

            if (sender is Button b)
            {
                if (b.Content.ToString() == "B")
                    Richtextbox1.Selection.ApplyPropertyValue(RichTextBox.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            Richtextbox1.Selection.ApplyPropertyValue(RichTextBox.FontStyleProperty,FontStyles.Italic);
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button b)
            {
                if(b.Content.ToString()== "U")
                    Richtextbox1.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, TextDecorations.Underline);

            }
        }

        private void Normalize_Click(object sender, RoutedEventArgs e)
        {
            Richtextbox1.Selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
        }

        //Fonts size btn

        private void FontSize_Up(object sender, RoutedEventArgs e)
        {
            Richtextbox1.FontSize++;
        }

        private void FontSize_Down(object sender, RoutedEventArgs e)
        {
            Richtextbox1.FontSize--;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if(sender is MenuItem m)
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Text Files |*.txt";

                //if (op.ShowDialog() == DialogResult.OK)
                //{
                //    using StreamReader sr = new(op.FileName);
                //    Richtextbox1.Document.Blocks.Add(new Paragraph(new Run(sr.ReadToEnd())));
                //}
                
            }    
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();
            if(sv.ShowDialog() == true)
            {
                using StreamWriter sw = new(sv.FileName);
                sw.Write(new TextRange(Richtextbox1.Document.ContentStart, Richtextbox1.Document.ContentEnd).Text);
            }
        }
    }
}
