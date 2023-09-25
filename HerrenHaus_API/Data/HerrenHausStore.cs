using HerrenHaus_API.Models.Dto;

namespace HerrenHaus_API.Data
{
    public static class HerrenHausStore
    {
        public static List< HerrenHausDto> HerrenHausList = new List<HerrenHausDto>()
            {
                new HerrenHausDto { ID=1,Name="HerrenHaus am Strand",Location="Munish",price="10M $"},
                new HerrenHausDto { ID=2,Name="HerrenHaus im Berg",Location="Frankfort",price="20M $"}
            };
    }
}
