using System.Collections.Generic;
using System.Threading.Tasks;
using static DLToolkitControlsSamples.MainPageModel;

namespace DLToolkitControlsSamples.Services
{
    public interface IThumbnailReaderService
    {
        Task GetAllThumbnails(List<ItemModel> items);
    }
}
