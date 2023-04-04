using ProjectSims.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSims.Model
{
    public class TourAndGuideRating:ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int TourId { get; set; }
        public int KnowledgeGuide { get; set; }
        public int LanguageGuide { get; set; }
        public int InterestingTour { get; set; }
        public string AddedComment { get; set; }
        public List<string> Images { get; set; }

        public TourAndGuideRating()
        {
            Images = new List<string>();
        }

        public TourAndGuideRating(int guestId, int tourId, int knowledgeGuide, int languageGuide, int interestingTour, string addedComment, List<string> images)
        {
            GuestId = guestId;
            TourId = tourId;
            KnowledgeGuide = knowledgeGuide;
            LanguageGuide = languageGuide;
            InterestingTour = interestingTour;
            AddedComment = addedComment;
            Images = images;
        }

        public string[] ToCSV()
        {
            string ImageString = "";
            foreach (string image in Images)
            {
                if (image != Images.Last())
                {
                    ImageString += image + ",";
                }
            }
            ImageString += Images.Last();

            string[] csvvalues = { Id.ToString(), GuestId.ToString(), TourId.ToString(), KnowledgeGuide.ToString(),
                LanguageGuide.ToString(), InterestingTour.ToString(), AddedComment, ImageString};
            return csvvalues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            TourId = Convert.ToInt32(values[2]);
            KnowledgeGuide = Convert.ToInt32(values[3]);
            LanguageGuide = Convert.ToInt32(values[4]);
            InterestingTour = Convert.ToInt32(values[5]);
            AddedComment = values[6];
            foreach (string image in values[7].Split(","))
            {
                Images.Add(image);
            }
        }
    }
}
