# Semantic Searching using Embedding in WPF SfDataGrid

## Overview
This sample demonstrates how to implement semantic searching using embeddings in a **WPF SfDataGrid**. Semantic searching enables users to find products based on meaning and intent rather than exact keyword matching. When a user enters a search query, the application converts it into an embedding vector and calculates similarity scores against product embeddings to filter and display relevant results.

## About Semantic Searching using Embedding in WPF SfDataGrid

Semantic searching leverages **embedding technology** to understand the contextual meaning of search queries. Instead of traditional keyword matching, this approach:

- Converts search queries and product names into numerical vectors (embeddings)
- Calculates similarity scores between query vectors and product vectors
- Filters the SfDataGrid based on configurable similarity thresholds
- Enables more intuitive search results that capture semantic meaning

For example, searching for "kitchen blades" might return "KnifeSet" because the semantic meaning is understood, even though the exact words don't match.

## SmartComponents.LocalEmbeddings Package

The **SmartComponents.LocalEmbeddings** NuGet package provides local embedding generation capabilities:

```xml
<PackageReference Include="SmartComponents.LocalEmbeddings" Version="0.1.0-preview10148" />
```

This package includes:
- **LocalEmbedder**: Generates embeddings locally without external API calls
- **EmbeddingF32**: Stores embeddings as 32-bit floating-point vectors
- **Similarity calculation**: Computes cosine similarity between embedding vectors
- **Case sensitivity control**: Option to enable/disable case-sensitive embeddings

## XAML Implementation

```xml
<Window x:Class="SemanticSearch.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:SemanticSearch"
        mc:Ignorable="d"
        xmlns:syncfusion="http://schemas.syncfusion.com/wpf"
        Title="MainWindow" Height="450" Width="700" WindowStartupLocation="CenterScreen" >
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        <StackPanel Orientation="Horizontal" Margin="20">
            <TextBlock Text="Smart Filter:" Margin="0,0,5,0" FontWeight="Black"/>
            <TextBox KeyDown="textBox_KeyDown" Width="300" x:Name="searchtextBox"/>
            <TextBlock Text="Search Vector:" Margin="0,0,5,0" FontWeight="Black" Padding="10,0,0,0"/>
            <syncfusion:DoubleTextBox Value="0.55"  Width="150" x:Name="vectortextBox" ValueChanged="vectortextBox_ValueChanged"/>
        </StackPanel>
        <syncfusion:SfDataGrid Margin="30,0, 30, 20" Grid.Row="1" x:Name="grid" ColumnSizer="Star" AutoGenerateColumns="True" HeaderRowHeight="28"/>
        
    </Grid>
</Window>
```

**UI Components:**
- **Smart Filter TextBox**: Input field for semantic search queries
- **Search Vector DoubleTextBox**: Adjustable similarity threshold (0.0 to 1.0) to control filter sensitivity
- **SfDataGrid**: Displays filtered product data with auto-generated columns

## Code-Behind Implementation

```csharp
using SmartComponents.LocalEmbeddings;
using System.Collections.Concurrent;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SemanticSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double? VectorValue = 0.55;

        readonly static LocalEmbedder Embedder = new LocalEmbedder(caseSensitive: false);
        public static Dictionary<int, EmbeddingF32> ProductEmbeddings { get; set; } = new Dictionary<int, EmbeddingF32>();

        EmbeddingF32 queryVector;
        public MainWindow()
        {
            InitializeComponent();

            //Set ItemsSource for DataGrid
            ProductInfoViewModel viewModel = new ProductInfoViewModel();
            grid.ItemsSource = viewModel.ProductDetails; 

            foreach (var expense in viewModel.ProductDetails)
            {
                ProductEmbeddings.TryAdd(expense.ProductID, Embedder.Embed($"{expense.ProductName}"));
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (string.IsNullOrEmpty(searchtextBox.Text))
                {
                    grid.View.Filter = null;
                    grid.View.RefreshFilter();
                    return;
                }
                queryVector = Embedder.Embed(searchtextBox.Text);
                grid.View.Filter = FilterRecords;
                grid.View.RefreshFilter();
            }
        }
        
        public bool FilterRecords(object o)
        {   
            var item = o as ProductInfo;
            if (SmartFilterRenderer.Similarity(ProductEmbeddings[item.ProductID], queryVector ) > VectorValue)
                return true;
           
            return false;
        }

        private void vectortextBox_ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VectorValue = vectortextBox.Value;
            if(grid != null && grid.View != null)
            {
                grid.View.Filter = FilterRecords;
                grid.View.RefreshFilter();
            }

        }
    }

    public static class SmartFilterRenderer
    {
        public static float Similarity<TEmbedding>(TEmbedding a, TEmbedding b) where TEmbedding : IEmbedding<TEmbedding>
        {
            return a.Similarity(b);
        }
    }
}
```

**Key Features:**

1. **Embedder Initialization**: Creates a `LocalEmbedder` instance with case-insensitive embeddings
2. **Product Embedding Generation**: During initialization, all product names are converted to embeddings and stored in a dictionary
3. **Search Query Handling**: When the user presses Enter in the search TextBox:
   - The query text is converted to an embedding vector
   - The SfDataGrid's filter is applied using the `FilterRecords` method
   - The grid is refreshed to display results
4. **Dynamic Filtering**: The `FilterRecords` method:
   - Compares the query vector similarity with each product's embedding
   - Includes items where similarity exceeds the threshold value
   - Returns matching items to display in SfDataGrid
5. **Adjustable Threshold**: The vector similarity value (default 0.55) can be adjusted in real-time to make filtering more or less strict
6. **Similarity Calculation**: The `SmartFilterRenderer` helper class provides a generic method to calculate cosine similarity between embeddings

## How It Works

1. On application load, product names are converted into embedding vectors
2. User enters a search query in the TextBox
3. The query is converted to an embedding vector using the same embedder
4. Similarity scores are calculated between the query vector and each product vector
5. Products with similarity scores above the threshold are displayed in the SfDataGrid
6. The threshold can be adjusted to make results more or less restrictive
