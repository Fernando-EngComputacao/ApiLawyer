namespace API_Lawyer.Model
{
    public class ParteModel
    {
        private String Id { get; set; }
        private enum Tipo {Apelante, Apelado} 
        private String NomePessoa { get; set; }
        private String NomeAdvogado { get; set; }
        
        private String IdNumeroProcesso { get; set; }

    }
}
