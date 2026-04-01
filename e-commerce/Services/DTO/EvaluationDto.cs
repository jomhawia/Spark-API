namespace e_commerce.Services.DTO
{
    public class EvaluationDto
    {
        public string name {  get; set; }

        public bool PercentageOfUsed { get; set; }

        public bool PercentageOfScreen { get; set; }
        public bool PercentageOfBackScreen { get; set; }
        public bool PercentageOfBattery { get; set; }
        public bool PercentageOfCamera { get; set; }
        public bool PercentageOfOpen { get; set; }          
        public bool PercentageOfOutScrren { get; set; }     

        public bool PercentageOfBody { get; set; }      
        public bool PercentageOfBiometrics { get; set; }  
    }
}
