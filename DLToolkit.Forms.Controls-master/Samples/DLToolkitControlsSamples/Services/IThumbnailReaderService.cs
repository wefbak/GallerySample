using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static DLToolkitControlsSamples.MainPageModel;

namespace DLToolkitControlsSamples.Services
{
    public interface IThumbnailReaderService
    {
        Task GetAllThumbnails(ObservableCollection<ItemModel> items);
    }
}
