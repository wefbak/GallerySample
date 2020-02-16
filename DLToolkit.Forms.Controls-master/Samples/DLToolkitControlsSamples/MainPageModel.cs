using System.Collections.ObjectModel;
using DLToolkitControlsSamples.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamvvm;

namespace DLToolkitControlsSamples
{
    public class MainPageModel : BasePageModel
	{
        public override void OnAppearing()
        {
            base.OnAppearing();

			ReloadData();
		}

        public async void ReloadData()
		{
			var list = new ObservableCollection<ItemModel>();

			var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

			if (status != PermissionStatus.Granted)
			{
				var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Photos);

				if (response[Permission.Photos] != PermissionStatus.Granted)
					return;
			}

			status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

			if (status != PermissionStatus.Granted)
			{
				var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);

				if (response.Count == 0 || response[Permission.Storage] != PermissionStatus.Granted)
					return;
			}

			await DependencyService.Get<IThumbnailReaderService>().GetAllThumbnails(list);

			Items = list;
		}

		public ImageSource TestImage
		{
			get { return GetField<ImageSource>(); }
			set { SetField(value); }
		}

		public ObservableCollection<ItemModel> Items
		{
			get { return GetField<ObservableCollection<ItemModel>>(); }
			set { SetField(value); }
		}

		public class ItemModel : BaseModel
		{
			IThumbLoader _thumbLoader;
			public IThumbLoader ThumbLoader
            {
				get { return _thumbLoader; }
				set
                {
					_thumbLoader = value;
					OnPropertyChanged(nameof(Source));
				}
            }

			ImageSource source;
			public ImageSource Source
			{
				get {
					if (source == null && _thumbLoader != null)
					{
						var t = ThumbLoader.GetThumbnailSource(this);
						return ImageSource.FromFile("image_loading.png");
					}

                    return source;
                }
				set { SetField(ref source, value); }
			}

			string fileName;
			public string FileName
			{
				get { return fileName; }
				set { SetField(ref fileName, value); }
			}
		}
	}
}
