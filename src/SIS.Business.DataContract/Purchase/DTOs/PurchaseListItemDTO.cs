﻿using RedStarter.Business.DataContract.Painting;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedStarter.Business.DataContract.Purchase.DTOs
{
    public class PurchaseListItemDTO
    {
        public int PurchaseId { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }

        public virtual PaintingGetListItemDTO Painting { get; set; }
    }
}
