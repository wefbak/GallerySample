using System.Threading.Tasks;
using static DLToolkitControlsSamples.MainPageModel;

namespace DLToolkitControlsSamples.Services
{
    public interface IThumbLoader
    {
        Task GetThumbnailSource(ItemModel item);
    }
}
