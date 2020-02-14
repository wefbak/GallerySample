using System;
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

                //Task[] tasks = new Task[fetchResults.Count];

                //var imageMgr = new PHImageManager();

                //for (nint i = 0; i < fetchResults.Count; i++)
                //{
                //    var tcs = new TaskCompletionSource<bool>();
                //    tasks[i] = tcs.Task;

                //    object locker = new object();

                //    imageMgr.RequestImageForAsset(
                //            (PHAsset)fetchResults[i],
                //            new CoreGraphics.CGSize(256, 256),
                //            PHImageContentMode.AspectFill, new PHImageRequestOptions(),
                //            (img, info) =>
                //            {
                //                lock (locker)
                //                {
                //                    if (tcs != null && (img.Size.Width >= 256 || img.Size.Height >= 256))
                //                    {
                //                        items.Add(new ItemModel { Source = ImageSource.FromStream(img.AsPNG().AsStream) });
                //                        tcs.SetResult(true);
                //                        tcs = null;
                //                    }
                //                }
                //            }
                //        );
                //}

                //Task.WaitAll(tasks);
            });
        }

    }
}
