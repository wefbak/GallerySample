using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DLToolkitControlsSamples.Services;
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
