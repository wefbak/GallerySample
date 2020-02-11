using System.Collections.Generic;
using Xamarin.Forms;
using DLToolkitControlsSamples.Services;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using static DLToolkitControlsSamples.MainPageModel;

[assembly: Dependency(typeof(DLToolkitControlsSamples.iOS.Services.ThumbnailReaderService))]
namespace DLToolkitControlsSamples.iOS.Services
{
    public class ThumbnailReaderService : IThumbnailReaderService
    {
        public ThumbnailReaderService()
        {
        }

        public async Task GetAllThumbnails(ObservableCollection<ItemModel> items)
        {


            items.Add(new ItemModel { ImageUrl = "https://farm9.staticflickr.com/8625/15806486058_7005d77438.jpg" });
            items.Add(new ItemModel { ImageUrl = "https://farm5.staticflickr.com/4011/4308181244_5ac3f8239b.jpg" });
            items.Add(new ItemModel{ ImageUrl = "https://farm8.staticflickr.com/7423/8729135907_79599de8d8.jpg" });
            items.Add(new ItemModel{ ImageUrl = "https://farm3.staticflickr.com/2475/4058009019_ecf305f546.jpg" });
            items.Add(new ItemModel{ ImageUrl = "https://farm6.staticflickr.com/5117/14045101350_113edbe20b.jpg" });
            items.Add(new ItemModel{ ImageUrl = "https://farm2.staticflickr.com/1227/1116750115_b66dc3830e.jpg" });
            items.Add(new ItemModel{ ImageUrl = "https://farm8.staticflickr.com/7351/16355627795_204bf423e9.jpg" });
            items.Add(new ItemModel{ ImageUrl = "https://farm1.staticflickr.com/44/117598011_250aa8ffb1.jpg" });
            items.Add(new ItemModel{ ImageUrl = "https://farm8.staticflickr.com/7524/15620725287_3357e9db03.jpg" });
            items.Add(new ItemModel { ImageUrl = "https://farm9.staticflickr.com/8351/8299022203_de0cb894b0.jpg" });

            return thumbs;
        }

    }
}
