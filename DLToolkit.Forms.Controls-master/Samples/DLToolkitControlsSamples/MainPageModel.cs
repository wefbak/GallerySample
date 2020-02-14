using System.Collections.ObjectModel;
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

			//TestImage = await DependencyService.Get<IThumbnailReaderService>().GetFirstThumbail();

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
			ImageSource source;
			public ImageSource Source
			{
				get { return source; }
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
