using HerrenHaus_API.Models.Dto;

namespace HerrenHaus_API.Data
{
    public static class HerrenHausStore
    {
        public static IEnumerable< HerrenHausDto> HerrenHausList = new List<HerrenHausDto>()
            {
                new HerrenHausDto { ID=1,Name="HerrenHaus am Strand"},
                new HerrenHausDto { ID=2,Name="HerrenHaus im Berg"}
            };
    }
}
