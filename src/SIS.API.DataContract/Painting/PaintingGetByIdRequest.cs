﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RedStarter.API.DataContract.Painting
{
    public class PaintingGetByIdRequest
    {
        public int PaintingEntityId { get; set; }
        public string Title { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }
    }
}
