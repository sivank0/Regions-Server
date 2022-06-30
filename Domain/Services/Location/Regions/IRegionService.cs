using Domain.Location.Regions;
using Tools.Types;

namespace Domain.Services.Location.Regions;
public interface IRegionService
{
    public Result SaveRegion(RegionBlank regionBlank);
    public Result RemoveRegion(Guid id);
    public Region? GetRegion(Guid id);
    public Page<Region> GetRegionsPage(Int32 page, Int32 countInPage, String searchingText);
}
