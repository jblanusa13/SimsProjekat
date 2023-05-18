﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ProjectSims.Serializer;

namespace ProjectSims.Domain.Model
{
    public class SuperGuest : ISerializable
    {
        public int Id { get; set; }
        public DateOnly StartDate { get; set; }
        public int BonusPoints { get; set; }

        public SuperGuest() { }
        public SuperGuest(int id,  int bonusPoints, DateOnly startDate)
        {
            Id = id;
            BonusPoints = bonusPoints;
            StartDate = startDate;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartDate = DateOnly.ParseExact(values[1], "dd.MM.yyyy");
            BonusPoints = Convert.ToInt32(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvvalues =
            {
                Id.ToString(),
                StartDate.ToString(),
                BonusPoints.ToString()
            };
            return csvvalues;
        }
    }
}
