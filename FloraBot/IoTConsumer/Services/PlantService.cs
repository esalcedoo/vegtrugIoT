using FloraModels;
using IoTConsumer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task ProcessFloraDeviceMessage(IoTHub.Models.FloraDeviceMessageModel model)
        {
            var floraDevice = await _context.FloraDevices.FindAsync(model.DeviceId);

            if (floraDevice?.PlantId == null)
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

        public async Task ProcessFloraDeviceMessage(IoTCentralFunction.Models.FloraDeviceMessageModel model)
        {
            var floraDevice = await _context.FloraDevices.FindAsync(model.MessageProperties.MAC);

            if(floraDevice?.PlantId == null)
            {
                throw new InvalidOperationException();
            }

            floraDevice.Battery = model.Telemetry.Battery;

            var entry = new EntryEntity
            {
                Conductivity = model.Telemetry.Humidity,
                Light = model.Telemetry.Light,
                Moisture = model.Telemetry.Fertility,
                Temperature = model.Telemetry.Temperature,
                Timestamp = model.EnqueuedTime,
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

        public async Task<PlantModel> FindPlantById(int id)
        {
            var plantEntity = await _context.Plants
                                    .FindAsync(id);

            return plantEntity.ToModel();
        }

        public async Task<List<PlantModel>> FindPlantsById(List<int> ids)
        {
            var kk = await _context.Plants.ToListAsync();
            var plantsEntity = await _context.FloraDevices.Where(d => d.Active && ids.Contains(d.PlantId.GetValueOrDefault()))
                .Select(d => d.Plant)
                .ToListAsync();

            return plantsEntity.Select(plant => plant.ToModel()).ToList();
        }

        public async Task<List<StatusPlantModel>> GetStatus()
        {
            // TO-DO GroupBy with EFCore 5.0
            List<EntryEntity> entries = await _context.Entries
                                .Where(entry => entry.Timestamp > DateTime.Now.AddDays(-1))
                                .ToListAsync();

            return entries.GroupBy(entry => entry.PlantId)
                .Select(gr => gr.OrderByDescending(entry => entry.Timestamp).FirstOrDefault())
                .Select(entry => new StatusPlantModel
                {
                    Conductivity = entry.Conductivity,
                    Light = entry.Light,
                    Moisture = entry.Moisture,
                    PlantId = entry.PlantId,
                    Temperature = entry.Temperature,
                    Timestamp = entry.Timestamp.Value
                }).ToList();
        }
    }
}
