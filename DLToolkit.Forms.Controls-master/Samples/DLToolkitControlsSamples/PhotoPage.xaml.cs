using Xamarin.Forms;
using static DLToolkitControlsSamples.MainPageModel;

namespace DLToolkitControlsSamples
{
    public partial class PhotoPage : ContentPage
    {
        public PhotoPage(ItemModel itemModel)
        {
            InitializeComponent();

            photo.BindingContext = itemModel;
        }
    }
}
