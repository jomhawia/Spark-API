namespace e_commerce.Entites
{
    public class PhoneEvaluation
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string PhoneBrand { get; set; } 


        public decimal BasePrice { get; set; }

        public float PercentageOfUsed { get; set; }

        public float PercentageOfScreen { get; set; }
        public float PercentageOfBackScreen { get; set; }
        public float PercentageOfBattery { get; set; }
        public float PercentageOfCamera { get; set; }
        public float PercentageOfOpen { get; set; }          
        public float PercentageOfOutScrren { get; set; }     

        public float PercentageOfBody { get; set; }      
        public float PercentageOfBiometrics { get; set; }    

    }
}
