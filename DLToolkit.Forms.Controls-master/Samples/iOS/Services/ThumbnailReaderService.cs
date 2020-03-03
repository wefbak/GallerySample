using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DLToolkitControlsSamples.Services;
using Foundation;
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

        public async Task GetAllThumbnails(List<ItemModel> items)
        {
            await Task.Run(() =>
            {
                PHFetchResult fetchResults = PHAsset.FetchAssets(PHAssetMediaType.Image, null);

                foreach (PHAsset asset in fetchResults)
                {                    
                    items.Add(new ItemModel { ThumbLoader = new ThumbLoader_iOS(asset), ModificationDate = asset.ModificationDate.ToDateTime() });
                }
            });
        }

        

    }

    public static class Utilsios
    {
        private static DateTime _nsRef = new DateTime(2001, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
        public static DateTime ToDateTime(this NSDate nsDate)
        {
            // We loose granularity below millisecond range but that is probably ok
            return _nsRef.AddSeconds(nsDate.SecondsSinceReferenceDate);
        }
    }
}
