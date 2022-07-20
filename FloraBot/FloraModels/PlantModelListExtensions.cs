using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FloraModels
{
    public static class PlantModelListExtensions
    {
        public static List<PlantModel> NeedsWater(this List<StatusPlantModel> sourcePlants)
        {
            var plantsThatNeedWater = new List<PlantModel>();
            //sourcePlants.Where(p=> p.NeedsWater(p.Moisture));
            return plantsThatNeedWater;
        }
    }
}
