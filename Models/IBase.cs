using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RestaurantAPI.Models
{
    public interface IBase
    {
        public int Id { get; set; }
    }
}