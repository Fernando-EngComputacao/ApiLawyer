namespace API_Lawyer.Assets.Model.Parte
{
    public class ParteModel
    {
        private string Id { get; set; }
        private enum Tipo { Apelante, Apelado }
        private string NomePessoa { get; set; }
        private string NomeAdvogado { get; set; }

        private string IdNumeroProcesso { get; set; }

    }
}
