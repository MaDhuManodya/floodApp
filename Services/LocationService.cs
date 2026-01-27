using FloodApp.Models;

namespace FloodApp.Services;

public class LocationService
{
    private readonly List<Province> _provinces;
    private readonly List<District> _districts;
    private readonly List<Town> _towns;

    public LocationService()
    {
        // Data Seed
        // Provinces
        _provinces = new List<Province>
        {
            new() { Id = 1, Name = "Western", Coords = new(6.9016, 80.0088) },
            new() { Id = 2, Name = "Central", Coords = new(7.2906, 80.6337) },
            new() { Id = 3, Name = "Southern", Coords = new(6.0535, 80.2210) },
            new() { Id = 4, Name = "Northern", Coords = new(9.6615, 80.0255) },
            new() { Id = 5, Name = "Eastern", Coords = new(7.7102, 81.6924) },
            new() { Id = 6, Name = "North Western", Coords = new(7.7601, 80.1206) },
            new() { Id = 7, Name = "North Central", Coords = new(8.3350, 80.4108) },
            new() { Id = 8, Name = "Uva", Coords = new(6.8649, 81.1802) },
            new() { Id = 9, Name = "Sabaragamuwa", Coords = new(6.7041, 80.3756) }
        };

        // Districts (Mapped 25 districts)
        _districts = new List<District>
        {
            // Western
            new() { Id = 1, ProvinceId = 1, Name = "Colombo", Coords = new(6.9271, 79.8612) },
            new() { Id = 2, ProvinceId = 1, Name = "Gampaha", Coords = new(7.0840, 79.9939) },
            new() { Id = 3, ProvinceId = 1, Name = "Kalutara", Coords = new(6.5854, 79.9607) },
            // Central
            new() { Id = 4, ProvinceId = 2, Name = "Kandy", Coords = new(7.2906, 80.6337) },
            new() { Id = 5, ProvinceId = 2, Name = "Matale", Coords = new(7.4667, 80.6167) },
            new() { Id = 6, ProvinceId = 2, Name = "Nuwara Eliya", Coords = new(6.9497, 80.7891) },
            // Southern
            new() { Id = 7, ProvinceId = 3, Name = "Galle", Coords = new(6.0321, 80.2170) },
            new() { Id = 8, ProvinceId = 3, Name = "Matara", Coords = new(5.9500, 80.5333) },
            new() { Id = 9, ProvinceId = 3, Name = "Hambantota", Coords = new(6.1245, 81.1010) },
            // Northern
            new() { Id = 10, ProvinceId = 4, Name = "Jaffna", Coords = new(9.6615, 80.0255) },
            new() { Id = 11, ProvinceId = 4, Name = "Kilinochchi", Coords = new(9.3802, 80.3770) },
            new() { Id = 12, ProvinceId = 4, Name = "Mannar", Coords = new(8.9766, 79.9043) },
            new() { Id = 13, ProvinceId = 4, Name = "Vavuniya", Coords = new(8.7514, 80.4971) },
            new() { Id = 14, ProvinceId = 4, Name = "Mullaitivu", Coords = new(9.2671, 80.8142) },
            // Eastern
            new() { Id = 15, ProvinceId = 5, Name = "Batticaloa", Coords = new(7.7102, 81.6924) },
            new() { Id = 16, ProvinceId = 5, Name = "Ampara", Coords = new(7.2833, 81.6667) },
            new() { Id = 17, ProvinceId = 5, Name = "Trincomalee", Coords = new(8.5667, 81.2333) },
            // North Western
            new() { Id = 18, ProvinceId = 6, Name = "Kurunegala", Coords = new(7.4833, 80.3667) },
            new() { Id = 19, ProvinceId = 6, Name = "Puttalam", Coords = new(8.0362, 79.8283) },
            // North Central
            new() { Id = 20, ProvinceId = 7, Name = "Anuradhapura", Coords = new(8.3114, 80.4037) },
            new() { Id = 21, ProvinceId = 7, Name = "Polonnaruwa", Coords = new(7.9403, 81.0188) },
            // Uva
            new() { Id = 22, ProvinceId = 8, Name = "Badulla", Coords = new(6.9934, 81.0550) },
            new() { Id = 23, ProvinceId = 8, Name = "Monaragala", Coords = new(6.8714, 81.3487) },
            // Sabaragamuwa
            new() { Id = 24, ProvinceId = 9, Name = "Ratnapura", Coords = new(6.7056, 80.3847) },
            new() { Id = 25, ProvinceId = 9, Name = "Kegalle", Coords = new(7.2513, 80.3464) }
        };

        // Sample Towns
        _towns = new List<Town>
        {
            // Western Province
            // Colombo (ID: 1)
            new() { Id = 1, DistrictId = 1, Name = "Colombo", Coords = new(6.9271, 79.8612) },
            new() { Id = 2, DistrictId = 1, Name = "Dehiwala-Mount Lavinia", Coords = new(6.8511, 79.8659) },
            new() { Id = 3, DistrictId = 1, Name = "Moratuwa", Coords = new(6.7991, 79.8767) },
            new() { Id = 4, DistrictId = 1, Name = "Sri Jayawardenepura Kotte", Coords = new(6.9108, 79.8878) },
            new() { Id = 5, DistrictId = 1, Name = "Maharagama", Coords = new(6.8494, 79.9236) },
            new() { Id = 6, DistrictId = 1, Name = "Homagama", Coords = new(6.8412, 79.9984) },
            new() { Id = 7, DistrictId = 1, Name = "Avissawella", Coords = new(6.9543, 80.2046) },
            new() { Id = 8, DistrictId = 1, Name = "Piliyandala", Coords = new(6.8018, 79.9227) },
            // Gampaha (ID: 2)
            new() { Id = 9, DistrictId = 2, Name = "Gampaha", Coords = new(7.0840, 79.9939) },
            new() { Id = 10, DistrictId = 2, Name = "Negombo", Coords = new(7.2008, 79.8737) },
            new() { Id = 11, DistrictId = 2, Name = "Wattala", Coords = new(6.9897, 79.8924) },
            new() { Id = 12, DistrictId = 2, Name = "Ja-Ela", Coords = new(7.0766, 79.8906) },
            new() { Id = 13, DistrictId = 2, Name = "Kelaniya", Coords = new(6.9554, 79.9173) },
            new() { Id = 14, DistrictId = 2, Name = "Minuwangoda", Coords = new(7.1704, 79.9482) },
            new() { Id = 15, DistrictId = 2, Name = "Katunayake", Coords = new(7.1758, 79.8727) },
            new() { Id = 16, DistrictId = 2, Name = "Divulapitiya", Coords = new(7.2403, 80.0123) },
            // Kalutara (ID: 3)
            new() { Id = 17, DistrictId = 3, Name = "Kalutara", Coords = new(6.5854, 79.9607) },
            new() { Id = 18, DistrictId = 3, Name = "Panadura", Coords = new(6.7115, 79.9074) },
            new() { Id = 19, DistrictId = 3, Name = "Beruwala", Coords = new(6.4789, 79.9828) },
            new() { Id = 20, DistrictId = 3, Name = "Horana", Coords = new(6.7154, 80.0626) },
            new() { Id = 21, DistrictId = 3, Name = "Matugama", Coords = new(6.5222, 80.1137) },
            new() { Id = 22, DistrictId = 3, Name = "Aluthgama", Coords = new(6.4357, 80.0019) },
            new() { Id = 23, DistrictId = 3, Name = "Bandaragama", Coords = new(6.7155, 79.9856) },

            // Central Province
            // Kandy (ID: 4)
            new() { Id = 24, DistrictId = 4, Name = "Kandy", Coords = new(7.2906, 80.6337) },
            new() { Id = 25, DistrictId = 4, Name = "Gampola", Coords = new(7.1643, 80.5694) },
            new() { Id = 26, DistrictId = 4, Name = "Peradeniya", Coords = new(7.2657, 80.5960) },
            new() { Id = 27, DistrictId = 4, Name = "Nawalapitiya", Coords = new(7.0543, 80.5332) },
            new() { Id = 28, DistrictId = 4, Name = "Katugastota", Coords = new(7.3192, 80.6273) },
            new() { Id = 29, DistrictId = 4, Name = "Kundasale", Coords = new(7.2863, 80.6865) },
            // Matale (ID: 5)
            new() { Id = 30, DistrictId = 5, Name = "Matale", Coords = new(7.4667, 80.6167) },
            new() { Id = 31, DistrictId = 5, Name = "Dambulla", Coords = new(7.8600, 80.6500) },
            new() { Id = 32, DistrictId = 5, Name = "Sigiriya", Coords = new(7.9570, 80.7603) },
            new() { Id = 33, DistrictId = 5, Name = "Galewela", Coords = new(7.7719, 80.5678) },
            new() { Id = 34, DistrictId = 5, Name = "Ukuwela", Coords = new(7.4243, 80.6276) },
            // Nuwara Eliya (ID: 6)
            new() { Id = 35, DistrictId = 6, Name = "Nuwara Eliya", Coords = new(6.9497, 80.7891) },
            new() { Id = 36, DistrictId = 6, Name = "Hatton", Coords = new(6.8920, 80.5947) },
            new() { Id = 37, DistrictId = 6, Name = "Talawakele", Coords = new(6.9387, 80.6622) },
            new() { Id = 38, DistrictId = 6, Name = "Ragala", Coords = new(6.9833, 80.7667) },
            new() { Id = 39, DistrictId = 6, Name = "Ginigathena", Coords = new(6.9911, 80.4907) },

            // Southern Province
            // Galle (ID: 7)
            new() { Id = 40, DistrictId = 7, Name = "Galle", Coords = new(6.0321, 80.2170) },
            new() { Id = 41, DistrictId = 7, Name = "Hikkaduwa", Coords = new(6.1362, 80.1242) },
            new() { Id = 42, DistrictId = 7, Name = "Ambalangoda", Coords = new(6.2346, 80.0543) },
            new() { Id = 43, DistrictId = 7, Name = "Elpitiya", Coords = new(6.2570, 80.1438) },
            new() { Id = 44, DistrictId = 7, Name = "Karapitiya", Coords = new(6.0594, 80.2227) },
            new() { Id = 45, DistrictId = 7, Name = "Bentota", Coords = new(6.4173, 79.9961) },
            // Matara (ID: 8)
            new() { Id = 46, DistrictId = 8, Name = "Matara", Coords = new(5.9500, 80.5333) },
            new() { Id = 47, DistrictId = 8, Name = "Weligama", Coords = new(5.9739, 80.4294) },
            new() { Id = 48, DistrictId = 8, Name = "Akuressa", Coords = new(6.1132, 80.4727) },
            new() { Id = 49, DistrictId = 8, Name = "Dikwella", Coords = new(5.9818, 80.6865) },
            new() { Id = 50, DistrictId = 8, Name = "Kamburupitiya", Coords = new(6.0847, 80.5516) },
            new() { Id = 51, DistrictId = 8, Name = "Hakmana", Coords = new(6.0792, 80.6552) },
            // Hambantota (ID: 9)
            new() { Id = 52, DistrictId = 9, Name = "Hambantota", Coords = new(6.1245, 81.1010) },
            new() { Id = 53, DistrictId = 9, Name = "Tangalle", Coords = new(6.0243, 80.7937) },
            new() { Id = 54, DistrictId = 9, Name = "Tissamaharama", Coords = new(6.2804, 81.2874) },
            new() { Id = 55, DistrictId = 9, Name = "Ambalantota", Coords = new(6.1215, 81.0182) },
            new() { Id = 56, DistrictId = 9, Name = "Beliatta", Coords = new(6.0385, 80.7437) },

            // Northern Province
            // Jaffna (ID: 10)
            new() { Id = 57, DistrictId = 10, Name = "Jaffna", Coords = new(9.6615, 80.0255) },
            new() { Id = 58, DistrictId = 10, Name = "Chavakachcheri", Coords = new(9.6558, 80.1772) },
            new() { Id = 59, DistrictId = 10, Name = "Point Pedro", Coords = new(9.8252, 80.2333) },
            new() { Id = 60, DistrictId = 10, Name = "Nallur", Coords = new(9.6706, 80.0381) },
            new() { Id = 61, DistrictId = 10, Name = "Kankesanthurai", Coords = new(9.8167, 80.0333) },
            // Kilinochchi (ID: 11)
            new() { Id = 62, DistrictId = 11, Name = "Kilinochchi", Coords = new(9.3802, 80.3770) },
            new() { Id = 63, DistrictId = 11, Name = "Paranthan", Coords = new(9.4419, 80.4042) },
            new() { Id = 64, DistrictId = 11, Name = "Poonakary", Coords = new(9.5100, 80.2100) },
            // Mannar (ID: 12)
            new() { Id = 65, DistrictId = 12, Name = "Mannar", Coords = new(8.9766, 79.9043) },
            new() { Id = 66, DistrictId = 12, Name = "Talaimannar", Coords = new(9.0967, 79.7289) },
            new() { Id = 67, DistrictId = 12, Name = "Murunkan", Coords = new(8.8300, 80.0400) },
            // Vavuniya (ID: 13)
            new() { Id = 68, DistrictId = 13, Name = "Vavuniya", Coords = new(8.7514, 80.4971) },
            new() { Id = 69, DistrictId = 13, Name = "Cheddikulam", Coords = new(8.6811, 80.3150) },
            // Mullaitivu (ID: 14)
            new() { Id = 70, DistrictId = 14, Name = "Mullaitivu", Coords = new(9.2671, 80.8142) },
            new() { Id = 71, DistrictId = 14, Name = "Puthukkudiyiruppu", Coords = new(9.3000, 80.6833) },
            new() { Id = 72, DistrictId = 14, Name = "Mankulam", Coords = new(9.1122, 80.4578) },

            // Eastern Province
            // Trincomalee (ID: 17)
            new() { Id = 73, DistrictId = 17, Name = "Trincomalee", Coords = new(8.5667, 81.2333) },
            new() { Id = 74, DistrictId = 17, Name = "Kinniya", Coords = new(8.4988, 81.1895) },
            new() { Id = 75, DistrictId = 17, Name = "Kantale", Coords = new(8.3614, 81.0069) },
            new() { Id = 76, DistrictId = 17, Name = "Mutur", Coords = new(8.4552, 81.2662) },
            // Batticaloa (ID: 15)
            new() { Id = 77, DistrictId = 15, Name = "Batticaloa", Coords = new(7.7102, 81.6924) },
            new() { Id = 78, DistrictId = 15, Name = "Kattankudy", Coords = new(7.6897, 81.7247) },
            new() { Id = 79, DistrictId = 15, Name = "Eravur", Coords = new(7.7719, 81.6069) },
            new() { Id = 80, DistrictId = 15, Name = "Valaichchenai", Coords = new(7.9303, 81.5303) },
            // Ampara (ID: 16)
            new() { Id = 81, DistrictId = 16, Name = "Ampara", Coords = new(7.2833, 81.6667) },
            new() { Id = 82, DistrictId = 16, Name = "Kalmunai", Coords = new(7.4167, 81.8167) },
            new() { Id = 83, DistrictId = 16, Name = "Sainthamaruthu", Coords = new(7.4042, 81.8317) },
            new() { Id = 84, DistrictId = 16, Name = "Akkaraipattu", Coords = new(7.2142, 81.8483) },
            new() { Id = 85, DistrictId = 16, Name = "Pottuvil", Coords = new(6.8741, 81.8336) },

            // North Western Province
            // Kurunegala (ID: 18)
            new() { Id = 86, DistrictId = 18, Name = "Kurunegala", Coords = new(7.4833, 80.3667) },
            new() { Id = 87, DistrictId = 18, Name = "Kuliyapitiya", Coords = new(7.4686, 80.0406) },
            new() { Id = 88, DistrictId = 18, Name = "Narammala", Coords = new(7.4328, 80.2178) },
            new() { Id = 89, DistrictId = 18, Name = "Wariyapola", Coords = new(7.6186, 80.2319) },
            new() { Id = 90, DistrictId = 18, Name = "Pannala", Coords = new(7.3308, 79.9869) },
            new() { Id = 91, DistrictId = 18, Name = "Polgahawela", Coords = new(7.3325, 80.2978) },
            // Puttalam (ID: 19)
            new() { Id = 92, DistrictId = 19, Name = "Puttalam", Coords = new(8.0362, 79.8283) },
            new() { Id = 93, DistrictId = 19, Name = "Chilaw", Coords = new(7.5758, 79.7952) },
            new() { Id = 94, DistrictId = 19, Name = "Wennappuwa", Coords = new(7.3917, 79.8406) },
            new() { Id = 95, DistrictId = 19, Name = "Marawila", Coords = new(7.4169, 79.8211) },
            new() { Id = 96, DistrictId = 19, Name = "Dankotuwa", Coords = new(7.2917, 79.8833) },
            new() { Id = 97, DistrictId = 19, Name = "Kalpitiya", Coords = new(8.2294, 79.7594) },

            // North Central Province
            // Anuradhapura (ID: 20)
            new() { Id = 98, DistrictId = 20, Name = "Anuradhapura", Coords = new(8.3114, 80.4037) },
            new() { Id = 99, DistrictId = 20, Name = "Kekirawa", Coords = new(8.0436, 80.5906) },
            new() { Id = 100, DistrictId = 20, Name = "Medawachchiya", Coords = new(8.5367, 80.4908) },
            new() { Id = 101, DistrictId = 20, Name = "Thambuttegama", Coords = new(8.1561, 80.3017) },
            new() { Id = 102, DistrictId = 20, Name = "Mihintale", Coords = new(8.3503, 80.5039) },
            new() { Id = 103, DistrictId = 20, Name = "Nochchiyagama", Coords = new(8.2611, 80.2167) },
            // Polonnaruwa (ID: 21)
            new() { Id = 104, DistrictId = 21, Name = "Polonnaruwa", Coords = new(7.9403, 81.0188) },
            new() { Id = 105, DistrictId = 21, Name = "Kaduruwela", Coords = new(7.9450, 81.0250) },
            new() { Id = 106, DistrictId = 21, Name = "Hingurakgoda", Coords = new(8.0500, 80.9833) },
            new() { Id = 107, DistrictId = 21, Name = "Medirigiriya", Coords = new(8.1633, 80.9822) },

            // Uva Province
            // Badulla (ID: 22)
            new() { Id = 108, DistrictId = 22, Name = "Badulla", Coords = new(6.9934, 81.0550) },
            new() { Id = 109, DistrictId = 22, Name = "Bandarawela", Coords = new(6.8319, 80.9994) },
            new() { Id = 110, DistrictId = 22, Name = "Haputale", Coords = new(6.7689, 80.9575) },
            new() { Id = 111, DistrictId = 22, Name = "Ella", Coords = new(6.8667, 81.0467) },
            new() { Id = 112, DistrictId = 22, Name = "Mahiyanganaya", Coords = new(7.3197, 81.0514) },
            new() { Id = 113, DistrictId = 22, Name = "Welimada", Coords = new(6.9056, 80.9039) },
            // Monaragala (ID: 23)
            new() { Id = 114, DistrictId = 23, Name = "Monaragala", Coords = new(6.8714, 81.3487) },
            new() { Id = 115, DistrictId = 23, Name = "Kataragama", Coords = new(6.4136, 81.3325) },
            new() { Id = 116, DistrictId = 23, Name = "Wellawaya", Coords = new(6.7375, 81.1008) },
            new() { Id = 117, DistrictId = 23, Name = "Buttala", Coords = new(6.7578, 81.2425) },
            new() { Id = 118, DistrictId = 23, Name = "Bibile", Coords = new(7.1611, 81.2269) },

            // Sabaragamuwa Province
            // Ratnapura (ID: 24)
            new() { Id = 119, DistrictId = 24, Name = "Ratnapura", Coords = new(6.7056, 80.3847) },
            new() { Id = 120, DistrictId = 24, Name = "Balangoda", Coords = new(6.6472, 80.7028) },
            new() { Id = 121, DistrictId = 24, Name = "Embilipitiya", Coords = new(6.3356, 80.8525) },
            new() { Id = 122, DistrictId = 24, Name = "Pelmadulla", Coords = new(6.6264, 80.5358) },
            new() { Id = 123, DistrictId = 24, Name = "Eheliyagoda", Coords = new(6.8483, 80.2608) },
            // Kegalle (ID: 25)
            new() { Id = 124, DistrictId = 25, Name = "Kegalle", Coords = new(7.2513, 80.3464) },
            new() { Id = 125, DistrictId = 25, Name = "Mawanella", Coords = new(7.2536, 80.4450) },
            new() { Id = 126, DistrictId = 25, Name = "Warakapola", Coords = new(7.2272, 80.1989) },
            new() { Id = 127, DistrictId = 25, Name = "Rambukkana", Coords = new(7.3275, 80.3992) },
            new() { Id = 128, DistrictId = 25, Name = "Yatiyanthota", Coords = new(7.0428, 80.2972) }
        };
    }

    public List<Province> GetProvinces() => _provinces;
    
    public List<District> GetDistricts(int provinceId) 
        => _districts.Where(d => d.ProvinceId == provinceId).ToList();
        
    public List<Town> GetTowns(int districtId)
        => _towns.Where(t => t.DistrictId == districtId).ToList();

    public Town? GetTown(int townId) => _towns.FirstOrDefault(t => t.Id == townId);
}
