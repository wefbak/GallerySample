using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DLToolkitControlsSamples.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamvvm;

namespace DLToolkitControlsSamples
{
    public class MainPageModel : BasePageModel
	{
		bool didAppear = false;

        public override void OnAppearing()
        {
            base.OnAppearing();

			if (!didAppear)
			{
				ReloadData();
			}

			didAppear = true;
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

            Action<ItemModel> action = async (item) => { await GoToNextPage(item); };
			foreach (var i in list)
			{
				i.GotToNextTask = action;
			}

			Items = list;
		}

		async Task GoToNextPage(ItemModel item)
		{
			await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PhotoPage(item));
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
			public Action<ItemModel> GotToNextTask;

			public Command TappedCommand { get; private set; }

			public ItemModel()
			{
				TappedCommand = new Command(OnImageTapped);
			}

			void OnImageTapped()
			{
				GotToNextTask.Invoke(this);
			}


			IThumbLoader _thumbLoader;
			public IThumbLoader ThumbLoader
            {
				get { return _thumbLoader; }
				set
                {
					_thumbLoader = value;
					OnPropertyChanged(nameof(ThumbSource));
					OnPropertyChanged(nameof(ImgSource));
				}
            }

			ImageSource thumbSource;
			public ImageSource ThumbSource
			{
				get {
					if (thumbSource == null && _thumbLoader != null)
					{
						var t = ThumbLoader.GetThumbnailSource(this);
						return ImageSource.FromFile("image_loading.png");
					}

                    return thumbSource;
                }
				set { SetField(ref thumbSource, value); }
			}

			ImageSource imgSource;
			public ImageSource ImgSource
			{
				get
				{
					if (imgSource == null && _thumbLoader != null)
					{
						var t = ThumbLoader.GetImageSource(this);
						return null;
					}

					return imgSource;
				}
				set { SetField(ref imgSource, value); }
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
