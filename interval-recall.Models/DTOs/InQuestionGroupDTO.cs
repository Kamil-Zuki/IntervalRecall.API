using System.ComponentModel.DataAnnotations;

namespace interval_recall.Models.DTOs
{
    public class InQuestionGroupDTO
    {
        public string Title { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "AmountOfNew must be a non-negative number")]
        public int AmountOfNew { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "AmountOfLearn must be a non-negative number")]
        public int AmountOfLearn
        {
            get { return _amountOfLearn; }
            set
            {
                if (value < AmountOfNew * 10)
                {
                    throw new ArgumentOutOfRangeException("AmountOfLearn must be greater than or equal to AmountOfNew multiplied by 10");
                }
                _amountOfLearn = value;
            }
        }
        private int _amountOfLearn;
        public double IntervalModifier { get; set; }
        public double EasyBonus { get; set; }
        public double NewInterval { get; set; }
        public Guid? UserId { get; set; }
    }
}
