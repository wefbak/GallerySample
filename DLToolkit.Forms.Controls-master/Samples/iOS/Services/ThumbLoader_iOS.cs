using System;
using System.Collections;
using System.Threading.Tasks;
using DLToolkitControlsSamples.Services;
using Photos;
using Xamarin.Forms;
using static DLToolkitControlsSamples.MainPageModel;

namespace DLToolkitControlsSamples.iOS.Services
{
    public class ThumbLoader_iOS : IThumbLoader
    {
        PHAsset _asset;

        static PHImageManager _imageMgr = new PHImageManager();

        static Queue _requestsQueue = new Queue();

        public ThumbLoader_iOS(PHAsset asset)
        {
            _asset = asset;
            
        }

        public async Task GetThumbnailSource(ItemModel item)
        {
            await Task.Run(() =>
            {
                var requestId = _imageMgr.RequestImageForAsset(
                        _asset,
                        new CoreGraphics.CGSize(256, 256),
                        PHImageContentMode.AspectFill, new PHImageRequestOptions(),
                        async (img, info) =>
                        {
                            if (img?.Size != null && (img.Size.Width >= 256 || img.Size.Height >= 256))
                            {
                                await Task.Run(() =>
                                {
                                    var imgSource = ImageSource.FromStream(img.AsPNG().AsStream);
                                    item.Source = imgSource;
                                }).ConfigureAwait(false);
                            }
                        }
                    );

                Console.WriteLine($"ID: {requestId}");

                _requestsQueue.Enqueue(requestId);

                if (_requestsQueue.Count > 40)
                {
                    var oldRequestId = (int)_requestsQueue.Dequeue();

                    var t = Task.Run(() => { _imageMgr.CancelImageRequest(oldRequestId); });
                }
            }).ConfigureAwait(false);            
        }
    }
}
