using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DLToolkitControlsSamples.Services;
using Photos;
using Xamarin.Forms;
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
            await Task.Run(() =>
            {
                PHFetchResult fetchResults = PHAsset.FetchAssets(PHAssetMediaType.Image, null);

                foreach (PHAsset asset in fetchResults)
                {
                    items.Add(new ItemModel { ThumbLoader = new ThumbLoader_iOS(asset) });
                }
            });
        }

    }
}
