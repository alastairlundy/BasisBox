using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using WCount.Library.Models;

namespace WCount.Desktop;

public partial class StatisticsWindow : Window
{
    public StatisticsWindow(WCountResult wcountResult)
    {
        InitializeComponent();
        
        LoadResults();
        LoadTranslations();     
    }

    protected void LoadTranslations()
    {
        this.lineCountLabel.Text = $"{Localizations.Resources.Counting_Labels_Lines}:";
        this.byteCountLabel.Text = $"{Localizations.Resources.Counting_Labels_Bytes}:";
        this.charCountLabel.Text = $"{Localizations.Resources.Counting_Labels_Characters}:";
        this.wordCountLabel.Text = $"{Localizations.Resources.Counting_Labels_Words}:";

        this.fileNameLabel.Text = $"{Localizations.Resources.Counting_Labels_File_Singular}:";
    }

    protected void LoadResults()
    {
        


      //  this.lineCountLabel.Text =
    }
}