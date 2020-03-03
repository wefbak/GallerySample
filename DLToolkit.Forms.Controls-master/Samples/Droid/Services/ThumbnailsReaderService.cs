using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Provider;
using DLToolkitControlsSamples.Services;
using Java.IO;
using Xamarin.Forms;
using static DLToolkitControlsSamples.MainPageModel;

[assembly: Dependency(typeof(DLToolkitControlsSamples.Droid.Services.ThumbnailReaderService))]
namespace DLToolkitControlsSamples.Droid.Services
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
                string[] projection = { MediaStore.Images.Media.InterfaceConsts.Id, MediaStore.Images.Media.InterfaceConsts.Data, MediaStore.Images.Media.InterfaceConsts.DateModified };

                var cursor = MainActivity.Current.ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri,
                    projection, // Which columns to return
                    null,       // Return all rows
                    null,
                    null);

                int idxId = cursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Id);
                int idxPath = cursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.Data);
                int idxDate = cursor.GetColumnIndex(MediaStore.Images.Media.InterfaceConsts.DateModified);

                while (cursor.MoveToNext())
                {
                    var id = cursor.GetLong(idxId);
                    var path = cursor.GetString(idxPath);
                    var dt = cursor.GetDouble(idxDate);
                    var file = new File(path);

                    items.Add(new ItemModel { ThumbLoader = new ThumbsLoader_droid(id, Android.Net.Uri.FromFile(file)), ModificationDate=UnixTimeStampToDateTime(dt) });
                }

                cursor.Close();
                cursor.Dispose();
            });
        }

        public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
