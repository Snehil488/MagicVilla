using MagicVilla_API.Models.DTO;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Name = "Mariana Beach", Occupancy = 12, Sqft=200},
            new VillaDTO { Id = 2, Name = "Treble Beach", Occupancy=6, Sqft=100}
        };
    }
}
