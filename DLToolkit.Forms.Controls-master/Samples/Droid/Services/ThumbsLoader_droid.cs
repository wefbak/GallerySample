﻿using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Provider;
using DLToolkitControlsSamples.Services;
using Xamarin.Forms;
using static Android.Graphics.Bitmap;

namespace DLToolkitControlsSamples.Droid.Services
{
    public class ThumbsLoader_droid : IThumbLoader
    {
        long _imageID;
        Android.Net.Uri _uri;

        public ThumbsLoader_droid(long imageID, Android.Net.Uri uri)
        {
            _imageID = imageID;
            _uri = uri;
        }

        public async Task GetThumbnailSource(MainPageModel.ItemModel item)
        {
            await Task.Run(() =>
            {
                var cursor = MediaStore.Images.Thumbnails.QueryMiniThumbnail(MainActivity.Current.ContentResolver, _imageID, ThumbnailKind.MiniKind, null);
                if (cursor != null && cursor.Count > 0)
                {
                    cursor.MoveToFirst();
                    var thumbPath = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.Images.Thumbnails.Data));

                    item.ThumbSource = ImageSource.FromFile(thumbPath);

                    cursor.Close();
                    cursor.Dispose();
                }
                else
                {
                    item.ThumbSource = ImageSource.FromFile(_uri.Path);

                    Bitmap bitmap = MediaStore.Images.Thumbnails.GetThumbnail(
                        MainActivity.Current.ContentResolver, _imageID, ThumbnailKind.MiniKind,
                        (BitmapFactory.Options)null);

                    bitmap.Recycle();
                    bitmap.Dispose();
                }
            }).ConfigureAwait(false);
        }

        public async Task GetImageSource(MainPageModel.ItemModel item)
        {
            await Task.Run(() =>
            {
                item.ImgSource = ImageSource.FromFile(_uri.Path);
            }).ConfigureAwait(false);
        }
    }
}
