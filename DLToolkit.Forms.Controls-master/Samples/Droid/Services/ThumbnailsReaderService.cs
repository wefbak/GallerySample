using System.Collections.Generic;
using Xamarin.Forms;
using DLToolkitControlsSamples.Services;
using Android.Provider;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using static DLToolkitControlsSamples.MainPageModel;

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
                string[] projection = { MediaStore.Images.Thumbnails.Data };

                var cursor = MainActivity.Current.ContentResolver.Query(MediaStore.Images.Thumbnails.ExternalContentUri,
                    projection, // Which columns to return
                    null,       // Return all rows
                    null,
                    null);

                int columnIndex = cursor.GetColumnIndex(MediaStore.Images.Thumbnails.Data);

                while (cursor.MoveToNext())
                {
                    var path = cursor.GetString(columnIndex);

                    items.Add(new ItemModel { Source = ImageSource.FromFile(path) });
                }
                cursor.Close();
                cursor.Dispose();
            });
        }

    }
}
