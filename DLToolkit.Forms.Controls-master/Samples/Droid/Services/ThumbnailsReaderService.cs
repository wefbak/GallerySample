using System.Collections.Generic;
using Xamarin.Forms;
using DLToolkitControlsSamples.Services;
using Android.Provider;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using static DLToolkitControlsSamples.MainPageModel;
using Java.IO;

[assembly: Dependency(typeof(DLToolkitControlsSamples.Droid.Services.ThumbnailReaderService))]
namespace DLToolkitControlsSamples.Droid.Services
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
                string[] projection = { MediaStore.Images.Media.InterfaceConsts.Id, MediaStore.Images.Media.InterfaceConsts.Data };

                var cursor = MainActivity.Current.ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri,
                    projection, // Which columns to return
                    null,       // Return all rows
                    null,
                    null);

                int idxId = cursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Id);
                int idxPath = cursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Data);

                while (cursor.MoveToNext())
                {
                    var id = cursor.GetLong(idxId);
                    var path = cursor.GetString(idxPath);
                    var file = new File(path);

                    items.Add(new ItemModel { ThumbLoader = new ThumbsLoader_droid(id, Android.Net.Uri.FromFile(file)) });
                }

                cursor.Close();
                cursor.Dispose();
            });
        }

    }
}
