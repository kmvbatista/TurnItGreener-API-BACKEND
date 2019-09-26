﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTypeObject;
using System.Linq;

namespace BusinessLogicalLayer
{
    public class PlaceBLL : IPLACECRUD
    {
        private readonly BatteryCollectorDbContext placeDbContext;
        public PlaceBLL(BatteryCollectorDbContext _placeDbContext)
        {
            placeDbContext = _placeDbContext;
        }
        public PlaceBLL()
        {

        }
        public async Task Add(Place place)
        {
            try
            {
                await placeDbContext.Place.AddAsync(place);
                await placeDbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        public  Place Find(int Id)
        {
            try
            {
                return  placeDbContext.Place.Find(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Place>> GetAll()
        {
            try
            {
                return await GetListAsync();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        private Task<List<Place>> GetListAsync()
        {
            return Task.Run(() => placeDbContext.Place.ToList());
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(Place place)
        {
            throw new NotImplementedException();
        }
    }
}