﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataTypeObject
{
    public class Discard
    {
        public int Id { get; set; }
        public Material Material { get; set; }
        public int MaterialId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Place Place { get; set; }
        public int PlaceId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public Discard()
        {
            

        }
        public Discard(Material material, int materialId, User user,
            int userId, Place place, int placeId, int quantity, DateTime date)
        {
            this.Material = material;
            this.MaterialId = materialId;
            this.Place = place;
            this.PlaceId = PlaceId;
            this.Quantity = quantity;
            this.User = user;
            this.UserId = userId;
        }
    }
}
