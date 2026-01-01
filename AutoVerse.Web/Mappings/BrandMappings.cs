using AutoVerse.Core.Entities;
using AutoVerse.Core.ViewModels;

namespace AutoVerse.Web.Mappings
{
    public static class BrandMappings
    {

        public static Brand ToEntity(BrandViewModel brandViewModel)
        {
            return new Brand
            {
                Id = brandViewModel.Id,
                Name = brandViewModel.Name
            };
        }
        public static IEnumerable<BrandViewModel> ToViewModels(IEnumerable<Brand> brand)
        {
            return brand.Select(b => b.ToViewModel());
        }

        public static BrandViewModel ToViewModel(this Brand brand)
        {
            return new BrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name
            };
        }
    }
}
