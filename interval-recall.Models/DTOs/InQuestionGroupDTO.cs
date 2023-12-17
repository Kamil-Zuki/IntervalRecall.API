using System.ComponentModel.DataAnnotations;

namespace interval_recall.Models.DTOs
{
    public class InQuestionGroupDTO
    {
        public string Title { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "AmountOfNew must be a non-negative number")]
        public int AmountOfNew { get; set; }

        [Range(typeof(int), "0", "1000000", ErrorMessage = "AmountOfLearn must be a non-negative number")]
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
        public double IntervalModifier { get; set; } = 1.0;
        public double EasyBonus { get; set; } = 1.3;
        public double NewInterval { get; set; } = 0.2;
        public Guid? UserId { get; set; }
    }
}
