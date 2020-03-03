using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DLToolkitControlsSamples.Services;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;

namespace DLToolkitControlsSamples
{
    public class MainPageModel : BaseViewModel
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
			var list = new List<ItemModel>();

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

			Title = $"My {list.Count} Photos";

			Action<ItemModel> action = async (item) => { await GoToNextPage(item); };
			foreach (var i in list)
			{
				i.GotToNextTask = action;
			}

			Items = new ObservableCollection<ItemModel>(list.OrderByDescending(x => x.ModificationDate));
		}

		async Task GoToNextPage(ItemModel item)
		{
			await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new PhotoPage(item));
        }

		string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		ImageSource _testImage;
		public ImageSource TestImage
		{
			get { return _testImage; }
			set { SetProperty(ref _testImage, value); }
		}

		ObservableCollection<ItemModel> _items;
		public ObservableCollection<ItemModel> Items
		{
			get { return _items; }
			set { SetProperty(ref _items, value); }
		}

		public class ItemModel : BaseViewModel
		{
			public Action<ItemModel> GotToNextTask;

			public DateTime ModificationDate { get; set; }

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
				set { SetProperty(ref thumbSource, value); }
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
				set { SetProperty(ref imgSource, value); }
			}

			string fileName;
			public string FileName
			{
				get { return fileName; }
				set { SetProperty(ref fileName, value); }
			}
		}
	}
}
