namespace WebApplication5.Services.Passwords 
{
    public class PasswordValidationResult 
    {
        public enum Marks {Excelent, Good, Normal, Bad};
        private float _result;
        public float Result
        {
            get { return _result; }
            set {
                if(value > 1) throw new ArgumentException();
                _result = value;
             }
        }
        public Marks EnumResult {
            get 
            {
                if(_result < 0.5f) return Marks.Bad;
                else if(_result >= 0.5f && _result < 0.6f) return Marks.Normal;
                else if(_result >= 0.6f && _result < 0.8f) return Marks.Good;
                else return Marks.Excelent;
            }
        }
    }
}