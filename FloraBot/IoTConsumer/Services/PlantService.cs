using IoTConsumer.Models;
using System.Collections.Generic;

namespace IoTConsumer.Services
{
    public interface IPlantService
    {
        public PlantModel FindById(string id);
    }

    class LocalPlantService : IPlantService
    {
        private static readonly Dictionary<string, PlantModel> _plants = new Dictionary<string, PlantModel>
        {
            { "C4:7C:8D:6B:3B:DC", new PlantModel( id: "C4:7C:8D:6B:3B:DC",
                name: "Maranta",
                conductivity: ConductivityRangesModel.MediumRange,
                light: LightRangesModel.MediumRange,
                moisture: MoistureRanges.MediumRange,
                temperature: TemperatureRanges.HighRange)
            },
            { "80:EA:CA:88:F5:5D", new PlantModel( id: "80:EA:CA:88:F5:5D",
                name: "Monstera",
                conductivity: ConductivityRangesModel.MediumRange,
                light: LightRangesModel.MediumRange,
                moisture: MoistureRanges.MediumRange,
                temperature: TemperatureRanges.HighRange)
            },
        };

        public PlantModel FindById(string id)
        {
            return _plants.ContainsKey(id) ? _plants[id] : null;
        }
    }

    //class DatabasePlanService : IPlantService
    //{
    //    private readonly DbContext _context;

    //    public DatabasePlanService(DbContext context)
    //    {
    //        _context = context;
    //    }
    //    public PlantModel FindById(string id)
    //    {
    //        return _context.Plants.FindById(id);
    //    }
    //}
}
