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