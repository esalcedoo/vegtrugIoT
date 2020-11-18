using IoTConsumer.Data;
using IoTConsumer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IoTConsumer.Services
{
    public class PlantService
    {
        private readonly FloraDBContext _context;

        public PlantService(FloraDBContext context)
        {
            _context = context;
        }

        public async Task ProcessFloraDeviceMessage(FloraDeviceMessageModel model)
        {
            var floraDevice = await _context.FloraDevices.FindAsync(model.DeviceId);

            if(floraDevice?.PlantId == null)
            {
                throw new InvalidOperationException();
            }

            floraDevice.Battery = model.Battery;

            var entry = new EntryEntity
            {
                Conductivity = model.Conductivity,
                Light = model.Light,
                Moisture = model.Moisture,
                Temperature = model.Temperature,
                Timestamp = model.Timestamp,
                PlantId = floraDevice.PlantId.Value
            };

            _context.Entries.Add(entry);

            await _context.SaveChangesAsync();
        }

        public async Task<PlantModel> FindPlantByDeviceId(string id)
        {
            var plantEntity = await _context.FloraDevices
                                    .Where(device => device.Id == id)
                                    .Select(device => device.Plant)
                                    .FirstOrDefaultAsync();

            return plantEntity.ToModel();
        }
    }
}
