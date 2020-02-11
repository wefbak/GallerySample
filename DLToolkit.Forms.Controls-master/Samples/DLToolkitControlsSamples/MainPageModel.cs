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

        public void ReloadData()
		{
			var list = new ObservableCollection<ItemModel>();

			Items = list;

			var t = DependencyService.Get<IThumbnailReaderService>().GetAllThumbnails(list);
		}

		public ObservableCollection<ItemModel> Items
		{
			get { return GetField<ObservableCollection<ItemModel>>(); }
			set { SetField(value); }
		}

		public class ItemModel : BaseModel
		{
			string imageUrl;
			public string ImageUrl
			{
				get { return imageUrl; }
				set { SetField(ref imageUrl, value); }
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
