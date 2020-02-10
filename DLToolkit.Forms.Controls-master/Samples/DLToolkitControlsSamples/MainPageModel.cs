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

			var images = DependencyService.Get<IThumbnailReaderService>().GetAllThumbnails();

			int number = 0;
			for (int n = 0; n < 20; n++)
			{
				for (int i = 0; i < images.Count; i++)
				{
					number++;
					var item = new ItemModel()
					{
						ImageUrl = images[i],
						FileName = string.Format("image_{0}.jpg", number),
					};

					list.Add(item);
				}
			}

			Items = list;
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
